using LTD.GameLogic.Managers;
using UnityEngine;

namespace LTD.GameLogic.Enemies
{
    /// <summary>
    /// Represents a small devil enemy that moves in a randomly offset direction and damages the player on contact.
    /// </summary>
    public class LTDSmallDevils : MonoBehaviour
    {
   

        [Header("Movement Settings")]
        [Tooltip("Movement speed of the small devil.")]
        [SerializeField] private float speed = 12f;

        [Header("Animation")]
        [Tooltip("Animator component for visual effects (optional).")]
        [SerializeField] private Animator animator;

        
        private Vector2 _moveDirection;
        private Rigidbody2D _rb;


        #region Unity Methods
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Applies constant movement in the chosen direction.
        /// </summary>
        private void FixedUpdate()
        {
            _rb.linearVelocity = _moveDirection * speed;
        }

        /// <summary>
        /// Handles collision with the player and applies damage.
        /// </summary>
        /// <param name="other">The collider that entered the trigger.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                LTDEvents.DecreasePlayerHealth?.Invoke(3);
            }

            Destroy(gameObject);
        }

        #endregion

        #region Movement Logic

        /// <summary>
        /// Sets the movement direction with a random angular deviation to create spread effect.
        /// </summary>
        /// <param name="direction">The base direction toward which the enemy should move.</param>
        public void SetDirection(Vector2 direction)
        {
            float randomAngle = Random.Range(-40f, 40f);
            float radian = randomAngle * Mathf.Deg2Rad;

            float cos = Mathf.Cos(radian);
            float sin = Mathf.Sin(radian);

            Vector2 rotatedDirection = new Vector2(
                direction.x * cos - direction.y * sin,
                direction.x * sin + direction.y * cos
            );

            _moveDirection = rotatedDirection.normalized;
        }

        #endregion
    }
}
