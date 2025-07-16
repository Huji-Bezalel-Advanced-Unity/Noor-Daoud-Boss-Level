using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using UnityEngine;

namespace LTD.GameLogic.Enemies
{
    /// <summary>
    /// Manages the health, damage behavior, and death logic for the boss enemy.
    /// Plays hurt animations, plays audio, and triggers death events when health reaches zero.
    /// </summary>
    public class LTDBossHealth : LTDBaseMono
    {

        /// <summary>
        /// Cached hash for the "Hurt" trigger in the Animator.
        /// </summary>
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        
        [Header("Boss Animation")]
        [Tooltip("Animator component controlling the boss animations.")]
        [SerializeField] private Animator animator;
        
        private int _currentHealth = 4;
        
        private void Awake()
        {
            LTDEvents.DecreaseDevilHealth += TakeDamage;
        }

        private void OnDestroy()
        {
            LTDEvents.DecreaseDevilHealth -= TakeDamage;
        }
        
        #region Damage Handling

        /// <summary>
        /// Called when the boss takes damage. Triggers hurt animation and sound.
        /// Destroys the boss if health reaches zero.
        /// </summary>
        private void TakeDamage()
        {
            _currentHealth--;
            animator?.SetTrigger(Hurt);
            LTDAudioManager.Instance.PlaySFX(LTDAudioManager.AudioClipType.DevilsHurt);

            if (_currentHealth <= 0)
            {
                LTDEvents.DevilDies?.Invoke();
                Destroy(gameObject, 1f);
            }
        }

        #endregion
    }
}
