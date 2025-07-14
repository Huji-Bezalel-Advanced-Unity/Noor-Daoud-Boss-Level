using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine;

namespace LTD.Core.Enemies
{
    public class LTDFire : LTDBaseMono
    {
        private Rigidbody2D _rigidbody2D;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.gravityScale = 0f;
        }

        public void Shoot(Vector3 direction, float speed)
        {
            _rigidbody2D.linearVelocity = direction * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                LTDEvents.DecreasePlayerHealth?.Invoke(2);
            }
            Destroy(gameObject);
        }

    }
}