using System;
using System.Collections;
using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine;

namespace LTD.GameLogic.Controls
{
    public class LTDBaseProjectile : LTDBaseMono, ILTDBaseProjectile
    {
        private static readonly int Shooting = Animator.StringToHash("Shooting");
        [SerializeField] private float speed;
        private Transform _target;

 
        public void FlyTowardsEnemy(Transform target)
        {
            _target = target;
            StartCoroutine(FlyCoroutine());
        }

        private IEnumerator FlyCoroutine()
        {
            while (_target != null)
            {
                Vector3 direction = (_target.position - transform.position).normalized;
                transform.up = direction; 
                transform.position += transform.up * speed * Time.deltaTime;
                Events.PlayerShoot.Invoke();
                yield return null;
            }

            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
       
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}