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
        [SerializeField] private Slider loadingSlider;

        private void Awake()
        {
            var coreManager = new LTDCoreManager();
            coreManager.LoadManagers(() =>
            {
                StartCoroutine(LoadNextSceneAsync());
            });
        }

        IEnumerator LoadNextSceneAsync()
        {
            int gameSceneIndex = 0; 
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameSceneIndex);
            asyncLoad.allowSceneActivation = false;
            
            while (asyncLoad.progress < 0.9f)
            {
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                loadingSlider.value = progress;
                yield return null;
            }
            loadingSlider.value = 1f;
            asyncLoad.allowSceneActivation = true;
        }
    }
}