using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;

namespace _LTD.Scripts.GameLogic
{
    public class LTDWand : LTDBaseMono
    {
        [Header("Projectile Settings")]
        [SerializeField] private LTDBaseProjectile spell;

        public void Fire(Vector3 direction)
        {
            var projectile = Instantiate(spell, transform.position, Quaternion.identity);
            projectile.LaunchInDirection(direction);
        }
    }
}