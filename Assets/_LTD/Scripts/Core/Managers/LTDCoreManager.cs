using System;

namespace LTD.GameLogic.Managers
{
    /// <summary>
    /// The core manager responsible for initializing and holding references to core game systems such as the LTDGameManager.
    /// Implements a singleton pattern for global access.
    /// </summary>
    public class LTDCoreManager
    {
        /// <summary>
        /// Static instance of the LTDCoreManager for global access.
        /// </summary>
        public static LTDCoreManager Instance { get; private set; }
        
        /// <summary>
        /// Reference to the main game logic manager.
        /// </summary>
        public LTDGameManager GameManager;

        
        /// <summary>
        /// Constructor that sets this instance as the singleton.
        /// </summary>
        public LTDCoreManager()
        {
            Instance = this;
        }
        
        #region Core Manager Loading

        /// <summary>
        /// Loads all core managers used in the game.
        /// Currently only initializes the LTDGameManager.
        /// </summary>
        /// <param name="onComplete">Callback invoked after managers are loaded.</param>
        public void LoadManagers(Action onComplete)
        {
            GameManager = new();
            onComplete?.Invoke();
        }
        #endregion
    }
}