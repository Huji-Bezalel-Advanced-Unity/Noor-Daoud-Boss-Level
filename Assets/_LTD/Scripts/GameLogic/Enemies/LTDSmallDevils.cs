using System.Collections;
using LTD.Core.Managers;
using UnityEngine;

namespace LTD.Core.Enemies
{
    public class LTDSmallDevils : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private Animator animator;
        
        private Vector2 _moveDirection;
        private Rigidbody2D _rb;

        public void SetDirection(Vector2 direction)
        {
            _moveDirection = direction.normalized;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = _moveDirection * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                LTDEvents.DecreasePlayerHealth?.Invoke(3);
            }
            animator.SetBool("Die", true);
           // LTDAudioManager.Instance.PlaySFX(LTDAudioManager.Instance.spellCastSFX);
            StartCoroutine(WaitAndDestroy());
        }

        private IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(0.19f);
            Destroy(gameObject);
        }
    }

}