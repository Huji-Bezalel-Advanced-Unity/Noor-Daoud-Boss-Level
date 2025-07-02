using LTD.Core.BaseMono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDPlayerHealthUI : LTDBaseMono
    {
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        private static readonly int Die = Animator.StringToHash("Die");

        [Header("UI Elements")]
        [SerializeField] private Slider healthSlider;

        [Header("Settings")]
        [SerializeField] private int maxHealth = 200;
        [SerializeField] private Animator playerAnimator;
        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
            UpdateUI();
            LTD.Core.Managers.LTDEvents.DecreasePlayerHealth += OnHealthDecreased;
        }

        private void OnDestroy()
        {
            LTD.Core.Managers.LTDEvents.DecreasePlayerHealth -= OnHealthDecreased;
        }

        private void OnHealthDecreased()
        {
            playerAnimator.SetTrigger(Hurt);
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
            playerAnimator.SetBool(Die,true);
            LTD.Core.Managers.LTDEvents.PlayerDies.Invoke();
        }

        private void UpdateUI()
        {
            if (healthSlider != null)
                healthSlider.value = (float)_currentHealth / maxHealth;
        }
    } 
}