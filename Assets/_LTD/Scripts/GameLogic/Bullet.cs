using System;
using Pool;
using UnityEngine;

namespace Pool
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [Header("Settings")]
        [SerializeField] private float speed = 10f; // Speed of the bullet
        private Rigidbody2D _rigidbody2D;
        private bool _isFired;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Shoot(Vector3 direction, string tag, float speed, string bulletLayer)
        {
            _rigidbody2D.linearVelocity = direction * speed; // Correct usage of velocity
            gameObject.tag = tag;
            gameObject.layer = LayerMask.NameToLayer(bulletLayer);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            BulletPool.Instance.Return(this);
            _isFired = false;

            //TODO Remove Health from player?
        }


        public void SetPoolable()
        {
            _isFired = true;
            _rigidbody2D.linearVelocity = Vector2.zero; // Reset velocity to avoid leftover movement
            transform.position = Vector2.zero;
            gameObject.tag = "Bullet";
            // gameObject.layer = LayerMask.NameToLayer("Fight");
        }
    }
}