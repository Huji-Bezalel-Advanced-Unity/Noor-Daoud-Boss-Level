using System;
using System.Collections;
using LTD.Core.BaseMono;
using LTD.Core;
using LTD.Core.Managers.AudioManager;
using UnityEngine;

namespace LTD.Core.Player
{
    public class LTDSpell : LTDBaseMono, ILTDSpell
    {

        private static readonly int Shooting = Animator.StringToHash("Shooting");
        [Header("Projectile Settings")]
        [SerializeField] private float speed;

        private Transform _target;
        
        
        public void FlyTowardsEnemy(Transform target)
        {
            _target = target;
            StartCoroutine(FlyCoroutine());
        }


        #region Coroutines

        private IEnumerator FlyCoroutine()
        {
            while (_target != null)
            {
                Vector3 direction = (_target.position - transform.position).normalized;
                transform.up = direction;
                transform.position += transform.up * speed * Time.deltaTime;

           //     LTDEvents.PlayerShoot.Invoke();

                yield return null;
            }

            Destroy(gameObject);
        }

        #endregion
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
        }

    }
}
