using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace LTD.GameLogic.Loaders
{
    /// <summary>
    /// Handles the initial loading of the game, including manager setup and transitioning to the main game scene.
    /// Displays a loading screen with a fill animation followed by a start button.
    /// </summary>
    public class LTDGameLoader : LTDBaseMono
    {
        #region UI References

        [FormerlySerializedAs("_startButton")]
        [Header("UI Elements")]
        [SerializeField] private Button startButton;

        [FormerlySerializedAs("_imageSlider")] [SerializeField] private Image imageSlider;

        [SerializeField] private Slider loadingSlider;

        /// <summary>
        /// Reference to the coroutine animating the image slider.
        /// </summary>
        private Coroutine _sliderCoroutine;

        #endregion

        #region Game Initialization

        /// <summary>
        /// Initializes the game loader, sets up UI states, initializes core managers,
        /// and begins a visual fill animation to simulate loading.
        /// </summary>
        private void Awake()
        {
            startButton.gameObject.SetActive(false);
            imageSlider.gameObject.SetActive(true);

            var coreManager = new LTDCoreManager();
            coreManager.LoadManagers(() =>
            {
                ChangeValueOverTime(
                    ref _sliderCoroutine,
                    0f,
                    1f,
                    2.5f,
                    applyValue => imageSlider.fillAmount = applyValue,
                    () =>
                    {
                        imageSlider.gameObject.SetActive(false);
                        startButton.gameObject.SetActive(true);
                    });
            });
            
            startButton.onClick.AddListener(() =>
            {
                StartCoroutine(LoadNextSceneAsync());
            });
        }

        #endregion

        #region Scene Loading Logic

        /// <summary>
        /// Asynchronously loads the next game scene while updating the UI slider
        /// to reflect loading progress. Prevents scene activation until loading reaches 90%.
        /// </summary>
        private IEnumerator LoadNextSceneAsync()
        {
            int gameSceneIndex = 1;
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

        #endregion
    }
}
