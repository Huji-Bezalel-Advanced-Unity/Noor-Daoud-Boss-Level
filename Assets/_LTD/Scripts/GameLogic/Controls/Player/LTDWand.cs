using BLE.Gamelogic.Providers;
using LTD.GameLogic.BaseMono;
using LTD.Gamelogic.Controls;
using LTD.GameLogic.Controls;
using UnityEngine;
using UnityEngine.Serialization;

namespace _LTD.Scripts.GameLogic
{
    public class LTDWand : LTDBaseMono
    {

        [Header("Projectile Settings")]
        [SerializeField] private LTDBaseProjectile spell;
        [FormerlySerializedAs("enemiesProvider")]
        [SerializeField] private LTDSmallDevilProvider smallDevilProvider;
        
        public void Fire()
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