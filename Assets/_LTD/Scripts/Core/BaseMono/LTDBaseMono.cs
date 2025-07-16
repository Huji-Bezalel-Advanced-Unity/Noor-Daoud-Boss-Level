using System;
using System.Collections;
using LTD.Core.Managers.AudioManager;
using LTD.Core.Utils;
using UnityEngine;

namespace LTD.Core.BaseMono
{
    public class LTDBaseMono : MonoBehaviour
    {
        protected LTDCoreManager CoreManager => LTDCoreManager.Instance;

        public void ChangeValueOverTime(ref Coroutine coroutine, float startValue, float endValue, 
            float duration, Action<float> applyValue, Action onComplete = null)
        {
            this.StopAndStartCoroutine(ref coroutine, LTDExtension.ChangeValueOverTime(startValue, endValue, duration, applyValue, onComplete));
        }


        private IEnumerator WaitForSeconds(int seconds, Action onComplete)
        {
            yield return new WaitForSeconds(seconds);
            onComplete?.Invoke();
        }
        
    }
}