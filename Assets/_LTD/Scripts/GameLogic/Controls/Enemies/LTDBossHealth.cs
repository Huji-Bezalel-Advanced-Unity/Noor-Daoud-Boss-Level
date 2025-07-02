using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDBossHealth : LTDBaseMono
    {
        #region Animator Hashes

        private static readonly int Die1 = Animator.StringToHash("Die");
        private static readonly int Hurt = Animator.StringToHash("Hurt");

        #endregion
        
        [Header("Boss Animation")]
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

        private void TakeDamage()
        {
            _currentHealth--;
            animator?.SetTrigger(Hurt);

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            animator?.SetBool(Die1, true);
            LTDEvents.DevilDies?.Invoke();

            Destroy(gameObject, 1.5f); // Delay to let death animation play
        }

        #endregion
    }
}