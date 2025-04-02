using System;
using Pool;
using UnityEngine;

namespace Pool
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] private float speed = 10f; // Speed of the bullet
        private Rigidbody2D _rigidbody2D;
        private bool _isFired;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Shoot(Vector3 direction, String tag, float speed, String bulletLayer)
        {
            _rigidbody2D.linearVelocity = direction * speed; // Correct usage of velocity
            gameObject.tag = tag;
            gameObject.layer = LayerMask.NameToLayer(bulletLayer); 
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            BulletPool.Instance.Return(this);
            _isFired = false;
        }

        public void Reset()
        {
            _isFired = true;
            _rigidbody2D.linearVelocity = Vector2.zero; // Reset velocity to avoid leftover movement
            transform.position = Vector2.zero;
            gameObject.tag = "Bullet";
            gameObject.layer = LayerMask.NameToLayer("Fight");
        }
    }
}