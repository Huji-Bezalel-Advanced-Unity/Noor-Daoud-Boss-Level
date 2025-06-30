using BLE.Gamelogic.Providers;
using LTD.Core.BaseMono;
using LTD.Gamelogic.Controls;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace _LTD.Scripts.GameLogic
{
    public class LTDWand:LTDBaseMono
    {
        [SerializeField] private float fireRate;
        [SerializeField] private LTDBaseProjectile spell;
        [SerializeField] private LTDEnemiesProvider enemiesProvider;
        
        private float _timer;
        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= fireRate )
            {
                Fire();
                _timer = 0;
            }   
        }

        private void Fire()
        {
            var projectile = Instantiate(spell, transform.position, Quaternion.identity);
            
            var enemy = enemiesProvider.GetNearestEnemy(transform.position);

            if (enemy == null)
            {
                   Destroy(projectile.gameObject);
                   return;
            }
            projectile.FlyTowardsEnemy(enemy);
        }
        
    }
}