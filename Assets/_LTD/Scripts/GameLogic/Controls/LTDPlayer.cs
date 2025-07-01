using System;
using _LTD.Scripts.GameLogic;
using LTD.Core.BaseMono;
using UnityEngine;

namespace LTD.GameLogic.Controls
{
    public class LTDPlayer : LTDBaseMono
    {
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Shooting = Animator.StringToHash("Shooting");

        [Header("Settings")] 
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private Animator animator;
        [Header("References")]
        [SerializeField] private LTDWand wand; 
        private Vector3 _direction;
        

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            _direction = new Vector3(horizontal, vertical, 0).normalized;

            transform.position += _direction * (Time.deltaTime * moveSpeed);

            bool isMoving = _direction.magnitude > 0.01f;
            if (isMoving)
            {
                animator.SetBool(Walk, true);
            }
            else
            {
                animator.SetBool(Walk, false);
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wand?.Fire();
            }
        }
    }
}