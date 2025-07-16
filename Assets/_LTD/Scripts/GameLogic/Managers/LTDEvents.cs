using System;

namespace LTD.GameLogic.Managers
{
    /// <summary>
    /// Central event hub for broadcasting gameplay-related events.
    /// Allows decoupled communication between systems such as UI, audio, player, and enemy logic.
    /// </summary>
    public static class LTDEvents
    {
        #region Player Events

        /// <summary>
        /// Invoked when the player dies.
        /// </summary>
        public static Action PlayerDies;

        /// <summary>
        /// Invoked to decrease the player's health by a specified amount.
        /// </summary>
        public static Action<int> DecreasePlayerHealth;

        /// <summary>
        /// Invoked to increase the player's health.
        /// </summary>
        public static Action IncreasePlayerHealth;

        /// <summary>
        /// Invoked when the player shoots a projectile or attack.
        /// </summary>
        public static Action PlayerShoot;

        /// <summary>
        /// Invoked to increase the player's movement speed.
        /// </summary>
        public static Action IncreasePlayerSpeed;

        #endregion

        #region Enemy Events

        /// <summary>
        /// Invoked to decrease the devil boss's health.
        /// </summary>
        public static Action DecreaseDevilHealth;

        /// <summary>
        /// Invoked when the devil boss dies.
        /// </summary>
        public static Action DevilDies;

        #endregion

        #region Zone Events

        /// <summary>
        /// Invoked when the player enters a safe zone.
        /// </summary>
        public static Action SafeZone;

        /// <summary>
        /// Invoked when the player exits a safe zone and enters a danger zone.
        /// </summary>
        public static Action RedZone;

        #endregion
    }
}