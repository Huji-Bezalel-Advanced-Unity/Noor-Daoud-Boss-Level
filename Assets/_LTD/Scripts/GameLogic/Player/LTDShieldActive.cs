using System.Collections;
using LTD.Core.BaseMono;
using UnityEngine;

namespace LTD.Core.Player
{
    public class LTDShieldActive : LTDBaseMono
    {
        [SerializeField] private GameObject shieldVisualPrefab;

        private float duration = 5f;

        public void ActivateShield()
        {
            
            StartCoroutine(ShieldRoutine());
        }

        private IEnumerator ShieldRoutine()
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                transform.position = CoreManager.GameManager.Player.transform.position;
                elapsed += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(other.gameObject);
        }
    }
}