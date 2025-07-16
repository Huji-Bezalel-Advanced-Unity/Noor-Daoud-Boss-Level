using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Enemies;
using UnityEngine;

namespace LTD.Gamelogic.Providers
{
    /// <summary>
    /// Responsible for spawning and tracking small devil enemies in the scene.
    /// Also provides utility to find the nearest valid enemy based on angle and distance.
    /// </summary>
    public class LTDSmallDevilProvider : LTDBaseMono
    {
        /// <summary>
        /// Singleton instance for global access.
        /// </summary>
        public static LTDSmallDevilProvider Instance { get; private set; }
        

        [Header("Spawn Settings")]
        [Tooltip("Spawn points where small devils will be instantiated.")]
        [SerializeField] private Transform[] spawnPoints;

        [Tooltip("Prefab to spawn for small devils.")]
        [SerializeField] private LTDSmallDevils smallDevilsPrefab;

        [Tooltip("Center point used for calculating radial spawn directions.")]
        [SerializeField] private Transform devilCenter;

        private float _spawnInterval = 2f;
        private List<Transform> _enemies;
        
        private void Start()
        {
            Instance = this;
            _enemies = new List<Transform>();
            StartCoroutine(SpawnEnemiesLoop());
        }
        
        #region Spawning Logic

        /// <summary>
        /// Continuously spawns enemies at random intervals in a radial pattern.
        /// </summary>
        private IEnumerator SpawnEnemiesLoop()
        {
            while (true)
            {
                SpawnRadialEnemies();
                yield return new WaitForSeconds(Random.Range(_spawnInterval + 1, _spawnInterval - 1));
            }
        }

        /// <summary>
        /// Spawns one enemy at each spawn point, setting their movement direction away from the devil center.
        /// </summary>
        private void SpawnRadialEnemies()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                Vector3 direction = (spawnPoint.position - devilCenter.position).normalized;

                var enemy = Instantiate(smallDevilsPrefab, spawnPoint.position, Quaternion.identity);
                enemy.SetDirection(direction);
                _enemies.Add(enemy.transform);
            }
        }

        #endregion

        #region Utility

        /// <summary>
        /// Returns the nearest enemy that lies within a given angle in front of the player.
        /// </summary>
        /// <param name="playerPosition">Player's world position.</param>
        /// <param name="playerForward">The direction the player is facing.</param>
        /// <param name="maxAngleDegrees">The maximum cone angle in degrees to consider.</param>
        /// <returns>The nearest enemy Transform or null if none found.</returns>
        public Transform GetNearestEnemy(Vector3 playerPosition, Vector3 playerForward, float maxAngleDegrees)
        {
            if (_enemies == null || _enemies.Count == 0) return null;
            
            _enemies = _enemies.Where(e => e != null).ToList();

            Transform nearest = null;
            float closestDistance = float.MaxValue;

            foreach (var enemy in _enemies)
            {
                Vector3 toEnemy = (enemy.position - playerPosition).normalized;
                float angle = Vector3.Angle(playerForward, toEnemy);

                if (angle <= maxAngleDegrees)
                {
                    float distance = Vector3.Distance(playerPosition, enemy.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearest = enemy;
                    }
                }
            }

            return nearest;
        }

        #endregion
    }
}
