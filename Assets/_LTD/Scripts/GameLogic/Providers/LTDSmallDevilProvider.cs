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
        [SerializeField] private float spawnInterval = 0.5f;

        private List<Transform> _enemies;
        private int _rowsToSpawn = 1;

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
                SpawnEnemiesInRows(8); 
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnEnemiesInRows(int numberOfRows)
        {
            for (int row = 0; row < numberOfRows; row++)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    var enemy = Instantiate(enemeyPrefab, spawnPoint.position, Quaternion.identity);
                    _enemies.Add(enemy.transform);
                }
            }
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
