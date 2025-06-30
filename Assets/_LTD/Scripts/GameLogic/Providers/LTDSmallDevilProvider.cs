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
        [SerializeField] private float timeBetweenEachSpawn = 2f;

        private List<Transform> _enemies;
        private int _enemyCount = 3;
        private float _spawnInterval = 5f;

        private Transform player;

        private void Awake()
        {
            var playerScript = FindObjectOfType<LTDPlayer>(); // Or your actual player script type
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
                yield return StartCoroutine(SpawnEnemiesOneByOne(_enemyCount));
                _enemyCount += 2;
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private IEnumerator SpawnEnemiesOneByOne(int count)
        {
            Vector3 spawnPos = GetClosestSpawnPointToPlayer();

            for (int i = 0; i < count; i++)
            {
                var enemy = Instantiate(enemeyPrefab, spawnPos, Quaternion.identity);
                _enemies.Add(enemy.transform);
                yield return new WaitForSeconds(timeBetweenEachSpawn);
            }
        }

        private Vector3 GetClosestSpawnPointToPlayer()
        {
            if (player == null || spawnPoints == null || spawnPoints.Length == 0)
                return Vector3.zero;

            return spawnPoints
                .OrderBy(point => Vector3.Distance(player.position, point.position))
                .First()
                .position;
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
