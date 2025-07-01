using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LTD.Core.BaseMono;
using UnityEngine;
using _LTD.Scripts.GameLogic.Controls;
using LTD.GameLogic.Controls;

namespace BLE.Gamelogic.Providers
{
    public class LTDSmallDevilProvider : LTDBaseMono
    {
        [SerializeField] private LTDSmallDevils enemeyPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnInterval = 1.5f;

        private List<Transform> _enemies;
        private int _enemyCount = 4;
        private Transform player;

        private void Awake()
        {
            var playerScript = FindObjectOfType<LTDPlayer>();
            if (playerScript != null)
            {
                player = playerScript.transform;
            }
        }

        private void Start()
        {
            _enemies = new List<Transform>();
            StartCoroutine(SpawnEnemiesLoop());
        }

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
            if (player == null || spawnPoints == null || spawnPoints.Length < 2)
                return new[] { Vector3.zero, Vector3.zero };

            return spawnPoints
                .OrderBy(point => Vector3.Distance(player.position, point.position))
                .Take(2)
                .Select(p => p.position)
                .ToArray();
        }

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
    }
}
