using BLE.Gamelogic.Providers;
using LTD.Core.BaseMono;
using LTD.Gamelogic.Controls;
using LTD.GameLogic.Controls;
using UnityEngine;
using UnityEngine.Serialization;

namespace _LTD.Scripts.GameLogic
{
    public class LTDWand:LTDBaseMono
    {
        [SerializeField] private float fireRate;
        [SerializeField] private LTDBaseProjectile spell;
        [FormerlySerializedAs("enemiesProvider")] [SerializeField] private LTDSmallDevilProvider smallDevilProvider;
        
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
            
            var enemy = smallDevilProvider.GetNearestEnemy(transform.position);

            if (enemy == null)
            {
                   Destroy(projectile.gameObject);
                   return;
            }
            projectile.FlyTowardsEnemy(enemy);
        }
        
    }
}