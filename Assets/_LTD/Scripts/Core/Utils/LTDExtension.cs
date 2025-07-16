using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LTD.Core.Utils
{
    public static class LTDExtension
    {
        #region Coroutines:

        public static IEnumerator ChangeValueOverTime(float startValue, float endValue, float duration, Action<float> applyValue, Action onComplete = null)
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
        public static void StopAndStartCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutine, IEnumerator routine)
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
            }
            coroutine = monoBehaviour.StartCoroutine(routine);
        }
 
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
