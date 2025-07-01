using LTD.Core.BaseMono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDPlayerHealthUI : LTDBaseMono
    {
        [Header("UI Elements")]
        [SerializeField] private Slider healthSlider;

        [Header("Settings")]
        [SerializeField] private int maxHealth = 20;

        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
            UpdateUI();
            LTD.Core.Managers.Events.DecreasePlayerHealth += OnHealthDecreased;
            LTD.Core.Managers.Events.PlayerDies += OnPlayerDied;
        }

        private void OnDestroy()
        {
            LTD.Core.Managers.Events.DecreasePlayerHealth -= OnHealthDecreased;
            LTD.Core.Managers.Events.PlayerDies -= OnPlayerDied;
        }

        private void OnHealthDecreased()
        {
            _currentHealth = Mathf.Max(_currentHealth - 1, 0);
            UpdateUI();
        }

        private void OnPlayerDied()
        {
            _currentHealth = 0; 
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (healthSlider != null)
                healthSlider.value = (float)_currentHealth / maxHealth;
        }
    }
}