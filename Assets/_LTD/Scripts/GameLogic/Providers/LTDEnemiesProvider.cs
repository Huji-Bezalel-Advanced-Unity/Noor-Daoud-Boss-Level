using System.Collections.Generic;
using LTD.Core.BaseMono;
using UnityEngine;
using System.Linq;

namespace BLE.Gamelogic.Providers
{
    public class LTDEnemiesProvider:LTDBaseMono
    {
        [SerializeField] private List<Transform> enemies;
        
        public Transform GetRandomEnemy()
        {
            return enemies[Random.Range(0, enemies.Count)];
        }
        
        public Transform GetNearestEnemy(Vector3 position)
        {
            if (enemies == null)
            {
                return null;
            }
            
            if (enemies.Count == 0)
            {
                return null;
            }
            
            enemies = enemies
                .Where(enemy => enemy != null)
                .ToList();
            
            return enemies
                .OrderBy(enemy => Vector3.Distance(position, enemy.position))
                .FirstOrDefault();
        }
        
        public void AddEnemy(Transform enemy)
        {
            if (enemies == null)
            {
                enemies = new List<Transform>();
            }
            
            if (!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }
        }
        
        public void RemoveEnemy(Transform enemy)
        {
            if (enemies == null)
            {
                return;
            }
            
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }
    }
