using System.Collections;
using LTD.GameLogic.Player;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using UnityEngine;

namespace LTD.Gamelogic.Features
{
    /// <summary>
    /// Represents a collectible power-up in the game world that grants a specific effect when triggered by the player.
    /// </summary>
    public class LTDPowerUp : LTDBaseMono
    {

        private static readonly int Magic = Animator.StringToHash("Magic");
        
        [Header("Power-Up Settings")]
        [Tooltip("Type of power-up to apply when collected.")]
        [SerializeField] private PowerUpType powerUpType;

        [Header("Animation Settings")]
        [Tooltip("Animator used for power-up collection visuals.")]
        [SerializeField] private Animator animator;

        [Tooltip("Shield prefab to instantiate when the power-up grants shielding.")]
        [SerializeField] private LTDShieldActive shieldPrefab;

        [Tooltip("Collider to disable once triggered to prevent multiple activations.")]
        [SerializeField] private Collider2D _triggeredCollider2D;
        
        #region Trigger Handling

        /// <summary>
        /// Handles the logic when the player collides with the power-up.
        /// Applies the appropriate effect based on power-up type.
        /// </summary>
        /// <param name="other">Collider that triggered the event.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            _triggeredCollider2D.enabled = false;

            LTDAudioManager.Instance.PlaySFX(LTDAudioManager.AudioClipType.PowerUp);

            switch (powerUpType)
            {
                case PowerUpType.IncreaseSpeed:
                    LTDEvents.IncreasePlayerSpeed?.Invoke();
                    break;

                case PowerUpType.IncreaseHealth:
                    LTDEvents.IncreasePlayerHealth?.Invoke();
                    break;

                case PowerUpType.Shield:
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

        #endregion

        #region Animation Handling

        /// <summary>
        /// Waits for the end of the animation before destroying the game object.
        /// </summary>
        /// <param name="anim">Animator playing the animation.</param>
        private IEnumerator DestroyAfterAnimation(Animator anim)
        {
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject);
        }

        #endregion
    }

    /// <summary>
    /// Enum representing the types of power-ups the player can collect.
    /// </summary>
    public enum PowerUpType
    {
        IncreaseSpeed,
        IncreaseHealth,
        Shield
    }
}
