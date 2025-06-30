using BLE.Gamelogic.Providers;
using LTD.Core.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDEnemyFollowPlayer:LTDBaseMono
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private LTDEnemiesProvider enemiesProvider ;
        
        private LTDPlayer _player;
        
        private void Awake()
        {
            _player = FindAnyObjectByType<LTDPlayer>();
            enemiesProvider.AddEnemy(transform);
        }

        private void OnDestroy()
        {
            enemiesProvider.RemoveEnemy(transform);
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