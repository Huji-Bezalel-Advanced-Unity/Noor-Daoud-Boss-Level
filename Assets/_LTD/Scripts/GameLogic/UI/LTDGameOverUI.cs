using System.Collections;
using LTD.Core.Managers.AudioManager;
using LTD.Core.BaseMono;
using LTD.Core.Enemies;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _LTD.Scripts.GameLogic.UI
{
    public class LTDGameOverUI : LTDBaseMono
    {

        [Header("UI References")]
        [FormerlySerializedAs("playerDeathPanel")]
        [SerializeField] private GameObject losingPanel;

        [FormerlySerializedAs("devilDefeatedPanel")]
        [SerializeField] private GameObject winningPanel;
        
        private void Awake()
        {
            losingPanel.SetActive(false);
            winningPanel.SetActive(false);

            LTDEvents.PlayerDies += ShowPlayerDeathPanel;
            LTDEvents.DevilDies += ShowDevilDefeatedPanel;
        }

        private void OnDestroy()
        {
            LTDEvents.PlayerDies -= ShowPlayerDeathPanel;
            LTDEvents.DevilDies -= ShowDevilDefeatedPanel;
        }


        #region Game Over Logic
        
        private void ShowPlayerDeathPanel()
        {
            losingPanel.SetActive(true);
            StartCoroutine(DelayPauseGame());
        }

        private void ShowDevilDefeatedPanel()
        {
            winningPanel.SetActive(true);
            StartCoroutine(DelayPauseGame());
        }

        private IEnumerator DelayPauseGame()
        {
            yield return new WaitForSecondsRealtime(1f); // Not affected by timeScale
            Time.timeScale = 0f;
        }

        #endregion

        #region Public UI Buttons
        
        public void RestartGame()
        {
            if (CoreManager.GameManager.Boss != null)
            {
                CoreManager.GameManager.Boss.DestroyAllFireballs();
            }
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion
    }
}