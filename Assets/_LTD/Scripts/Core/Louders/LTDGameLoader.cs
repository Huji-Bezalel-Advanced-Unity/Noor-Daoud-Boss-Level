using System.Collections;
using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LTD.Core.Louders
{
    public class LTDGameLoader : LTDBaseMono
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject loadingScreen;
        [SerializeField] private GameObject startButton; // The "Start" button shown after loading
        [SerializeField] private Slider loadingSlider;

        private AsyncOperation sceneLoadingOperation;
        private bool managersLoaded = false;

        private void Awake()
        {
            // Hide UI elements at startup
            loadingScreen.SetActive(true);
            startButton.SetActive(false);

            // Load managers first
            var coreManager = new LTDCoreManager();
            coreManager.LoadManagers(() =>
            {
                managersLoaded = true;
                StartCoroutine(LoadGameAsync());
            });
        }

        IEnumerator LoadGameAsync()
        {
            int gameSceneIndex = 1; // Scene to load
            sceneLoadingOperation = SceneManager.LoadSceneAsync(gameSceneIndex);
            sceneLoadingOperation.allowSceneActivation = false; // Don't activate yet

            while (sceneLoadingOperation.progress < 0.9f)
            {
                float progress = Mathf.Clamp01(sceneLoadingOperation.progress / 0.9f);
                loadingSlider.value = progress;
                yield return null;
            }

            // Fully loaded, now show the Start button
            loadingSlider.value = 1f;
            startButton.SetActive(true);
        }

        public void StartGame()
        {
            if (managersLoaded && sceneLoadingOperation != null)
            {
                sceneLoadingOperation.allowSceneActivation = true;
            }
            else
            {
                Debug.LogWarning("Managers not loaded or scene not ready!");
            }
        }
    }
}
