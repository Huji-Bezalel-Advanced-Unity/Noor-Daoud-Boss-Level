using System.Collections;
using LTD.GameLogic.BaseMono;
using UnityEngine;

namespace LTD.GameLogic.Player
{
    /// <summary>
    /// Represents an active shield that follows the player and destroys any enemy projectiles or objects on contact.
    /// Automatically expires after a set duration.
    /// </summary>
    public class LTDShieldActive : LTDBaseMono
    {
        
        [Header("Shield Visual")]
        [Tooltip("Prefab used to display the shield visually (optional, not instantiated in current implementation).")]
        [SerializeField] private GameObject shieldVisualPrefab;
        
        [Tooltip("Duration the shield remains active in seconds.")]
        private float duration = 5f;
        
        /// <summary>
        /// Begins the shield lifecycle coroutine which follows the player and destroys itself after duration ends.
        /// </summary>
        public void ActivateShield()
        {
            StartCoroutine(ShieldRoutine());
        }
        
        #region Shield Logic

        /// <summary>
        /// Coroutine that updates the shield's position to follow the player and destroys the shield after time expires.
        /// </summary>
        private IEnumerator ShieldRoutine()
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                if (CoreManager.GameManager?.Player != null)
                {
                    transform.position = CoreManager.GameManager.Player.transform.position;
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }

        /// <summary>
        /// Handles collision logic while the shield is active.
        /// Destroys any object that enters its trigger collider.
        /// </summary>
        /// <param name="other">The other collider entering the shield.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }

        #endregion
    }
}
