using System.Collections;
using _LTD.Scripts.GameLogic.Controls.Player;
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

        [Header("Animation Settings")]
        [SerializeField] private Animator animator;

        [SerializeField] private LTDShieldActive shieldPrefab;

        private bool _triggered = false;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_triggered) return;
            _triggered = true;
            LTDAudioManager.Instance.PlaySFX(LTDAudioManager.Instance.powerUpSFX);
            switch (powerUpType)
            {
                case PowerUpType.IncreaseSpeed:
                    LTDEvents.IncreasePlayerSpeed?.Invoke();
                    break;

                case PowerUpType.IncreaseHealth:
                    LTDEvents.IncreasePlayerHealth?.Invoke();
                    break;

                case PowerUpType.shield:
                    var player = CoreManager.GameManager.Player;
                    if (player != null)
                    {
                        var shield = Instantiate(shieldPrefab, player.transform.position, Quaternion.identity);
                        shield.gameObject.SetActive(true);
                        shield.ActivateShield();
                    }
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
        IncreaseHealth,
        shield
    }
}