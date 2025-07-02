using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace BLE.Gamelogic.Zone
{
    public class LTDIncreaseSpellPower:LTDBaseMono
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string triggerAnimationName = "PlayEffect";

        private bool _triggered = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_triggered) return;
            _triggered = true;

            LTDEvents.IncreaseSpellPowerUp?.Invoke();

            if (animator != null)
            {
                animator.SetTrigger(triggerAnimationName);
                StartCoroutine(DestroyAfterAnimation(animator));
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator DestroyAfterAnimation(Animator anim)
        {
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject);
        }
    }
}
   