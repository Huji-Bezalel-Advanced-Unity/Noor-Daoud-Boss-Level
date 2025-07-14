using LTD.Core.Managers;
using LTD.Core.BaseMono;
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
            Time.timeScale = 0f;
        }
        
        private void ShowDevilDefeatedPanel()
        {
            winningPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        #endregion

        #region Public UI Buttons
        
        public void RestartGame()
        {
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