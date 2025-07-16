using UnityEngine;

namespace LTD.GameLogic.Player
{
    /// <summary>
    /// Interface for spell behaviors in the game.
    /// Implement this interface to define how a spell moves toward its target.
    /// </summary>
    public interface ILTDSpell
    {
        /// <summary>
        /// Commands the spell to move toward a given enemy target.
        /// </summary>
        /// <param name="target">The transform of the enemy target.</param>
        void FlyTowardsEnemy(Transform target);
    }
}