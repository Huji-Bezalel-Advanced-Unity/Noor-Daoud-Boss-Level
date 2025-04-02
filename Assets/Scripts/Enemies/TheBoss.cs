using System;
using DefaultNamespace;
using Pool;
using UnityEngine;

namespace Enemies
{
    public class TheBoss:MonoBehaviour
    {
        [SerializeField] private Transform target;
        private float _shootCooldown = 0.5f; // Cooldown time in seconds
        private float _lastShootTime;
        private bool _InTheRedZone = true;
        private void Update()
        {
            if (_InTheRedZone)
            {
                HandleShooting();
            }
        }
        
        private void HandleShooting()
        {
            if (Time.time >= _lastShootTime + _shootCooldown)
            {
                var bullet = BulletPool.Instance.Get(); // Retrieve bullet from object pool

                if (bullet != null)
                {
                    bullet.transform.position = transform.position; // Set bullet's position to the boss's position
                    Vector3 direction = (target.position - transform.position).normalized; // Calculate direction to target
                    bullet.Shoot(direction, "Bullet", 10f, "BossBullet"); // Shoot towards the moving target
                    _lastShootTime = Time.time; // Update last shoot time
                }
            }
        }

        private void OnEnable()
        {
            Events.SafeZone += StopShooting;
            Events.RedZone += KeepShooting;
        }

        private void OnDisable()
        {
            Events.SafeZone -= StopShooting;
            Events.RedZone -= KeepShooting;

        }

        private void KeepShooting()
        {
            _InTheRedZone = true;
        }

        private void StopShooting()
        {
            _InTheRedZone = false;
        }
    }
}