using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using UnityEngine;

namespace LTD.Gamelogic.Features
{
    /// <summary>
    /// Triggers visual feedback and global events when the player enters or exits a safe zone.
    /// Includes delayed animation when entering the zone.
    /// </summary>
    public class SafeZone : LTDBaseMono
    {

        private static readonly int Stop = Animator.StringToHash("Stop");
        
        [Header("References")]
        [Tooltip("Animator used to control safe zone visual effects.")]
        [SerializeField] private Animator animator;
        
        private Coroutine _delayCoroutine;


        #region Trigger Handlers

        /// <summary>
        /// Triggered when an object enters the collider.
        /// Starts safe zone logic and delayed animation.
        /// </summary>
        /// <param name="other">Collider that entered.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            LTDEvents.SafeZone?.Invoke();

            if (_delayCoroutine != null)
                StopCoroutine(_delayCoroutine);

            _delayCoroutine = StartCoroutine(DelayedAnimation());
        }

        /// <summary>
        /// Triggered when an object exits the collider.
        /// Cancels delay and disables animation immediately.
        /// </summary>
        /// <param name="other">Collider that exited.</param>
        private void OnTriggerExit2D(Collider2D other)
        {
            LTDEvents.RedZone?.Invoke();

            if (_delayCoroutine != null)
            {
                StopCoroutine(_delayCoroutine);
                _delayCoroutine = null;
            }

            animator.SetBool(Stop, false);
        }

        #endregion

        #region Coroutine

        /// <summary>
        /// Waits for a short duration before enabling the "Stop" animation.
        /// </summary>
        private IEnumerator DelayedAnimation()
        {
            yield return new WaitForSeconds(3f);
            animator.SetBool(Stop, true);
        }

        #endregion
    }
}
