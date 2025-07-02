using System;
using _LTD.Scripts.GameLogic;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace LTD.GameLogic.Controls
{
    public class LTDPlayer : LTDBaseMono
    {
        #region Animator Hashes

        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Shooting = Animator.StringToHash("Shooting");

        #endregion
        
        [Header("Settings")]
        [SerializeField] private float moveSpeed = 5f;

        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private LTDWand wand;
        // [SerializeField] private AudioClip spellClip;
        
        private Vector3 _direction;

        private void Start()
        {
            LTDEvents.PlayerShoot += ShootAnimation;
            LTDEvents.IncreasePlayerSpeed += IncreaseSpeed;
        }

        private void OnDestroy()
        {
            LTDEvents.PlayerShoot -= ShootAnimation;
            LTDEvents.IncreasePlayerSpeed -= IncreaseSpeed;
        }

        private void Update()
        {
            CheckInput();
        }


        #region Input Handling

        private void CheckInput()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            _direction = new Vector3(horizontal, vertical, 0).normalized;
            transform.position += _direction * (Time.deltaTime * moveSpeed);

            bool isMoving = _direction.magnitude > 0.01f;
            animator.SetBool(Walk, isMoving);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                wand?.Fire();
                // LTDAudioManager.Instance.PlaySFX(spellClip);
            }
        }

        #endregion

        #region Event Handlers

        private void IncreaseSpeed()
        {
            moveSpeed += 3;
            print("IncreaseSpeed");
            print(moveSpeed);
        }

        private void ShootAnimation()
        {
            animator.SetTrigger(Shooting);
        }

        #endregion
    }
}
