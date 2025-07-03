using System.Collections;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Controls
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
            LTDEvents.DecreasePlayerHealth?.Invoke();
            animator.SetBool("Die", true);
           // LTDAudioManager.Instance.PlaySFX(LTDAudioManager.Instance.spellCastSFX);
            StartCoroutine(WaitAndDestroy());
        }

        private IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }

}