using System.Collections;
using _LTD.Scripts.GameLogic.Controls;
using LTD.Core.BaseMono;
using LTD.Core.Managers;
using LTD.Core.Utils;
using UnityEngine;

namespace LTD.Gamelogic.Controls
{
    public class LTDBoss : LTDBaseMono
    {
        [Header("References")]
        [SerializeField] private Transform target;
        [SerializeField] private LTDFire firePrefab;

        private float _shootCooldown = 0.5f;
        private float _lastShootTime;

        private Coroutine _shootingCoroutine;
        private Coroutine _safeZoneDelayCoroutine;

        private void Awake()
        {
            Events.SafeZone += OnEnterSafeZone;
            Events.RedZone += OnExitSafeZone;
        }

        private void Start()
        {
            StartShooting();
        }

        private void OnDestroy()
        {
            Events.SafeZone -= OnEnterSafeZone;
            Events.RedZone -= OnExitSafeZone;
        }

        #region Safe Zone

        private void OnEnterSafeZone()
        {
            StopShooting();

            if (_safeZoneDelayCoroutine != null)
                StopCoroutine(_safeZoneDelayCoroutine);

            _safeZoneDelayCoroutine = StartCoroutine(ResumeShootingAfterDelay(5f));
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

        #endregion

        #region Shooting Logic

        private void StartShooting()
        {
            if (_shootingCoroutine == null)
                _shootingCoroutine = StartCoroutine(HandleShootingCoroutine());
        }

        private void StopShooting()
        {
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
                _shootingCoroutine = null;
            }
        }

        private IEnumerator ResumeShootingAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _safeZoneDelayCoroutine = null;
            StartShooting(); 
        }

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

        private IEnumerator HandleShooting()
        {
            var fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            if (fire != null)
            {
                fire.transform.position = transform.position;
                Vector3 direction = (target.position - transform.position).normalized;

                fire.Shoot(direction, 20f);
                _lastShootTime = Time.time;

                yield return new WaitForSeconds(_shootCooldown);
            }
        }

        #endregion
    }
}
