using LTD.GameLogic.BaseMono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDPlayerHealthUI : LTDBaseMono
    {
        #region Animator Hashes

        private static readonly int Hurt = Animator.StringToHash("Hurt");
        private static readonly int Die = Animator.StringToHash("Die");

        #endregion

        [Header("UI Elements")]
        [SerializeField] private Slider healthSlider;

        [Header("Settings")]
        [SerializeField] private int maxHealth = 200;
        private Animator playerAnimator;

        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
            UpdateUI();
            LTD.GameLogic.Controls.LTDEvents.DecreasePlayerHealth += OnHealthDecreased;
            LTD.GameLogic.Controls.LTDEvents.IncreasePlayerHealth += OnHealthIncreased;
            
        }

       

        private void OnDestroy()
        {
            LTD.GameLogic.Controls.LTDEvents.DecreasePlayerHealth -= OnHealthDecreased;
            LTD.GameLogic.Controls.LTDEvents.IncreasePlayerHealth -= OnHealthIncreased;

        }


        #region Health Handling

        private void OnHealthIncreased()
        {
            _currentHealth = Mathf.Max(_currentHealth + 3, 0);
            UpdateUI();
            print("Health Increased");
        }
        private void OnHealthDecreased()
        {
            CoreManager.GameManager.Player.Animator.SetTrigger(Hurt);

            _currentHealth = Mathf.Max(_currentHealth - 1, 0);

            if (_currentHealth == 0)
            {
                OnPlayerDied();
            }
            else
            {
                UpdateUI();
            }
        }

        private void OnPlayerDied()
        {
            UpdateUI();
            CoreManager.GameManager.Player.Animator.SetBool(Die, true);
            LTD.GameLogic.Controls.LTDEvents.PlayerDies?.Invoke();
        }

        #endregion

        #region UI

        private void UpdateUI()
        {
            if (healthSlider != null)
                healthSlider.value = (float)_currentHealth / maxHealth;
        }

        #endregion
    }
}