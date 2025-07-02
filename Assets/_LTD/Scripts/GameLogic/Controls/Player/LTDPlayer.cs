using System;
using _LTD.Scripts.GameLogic;
using LTD.GameLogic.BaseMono;
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
        [SerializeField] private LTDWand wand;
        [field: SerializeField] public Animator Animator { get; private set; }

        private Vector3 _direction;
        private float _lastHorizontal = 1f;

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
            float horizontal = 0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                horizontal = -1f;
            else if (Input.GetKey(KeyCode.RightArrow))
                horizontal = 1f;

            float vertical = Input.GetAxisRaw("Vertical");

            _direction = new Vector3(horizontal, vertical, 0).normalized;
            transform.position += _direction * (Time.deltaTime * moveSpeed);

            // Flip character based on horizontal direction
            if (horizontal != 0 && horizontal != _lastHorizontal)
            {
                Vector3 scale = Animator.transform.localScale;
                scale.x = Mathf.Abs(scale.x) * (horizontal > 0 ? 1 : -1);
                Animator.transform.localScale = scale;
                _lastHorizontal = horizontal;
            }

            Animator.SetBool(Walk, _direction.magnitude > 0.01f);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                wand?.Fire();
            }
        }

        #endregion

        #region Event Handlers

        private void IncreaseSpeed()
        {
            moveSpeed += 3f;
            Debug.Log("Increased speed to " + moveSpeed);
        }

        private void ShootAnimation()
        {
            Animator.SetTrigger(Shooting);
        }

        #endregion
    }
}
