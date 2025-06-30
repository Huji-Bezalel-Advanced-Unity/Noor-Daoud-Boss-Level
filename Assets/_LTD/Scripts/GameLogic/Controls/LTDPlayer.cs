using _LTD.Scripts.GameLogic;
using LTD.Core.BaseMono;
using UnityEngine;

namespace LTD.GameLogic.Controls
{

    public class LTDPlayer : LTDBaseMono
    {
        [Header("Settings")] [SerializeField] private float moveSpeed = 5f;
        private Vector3 _direction;
        
        public void Update()
        {
            CheckInput();
        }

        
        private void CheckInput()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            _direction = new Vector3(horizontal, vertical, 0).normalized;

            transform.position += _direction * (Time.deltaTime * moveSpeed);
        }

    }
}