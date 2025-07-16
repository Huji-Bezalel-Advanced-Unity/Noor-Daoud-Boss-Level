using System;
using LTD.GameLogic.Managers;
using LTD.GameLogic.Utils;
using UnityEngine;

namespace LTD.GameLogic.BaseMono
{
    /// <summary>
    /// Base MonoBehaviour class for LTD core systems.
    /// Provides access to the CoreManager singleton and utility functionality like value interpolation over time.
    /// </summary>
    public class LTDBaseMono : MonoBehaviour
    {

        /// <summary>
        /// Reference to the LTDCoreManager singleton instance.
        /// Used to access shared managers and systems in the game.
        /// </summary>
        protected LTDCoreManager CoreManager => LTDCoreManager.Instance;
        
        #region Utility Methods

        /// <summary>
        /// Smoothly changes a float value from a start value to an end value over a specified duration.
        /// This is typically used for animations, UI fades, or timed effects.
        /// </summary>
        /// <param name="coroutine">A reference to the coroutine to manage cancellation and restarting.</param>
        /// <param name="startValue">The initial float value.</param>
        /// <param name="endValue">The final float value after the duration ends.</param>
        /// <param name="duration">Time in seconds over which the value should interpolate.</param>
        /// <param name="applyValue">Callback invoked every frame with the interpolated value.</param>
        /// <param name="onComplete">Optional callback to invoke when interpolation finishes.</param>
        public void ChangeValueOverTime(ref Coroutine coroutine, float startValue, float endValue, 
            float duration, Action<float> applyValue, Action onComplete = null)
        {
            this.StopAndStartCoroutine(
                ref coroutine, 
                LTDExtension.ChangeValueOverTime(startValue, endValue, duration, applyValue, onComplete)
            );
        }
        #endregion
    }
}