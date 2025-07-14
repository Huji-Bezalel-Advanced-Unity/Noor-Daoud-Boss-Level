using System.Collections;
using LTD.Core.BaseMono;
using LTD.Core.Utils;
using LTD.Core.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace LTD.Core.Enemies
{
    public class LTDBoss : LTDBaseMono
    {

        [Header("Target & Projectile")]
        [SerializeField] private LTDFire firePrefab;

        [FormerlySerializedAs("_shootCooldown")]
        // [Header("Shooting Settings")]
        private float shootCooldown = 1;

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

        private void OnEnterSafeZone()
        {
            StopShooting();

            this.StopAndStartCoroutine(ref _safeZoneDelayCoroutine, ResumeShootingAfterDelay(3f));
        }

        private void OnExitSafeZone()
        {
            if (_safeZoneDelayCoroutine != null)
            {
                StopCoroutine(_safeZoneDelayCoroutine);
                _safeZoneDelayCoroutine = null;
            }

            StartShooting();
        }

        private IEnumerator ResumeShootingAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _safeZoneDelayCoroutine = null;
            StartShooting();
        }

        #endregion

        #region Shooting Logic

        private void StartShooting()
        {
            this.StopAndStartCoroutine(ref _shootingCoroutine, HandleShootingCoroutine());
        }

        private void StopShooting()
        {
            this.StopWithNullifyCoroutine(ref _shootingCoroutine);
        }

        private IEnumerator HandleShootingCoroutine()
        {
            while (true)
            {
                if (Time.time >= _lastShootTime + shootCooldown)
                {
                    yield return HandleShooting();
                }

                yield return null;
            }
        }

        // private IEnumerator HandleShooting()
        // {
        //     var fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
        //     if (fire != null)
        //     {
        //         fire.transform.position = transform.position;
        //         Vector3 direction = (CoreManager.GameManager.Player.transform.position - transform.position).normalized;
        //         fire.Shoot(direction, 30f);

        //         _lastShootTime = Time.time;

        //         yield return new WaitForSeconds(shootCooldown);
        //     }
        // }

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

            yield return new WaitForSeconds(shootCooldown);

        }

        #endregion
    }
}
