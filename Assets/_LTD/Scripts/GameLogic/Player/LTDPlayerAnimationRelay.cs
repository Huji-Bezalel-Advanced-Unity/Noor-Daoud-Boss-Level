using UnityEngine;

namespace LTD.GameLogic.Player
{
    /// <summary>
    /// Acts as a relay between animation events and gameplay logic.
    /// Specifically, it triggers spell casting from the animator timeline.
    /// </summary>
    public class LTDPlayerAnimationRelay : MonoBehaviour
    {
        /// <summary>
        /// Reference to the player component in the parent hierarchy.
        /// </summary>
        private LTDPlayer _player;
        

        /// <summary>
        /// Finds the LTDPlayer component from the parent on Awake.
        /// </summary>
        private void Awake()
        {
            _player = GetComponentInParent<LTDPlayer>();
        }
        
        #region Animation Events

        /// <summary>
        /// Called from the animation (e.g., via an Animation Event) to trigger the wand's firing logic.
        /// </summary>
        public void ShootSpell()
        {
            _player.Wand?.Fire();
        }

        #endregion
    }
}