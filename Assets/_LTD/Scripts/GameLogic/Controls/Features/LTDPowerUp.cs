using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace BLE.Gamelogic.Zone
{
    public class LTDPowerUp : LTDBaseMono
    {
        private static readonly int Magic = Animator.StringToHash("Magic");

        [Header("Power-Up Settings")]
        [SerializeField] private PowerUpType powerUpType;
        [SerializeField] private Animator animator;

        private bool _triggered = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_triggered) return;
            _triggered = true;

            switch (powerUpType)
            {
                case PowerUpType.IncreaseSpeed:
                    LTDEvents.IncreasePlayerSpeed?.Invoke();
                    print("Increased speed");
                    break;
                case PowerUpType.IncreaseSpellPower:
                    LTDEvents.IncreaseSpellPowerUp?.Invoke();
                    break;
            }

            if (animator != null)
            {
                animator.SetBool(Magic, true);
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
    public enum PowerUpType
    {
        IncreaseSpeed,
        IncreaseSpellPower
    }
}