using LTD.Core.Managers;
using LTD.Core.BaseMono;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _LTD.Scripts.GameLogic.UI
{
    public class LTDDevilDefeatedUI : LTDBaseMono
    {
        [Header("UI References")]
        [SerializeField] private GameObject defeatPanel;

        private void Awake()
        {
            defeatPanel.SetActive(false); 
            Events.DevilDies += ShowDefeatPanel;
        }

        private void OnDestroy()
        {
            Events.DevilDies -= ShowDefeatPanel;
        }

        private void ShowDefeatPanel()
        {
            defeatPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        public void EndGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
#else
            Application.Quit(); // Quit the application if built
#endif
        }

        public void RestartGame()
        {
            Time.timeScale = 1f; 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}