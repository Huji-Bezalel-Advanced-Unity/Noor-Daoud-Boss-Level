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

        [Header("Spawn Settings")]
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private LTDSmallDevils smallDevilsPrefab;
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

        private IEnumerator SpawnEnemiesLoop()
        {
            while (true)
            {
                SpawnRadialEnemies();
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private void SpawnRadialEnemies()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                var spawnPoint = spawnPoints[i];
                Vector3 direction;
               
                if (i < 3)
                {
                    direction = (spawnPoint.position - devilCenter.position).normalized;
                }
                else
                {
                    direction = (devilCenter.position + spawnPoint.position).normalized;
                }
                if (i == 3)
                {
                    direction = Vector3.left.normalized;

                }
                if (i == 4)
                {
                    direction = Vector3.up.normalized;

                }
                var enemy = Instantiate(smallDevilsPrefab, spawnPoint.position, Quaternion.identity);
                enemy.SetDirection(direction);

                _enemies.Add(enemy.transform);
            }
        }



        #endregion

        #region Utility

        public Transform GetNearestEnemy(Vector3 playerPosition, Vector3 playerForward, float maxAngleDegrees = 90f)
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
