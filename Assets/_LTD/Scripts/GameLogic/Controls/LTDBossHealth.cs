using System.Collections;
using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDBossHealth : LTDBaseMono
    {
        private static readonly int Die1 = Animator.StringToHash("Die");
        private static readonly int Hurt = Animator.StringToHash("Hurt");
        [SerializeField] private Animator animator;

        private int _currentHealth = 4;

        private void Awake()
        {
            Events.DecreaseDevilHealth += TakeDamage;
        }

        private void OnDestroy()
        {
            Events.DecreaseDevilHealth -= TakeDamage;
        }

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
            animator?.SetBool(Die1,true);
            Events.DevilDies?.Invoke();

            Destroy(this.gameObject, 1.5f); 
        }
    }
}