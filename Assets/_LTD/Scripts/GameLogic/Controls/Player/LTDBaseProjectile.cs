using System;
using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace LTD.GameLogic.Controls
{
    public class LTDBaseProjectile : LTDBaseMono, ILTDBaseProjectile
    {
        private static readonly int Shooting = Animator.StringToHash("Shooting");
        [SerializeField] private float speed;
        private Transform _target;
        private bool _powerUp = false;
        private int _damage = 2;
        
        private void Start()
        {
            LTDEvents.IncreaseSpellPowerUp += PowerUp;
        }

        private void OnDestroy()
        {
            LTDEvents.IncreaseSpellPowerUp -= PowerUp;

        }

        private void PowerUp()
        {
            _powerUp = true;
          
        }

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
                LTDEvents.PlayerShoot.Invoke();
                yield return null;
            }

            Destroy(gameObject);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (!_powerUp)
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                if (_damage == 0)
                {
                    Destroy(gameObject);
                    Destroy(other.gameObject);
                    
                }
                else
                {
                    _damage--;
                    Destroy(other.gameObject);
                }
            }
        }
    }
}