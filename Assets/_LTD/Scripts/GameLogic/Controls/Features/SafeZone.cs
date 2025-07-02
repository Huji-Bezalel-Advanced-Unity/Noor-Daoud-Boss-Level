using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace BLE.Gamelogic.Zone
{
    public class SafeZone : LTDBaseMono
    {
        #region Animator Keys

        private static readonly int Stop = Animator.StringToHash("Stop");

        #endregion
        
        [Header("References")]
        [SerializeField] private Animator animator;

        private Coroutine _delayCoroutine;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            LTDEvents.SafeZone?.Invoke();

            if (_delayCoroutine != null)
                StopCoroutine(_delayCoroutine);

            _delayCoroutine = StartCoroutine(DelayedAnimation());
        }
        
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
        
        private IEnumerator DelayedAnimation()
        {
            yield return new WaitForSeconds(3f);
            animator.SetBool(Stop, true);
        }
    }
}