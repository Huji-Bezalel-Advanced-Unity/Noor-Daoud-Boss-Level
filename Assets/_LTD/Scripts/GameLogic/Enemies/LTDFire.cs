using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using UnityEngine;

namespace LTD.GameLogic.Enemies
{
    /// <summary>
    /// Represents a projectile fired by the boss. Handles movement and collision with the player.
    /// </summary>
    public class LTDFire : LTDBaseMono
    {

        /// <summary>
        /// Reference to the Rigidbody2D component for movement control.
        /// </summary>
        private Rigidbody2D _rigidbody2D;

        /// <summary>
        /// Initializes the projectile by disabling gravity.
        /// </summary>
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0f;
        }
        

        #region Projectile Logic

        /// <summary>
        /// Launches the projectile in a specified direction with a given speed.
        /// </summary>
        /// <param name="direction">Normalized direction vector.</param>
        /// <param name="speed">Speed of the projectile.</param>
        public void Shoot(Vector3 direction, float speed)
        {
            _rigidbody2D.linearVelocity = direction * speed;
        }

        /// <summary>
        /// Handles collision with other objects. If the player is hit, triggers health decrease.
        /// </summary>
        /// <param name="other">Collider of the object entered.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                LTDEvents.DecreasePlayerHealth?.Invoke(3);
            }
            Destroy(gameObject);
        }

        #endregion
    }
}