using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LTD.GameLogic.Louders
{
    public class LTDGameLoader : LTDBaseMono
    {
        [Header("UI Elements")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Image _imageSlider;
        [SerializeField] private Slider loadingSlider;
        private Coroutine _sliderCoroutine;

        #region Game Initialization

        private void Awake()
        {
            _startButton.gameObject.SetActive(false);
            _imageSlider.gameObject.SetActive(true);

            var coreManager = new LTDCoreManager();
            coreManager.LoadManagers(() =>
            {
                ChangeValueOverTime(ref _sliderCoroutine, 0f, 1f, 0.4f, applyValue => _imageSlider.fillAmount = applyValue, () =>
                {
                    _imageSlider.gameObject.SetActive(false);
                    _startButton.gameObject.SetActive(true);
                });
            });

            _startButton.onClick.AddListener(() =>
            {
                StartCoroutine(LoadNextSceneAsync());
            });
        }

        #endregion

        #region Scene Loading Logic

        private IEnumerator LoadNextSceneAsync()
        {
            int gameSceneIndex = 1;
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(gameSceneIndex);

            // LTDAudioManager.Instance.PlayGameMusic();

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

        #endregion
    }
}