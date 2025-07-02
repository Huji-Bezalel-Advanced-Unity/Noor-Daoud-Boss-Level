using System;
using LTD.Core.BaseMono;
using LTD.Core.Managers;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDFire:LTDBaseMono
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

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                LTDEvents.DecreasePlayerHealth.Invoke();
            }
            Destroy(gameObject);
        }
    }
}