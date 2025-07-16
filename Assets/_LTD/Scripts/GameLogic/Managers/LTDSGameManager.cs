using LTD.GameLogic.Enemies;
using LTD.GameLogic.Player;
using UnityEngine;

namespace LTD.GameLogic.Managers
{
    /// <summary>
    /// Manages the core gameplay entities such as the player and the boss.
    /// Responsible for instantiating prefabs and tracking references to active instances.
    /// </summary>
    public class LTDGameManager
    {
        #region Prefabs and Positions

        private readonly Vector3 _spawnPosition = new Vector3(1f, -11f, 0.244f);
        private readonly Vector3 _bossSpawnPosition = new Vector3(2f, 0f, 0.244f);

        private LTDPlayer _playerPrefab;
        private LTDBoss _bossPrefab;

        #endregion

        #region Public Instances

        /// <summary>
        /// The currently active player instance in the scene.
        /// </summary>
        public LTDPlayer Player { get; private set; }

        /// <summary>
        /// The currently active boss instance in the scene.
        /// </summary>
        public LTDBoss Boss { get; private set; }

        #endregion

        #region Constructor & Initialization

        /// <summary>
        /// Loads player and boss prefabs from the Resources folder.
        /// </summary>
        public LTDGameManager()
        {
            _playerPrefab = Resources.Load<LTDPlayer>("PlayerPrefab");
            _bossPrefab = Resources.Load<LTDBoss>("BossPrefab");
        }

        #endregion

        #region Instantiation Methods

        /// <summary>
        /// Instantiates the player prefab at a predefined spawn position.
        /// </summary>
        public void InstantiatePlayer()
        {
            Player = Object.Instantiate(_playerPrefab, _spawnPosition, Quaternion.identity);
        }

        /// <summary>
        /// Instantiates the boss prefab at a predefined spawn position.
        /// </summary>
        public void InstantiateBoss()
        {
            Boss = Object.Instantiate(_bossPrefab);
            Boss.transform.position = _bossSpawnPosition;
        }

        #endregion
    }
}
