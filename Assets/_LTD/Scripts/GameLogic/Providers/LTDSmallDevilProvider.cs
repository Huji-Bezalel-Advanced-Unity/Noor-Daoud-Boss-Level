using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LTD.GameLogic.BaseMono;
using UnityEngine;
using _LTD.Scripts.GameLogic.Controls;
using LTD.GameLogic.Controls;

namespace BLE.Gamelogic.Providers
{
    public class LTDSmallDevilProvider : LTDBaseMono
    {
        public static LTDSmallDevilProvider Instance { get; private set; }

        [Header("Enemy Settings")]
        [SerializeField] private LTDSmallDevils enemeyPrefab;

        [Header("Spawn Settings")]
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnInterval = 1.5f;

        private List<Transform> _enemies;
        private int _enemyCount = 4;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _enemies = new List<Transform>();
            StartCoroutine(SpawnEnemiesLoop());
        }


        #region Spawning Logic

        private IEnumerator SpawnEnemiesLoop()
        {
            while (true)
            {
                SpawnEnemiesSimultaneously(_enemyCount);
                _enemyCount += 2;
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnEnemiesSimultaneously(int count)
        {
            var spawnPositions = GetTwoClosestSpawnPointsToPlayer();

            int halfCount = count / 2;
            int remaining = count % 2;

            for (int i = 0; i < halfCount; i++)
            {
                var enemy = Instantiate(enemeyPrefab, spawnPositions[0], Quaternion.identity);
                _enemies.Add(enemy.transform);
            }

            for (int i = 0; i < halfCount; i++)
            {
                var enemy = Instantiate(enemeyPrefab, spawnPositions[1], Quaternion.identity);
                _enemies.Add(enemy.transform);
            }

            if (remaining > 0)
            {
                var extra = Instantiate(enemeyPrefab, spawnPositions[0], Quaternion.identity);
                _enemies.Add(extra.transform);
            }
        }

        private Vector3[] GetTwoClosestSpawnPointsToPlayer()
        {
            if (CoreManager.GameManager.Player == null || spawnPoints == null || spawnPoints.Length < 2)
            {
                return new[] { Vector3.zero, Vector3.zero };
            }

            return spawnPoints.OrderBy(point => Vector3.Distance(CoreManager.GameManager.Player.transform.position, point.position))
                .Take(2)
                .Select(p => p.position)
                .ToArray();
        }

        #endregion

        #region Utility

        public Transform GetNearestEnemy(Vector3 position)
        {
            if (_enemies == null || _enemies.Count == 0) return null;

            _enemies = _enemies
                .Where(enemy => enemy != null)
                .ToList();

            return _enemies
                .OrderBy(enemy => Vector3.Distance(position, enemy.position))
                .FirstOrDefault();
        }

        #endregion
    }
}
