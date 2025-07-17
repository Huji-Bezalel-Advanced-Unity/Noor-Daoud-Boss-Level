using System.Collections;
using LTD.GameLogic.BaseMono;
using UnityEngine;

namespace LTD.GameLogic.Player
{
    /// <summary>
    /// Represents a spell projectile that automatically moves toward a specified target (typically an enemy).
    /// Destroys itself either when it reaches the target or collides with any object.
    /// </summary>
    public class LTDSpell : LTDBaseMono, ILTDSpell
    {
        
        [Header("Projectile Settings")]
        [Tooltip("The movement speed of the spell projectile.")]
        [SerializeField] private float speed;
        
        private Transform _target;
        

        /// <summary>
        /// Initializes the spell's trajectory toward a given target.
        /// </summary>
        /// <param name="target">The enemy transform to home in on.</param>
        public void FlyTowardsEnemy(Transform target)
        {
            _target = target;
            StartCoroutine(FlyCoroutine());
        }
        

        #region Coroutines

        /// <summary>
        /// Coroutine responsible for moving the spell toward its target each frame.
        /// If the target is destroyed or lost, the spell destroys itself.
        /// </summary>
        private IEnumerator FlyCoroutine()
        {
            while (_target != null)
            {
                Vector3 direction = (_target.position - transform.position).normalized;
                
                transform.up = direction;
                
                transform.position += transform.up * speed * Time.deltaTime;

                yield return null;
            }

            Destroy(gameObject);
        }

        #endregion

        #region Collision Handling

        /// <summary>
        /// Destroys the spell on any collision trigger.
        /// Consider refining with tag/layer checks if needed.
        /// </summary>
        /// <param name="other">Collider the spell collided with.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
        }

        #endregion
    }
}
