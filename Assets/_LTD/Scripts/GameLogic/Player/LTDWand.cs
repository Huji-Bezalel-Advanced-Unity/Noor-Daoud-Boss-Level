using LTD.Core.BaseMono;
using LTD.Core.Managers.AudioManager;
using LTD.Gamelogic.Providers;
using UnityEngine;

namespace LTD.Core.Player
{
    public class LTDWand : LTDBaseMono
    {

        [Header("Projectile Settings")]
        [SerializeField] private LTDSpell spell;

        public void Fire()
        {
            var ltdSpell = Instantiate(spell, transform.position, Quaternion.identity);
            LTDAudioManager.Instance.PlaySFX(LTDAudioManager.AudioClipType.SpellCast);

            Vector3 playerForward =  CoreManager.GameManager.Player.Animator.transform.localScale.x > 0 ? Vector3.right : Vector3.left;
            Transform target = LTDSmallDevilProvider.Instance.GetNearestEnemy(CoreManager.GameManager.Player.transform.position,
                playerForward, 160);
            if (target != null)
            {
                ltdSpell.FlyTowardsEnemy(target);
            }
            else
            {
                Destroy(ltdSpell.gameObject); 
            }
        }

    }
}