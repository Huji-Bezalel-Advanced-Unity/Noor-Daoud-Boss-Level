using System.Collections;
using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDBossHealth : LTDBaseMono
    {
       // [SerializeField] private Animator animator;

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
      //      animator?.SetTrigger("Hurt");
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
          //  animator?.SetTrigger("Die");
            Events.DevilDies?.Invoke();

            Destroy(this.gameObject, 1.5f); // delay to let animation play
        }
    }
}