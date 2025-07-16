using System;
using System.Collections;
using UnityEngine;

namespace LTD.GameLogic.Utils
{
    /// <summary>
    /// Provides extension methods and coroutine utilities for timing and value interpolation in Unity.
    /// </summary>
    public static class LTDExtension
    {
        #region Coroutines

        /// <summary>
        /// Interpolates a float value from start to end over a specified duration using Mathf.Lerp.
        /// Applies the interpolated value each frame via a callback, and calls an optional completion callback at the end.
        /// </summary>
        /// <param name="startValue">The starting float value.</param>
        /// <param name="endValue">The target float value.</param>
        /// <param name="duration">Duration in seconds for the interpolation.</param>
        /// <param name="applyValue">Callback invoked with the current interpolated value each frame.</param>
        /// <param name="onComplete">Optional callback invoked when the interpolation is complete.</param>
        /// <returns>An IEnumerator for use with Unity's coroutine system.</returns>
        public static IEnumerator ChangeValueOverTime(
            float startValue, 
            float endValue, 
            float duration, 
            Action<float> applyValue, 
            Action onComplete = null)
        {
            applyValue(startValue);
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
                applyValue(currentValue);
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            applyValue(endValue);
            onComplete?.Invoke();
        }

        /// <summary>
        /// Stops the current coroutine (if running) and starts a new one.
        /// Useful for restarting animations or timed actions safely.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour running the coroutine.</param>
        /// <param name="coroutine">Reference to the coroutine being managed.</param>
        /// <param name="routine">The new coroutine to start.</param>
        public static void StopAndStartCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutine, IEnumerator routine)
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
            }

            coroutine = monoBehaviour.StartCoroutine(routine);
        }

        /// <summary>
        /// Stops a coroutine if it's running and nullifies its reference.
        /// Useful for safe cancellation and cleanup of repeating actions.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour running the coroutine.</param>
        /// <param name="coroutine">Reference to the coroutine being managed.</param>
        public static void StopWithNullifyCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
            }

            coroutine = null;
        }

        #endregion
    }
}
