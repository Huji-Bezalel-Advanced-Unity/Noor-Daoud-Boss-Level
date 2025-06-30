using BLE.Gamelogic.Providers;
using LTD.Core.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDSmallDevils:LTDBaseMono
    {
        [SerializeField] private float speed = 5f;
        private LTDPlayer _player;
        
        private void Awake()
        {
            _player = FindAnyObjectByType<LTDPlayer>();
        }
        
        private void Update()
        {
            if (_player == null)
            {
                return;
            }
            transform.up = _player.transform.position - transform.position;
            transform.position += transform.up * speed * Time.deltaTime;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PlayerBullet"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }
}