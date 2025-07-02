using System;
using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace LTD.GameLogic.Controls
{
    public class LTDBaseProjectile : LTDBaseMono
    {
        [Header("Projectile Settings")]
        [SerializeField] private float speed;

        private Vector3 _moveDirection;

        public void LaunchInDirection(Vector3 direction)
        {
            _moveDirection = direction.normalized;
            transform.up = _moveDirection;
        }

        private void Update()
        {
            if (_moveDirection != Vector3.zero)
            {
                transform.position += _moveDirection * speed * Time.deltaTime;
                LTDEvents.PlayerShoot?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}