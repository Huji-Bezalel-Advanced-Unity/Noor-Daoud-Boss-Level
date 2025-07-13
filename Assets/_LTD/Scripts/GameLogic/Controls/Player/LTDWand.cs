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
        [SerializeField] private LTDSpell spell;

        public void Fire()
        {
            var projectile = Instantiate(spell, transform.position, Quaternion.identity);
            
            Vector3 playerForward =  CoreManager.GameManager.Player.Animator.transform.localScale.x > 0 ? Vector3.right : Vector3.left;
            Transform target = LTDSmallDevilProvider.Instance.GetNearestEnemy(CoreManager.GameManager.Player.transform.position,
                playerForward, 90f);
            if (target != null)
            {
                projectile.FlyTowardsEnemy(target);
            }
            else
            {
                Destroy(projectile.gameObject); 
            }
        }

    }
}