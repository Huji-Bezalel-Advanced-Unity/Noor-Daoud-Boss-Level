using LTD.Core.BaseMono;
using UnityEngine;
using UnityEngine.UI;

namespace LTD.Core.Player
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
        [SerializeField] private int maxHealth = 100;
        private Animator playerAnimator;

        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
            UpdateUI();
            Managers.AudioManager.LTDEvents.DecreasePlayerHealth += OnHealthDecreased;
            Managers.AudioManager.LTDEvents.IncreasePlayerHealth += OnHealthIncreased;
            
        }
        
        private void OnDestroy()
        {
            Managers.AudioManager.LTDEvents.DecreasePlayerHealth -= OnHealthDecreased;
            Managers.AudioManager.LTDEvents.IncreasePlayerHealth -= OnHealthIncreased;

        }
        
        #region Health Handling

        private void OnHealthIncreased()
        {
            _currentHealth += 2;
            UpdateUI();
        }
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

        private void OnPlayerDied()
        {
            UpdateUI();
            CoreManager.GameManager.Player.Animator.SetBool(Die, true); 
            Managers.AudioManager.LTDEvents.PlayerDies?.Invoke();
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