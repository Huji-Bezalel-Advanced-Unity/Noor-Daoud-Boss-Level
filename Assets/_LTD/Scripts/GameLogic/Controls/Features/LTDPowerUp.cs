using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace BLE.Gamelogic.Zone
{
    public class LTDPowerUp : LTDBaseMono
    {
        #region Animator Keys

        private static readonly int Magic = Animator.StringToHash("Magic");

        #endregion

        [Header("Power-Up Settings")]
        [SerializeField] private PowerUpType powerUpType;
        [Header("Animation Settings")]
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
                    Debug.Log("Increased speed");
                    break;

                case PowerUpType.IncreaseSpellPower:
                    LTDEvents.IncreaseSpellPowerUp?.Invoke();
                    Debug.Log("Increased spell power");
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
