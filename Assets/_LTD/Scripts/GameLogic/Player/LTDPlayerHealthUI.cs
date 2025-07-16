using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace LTD.GameLogic.Player
{
    /// <summary>
    /// Handles the player's health UI logic and health state tracking.
    /// Listens to game events to update the health slider and trigger animations.
    /// </summary>
    public class LTDPlayerHealthUI : LTDBaseMono
    {
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        private static readonly int Die = Animator.StringToHash("Die");

        [Header("UI Elements")]
        [Tooltip("Slider representing player's current health.")]
        [SerializeField] private Slider healthSlider;

        [Header("Settings")]
        [Tooltip("Maximum health of the player.")]
        [SerializeField] private int maxHealth = 30;

        private int _currentHealth;
        
        private void Awake()
        {
            _currentHealth = maxHealth;
            UpdateUI();

            LTDEvents.DecreasePlayerHealth += OnHealthDecreased;
            LTDEvents.IncreasePlayerHealth += OnHealthIncreased;
        }

        private void OnDestroy()
        {
            LTDEvents.DecreasePlayerHealth -= OnHealthDecreased;
            LTDEvents.IncreasePlayerHealth -= OnHealthIncreased;
        }
        
        #region Health Handling

        /// <summary>
        /// Increases the player's health and updates UI.
        /// </summary>
        private void OnHealthIncreased()
        {
            _currentHealth += 2;
            UpdateUI();
        }

        /// <summary>
        /// Decreases the player's health and checks for death.
        /// </summary>
        /// <param name="amount">Amount of health to decrease.</param>
        private void OnHealthDecreased(int amount)
        {
            CoreManager.GameManager.Player.Animator.SetTrigger(Hurt);
            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                OnPlayerDied();
            }
            else
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Called when player's health reaches zero.
        /// Triggers death event and final UI update.
        /// </summary>
        private void OnPlayerDied()
        {
            UpdateUI();
            LTDEvents.PlayerDies?.Invoke();
        }

        #endregion

        #region UI

        /// <summary>
        /// Updates the UI slider based on current health.
        /// </summary>
        private void UpdateUI()
        {
            if (healthSlider != null)
                healthSlider.value = (float)_currentHealth / maxHealth;
        }

        #endregion
    }
}
