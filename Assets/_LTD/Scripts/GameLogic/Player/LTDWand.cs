using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using LTD.Gamelogic.Providers;
using UnityEngine;

namespace LTD.GameLogic.Player
{
    /// <summary>
    /// Handles the logic for casting a spell from the player's wand.
    /// Responsible for instantiating the spell and determining its target.
    /// </summary>
    public class LTDWand : LTDBaseMono
    {

        [Header("Projectile Settings")]
        [Tooltip("The spell prefab to instantiate when casting.")]
        [SerializeField] private LTDSpell spell;
        
        /// <summary>
        /// Fires a spell from the wand. Attempts to locate the nearest enemy within range and sends the spell toward it.
        /// If no target is found, the spell is destroyed immediately.
        /// </summary>
        public void Fire()
        {
            var ltdSpell = Instantiate(spell, transform.position, Quaternion.identity);
            LTDAudioManager.Instance.PlaySFX(LTDAudioManager.AudioClipType.SpellCast);
            Vector3 playerForward = CoreManager.GameManager.Player.Animator.transform.localScale.x > 0 
                ? Vector3.right 
                : Vector3.left;
            Transform target = LTDSmallDevilProvider.Instance.GetNearestEnemy(
                CoreManager.GameManager.Player.transform.position,
                playerForward,
                160f
            );
            
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
