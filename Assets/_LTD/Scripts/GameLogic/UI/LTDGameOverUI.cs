using System.Collections;
using LTD.GameLogic.Managers;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Enemies;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace LTD.GameLogic.UI
{
    /// <summary>
    /// Manages the Game Over UI for both losing and winning scenarios.
    /// Handles activation of corresponding panels and pause behavior.
    /// </summary>
    public class LTDGameOverUI : LTDBaseMono
    {
        [Header("UI References")]
        [FormerlySerializedAs("playerDeathPanel")]
        [Tooltip("Panel displayed when the player dies.")]
        [SerializeField] private GameObject losingPanel;

        [FormerlySerializedAs("devilDefeatedPanel")]
        [Tooltip("Panel displayed when the boss is defeated.")]
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

        /// <summary>
        /// Displays the losing panel and pauses the game after a short delay.
        /// </summary>
        private void ShowPlayerDeathPanel()
        {
            losingPanel.SetActive(true);
            StartCoroutine(DelayPauseGame());
        }

        /// <summary>
        /// Displays the winning panel and pauses the game after a short delay.
        /// </summary>
        private void ShowDevilDefeatedPanel()
        {
            winningPanel.SetActive(true);
            StartCoroutine(DelayPauseGame());
        }

        /// <summary>
        /// Delays pausing the game for 1 second using unscaled time.
        /// </summary>
        private IEnumerator DelayPauseGame()
        {
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 0f;
        }

        #endregion

        #region Public UI Buttons

        /// <summary>
        /// Restarts the current scene and resumes time. Destroys all remaining fireballs before reload.
        /// </summary>
        public void RestartGame()
        {
            if (CoreManager.GameManager.Boss != null)
            {
                CoreManager.GameManager.Boss.DestroyAllFireballs();
            }

            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Quits the game or stops play mode in the Unity Editor.
        /// </summary>
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
