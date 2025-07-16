using System.Collections;
using System.Collections.Generic;
using LTD.GameLogic.Utils;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Managers;
using UnityEngine;

namespace LTD.GameLogic.Enemies
{
    /// <summary>
    /// Controls the behavior of the boss enemy, including shooting fireballs in patterns,
    /// reacting to safe zones, and cleaning up projectiles.
    /// </summary>
    public class LTDBoss : LTDBaseMono
    {
        [Header("Fireball Settings")]
        [Tooltip("Prefab for the fireball projectiles.")]
        [SerializeField] private LTDFire firePrefab;

        /// <summary>
        /// List of all active fireball GameObjects spawned by the boss.
        /// Used for cleanup during restart or scene unload.
        /// </summary>
        private readonly List<GameObject> _spawnedFireballs = new List<GameObject>();

        /// <summary>
        /// Time in seconds between each fireball attack cycle.
        /// </summary>
        private float _shootCooldown = 0.5f;

        private float _lastShootTime;
        private Coroutine _shootingCoroutine;
        private Coroutine _safeZoneDelayCoroutine;
        

        private void Awake()
        {
            LTDEvents.SafeZone += OnEnterSafeZone;
            LTDEvents.RedZone += OnExitSafeZone;
        }

        private void Start()
        {
            StartShooting();
        }

        private void OnDestroy()
        {
            LTDEvents.SafeZone -= OnEnterSafeZone;
            LTDEvents.RedZone -= OnExitSafeZone;
        }
        
        #region Safe Zone Logic

        /// <summary>
        /// Stops shooting immediately and starts a delayed resume coroutine.
        /// </summary>
        private void OnEnterSafeZone()
        {
            StopShooting();
            this.StopAndStartCoroutine(ref _safeZoneDelayCoroutine, ResumeShootingAfterDelay(3f));
        }

        /// <summary>
        /// Cancels any safe zone delay and resumes shooting immediately.
        /// </summary>
        private void OnExitSafeZone()
        {
            if (_safeZoneDelayCoroutine != null)
            {
                StopCoroutine(_safeZoneDelayCoroutine);
                _safeZoneDelayCoroutine = null;
            }

            StartShooting();
        }

        /// <summary>
        /// Waits for a delay before resuming shooting, used during safe zone entry.
        /// </summary>
        private IEnumerator ResumeShootingAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _safeZoneDelayCoroutine = null;
            StartShooting();
        }

        #endregion

        #region Shooting Logic

        /// <summary>
        /// Starts the continuous shooting coroutine.
        /// </summary>
        private void StartShooting()
        {
            this.StopAndStartCoroutine(ref _shootingCoroutine, HandleShootingCoroutine());
        }

        /// <summary>
        /// Stops the shooting coroutine safely.
        /// </summary>
        private void StopShooting()
        {
            this.StopWithNullifyCoroutine(ref _shootingCoroutine);
        }

        /// <summary>
        /// Coroutine that handles the repeated shooting based on cooldown.
        /// </summary>
        private IEnumerator HandleShootingCoroutine()
        {
            while (true)
            {
                if (Time.time >= _lastShootTime + _shootCooldown)
                {
                    yield return HandleShooting();
                }

                yield return null;
            }
        }

        /// <summary>
        /// Fires multiple projectiles in a circular spread pattern towards the player.
        /// </summary>
        private IEnumerator HandleShooting()
        {
            _lastShootTime = Time.time;

            int horizontalAmount = 6;
            float angleStep = 360f / horizontalAmount;
            int centerIndex = horizontalAmount / 2;

            for (int i = 0; i < horizontalAmount; i++)
            {
                var fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
                if (fire != null)
                {
                    _spawnedFireballs.Add(fire.gameObject);
                    fire.transform.position = transform.position;

                    Vector3 baseDirection = (CoreManager.GameManager.Player.transform.position - transform.position).normalized;

                    int offsetFromCenter = i - centerIndex;
                    float spreadAngle = offsetFromCenter * angleStep;
                    Vector3 rotatedDirection = Quaternion.Euler(0, 0, spreadAngle) * baseDirection;
                    float angle = Mathf.Atan2(rotatedDirection.y, rotatedDirection.x) * Mathf.Rad2Deg;

                    fire.transform.rotation = Quaternion.Euler(0, 0, angle);
                    float projectileSpeed = 5;
                    fire.Shoot(rotatedDirection, projectileSpeed);
                }

                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(_shootCooldown);
        }

        /// <summary>
        /// Destroys all active fireballs spawned by the boss and clears the internal list.
        /// </summary>
        public void DestroyAllFireballs()
        {
            foreach (var fireball in _spawnedFireballs)
            {
                if (fireball != null)
                    Destroy(fireball);
            }

            _spawnedFireballs.Clear();
        }

        #endregion
    }
}
