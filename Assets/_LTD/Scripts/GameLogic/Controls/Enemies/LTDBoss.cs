using System.Collections;
using _LTD.Scripts.GameLogic.Controls;
using LTD.GameLogic.BaseMono;
using LTD.GameLogic.Controls;
using UnityEngine;
using UnityEngine.Serialization;

namespace LTD.Gamelogic.Controls
{
    public class LTDBoss : LTDBaseMono
    {

        [Header("Target & Projectile")]
        [SerializeField] private Transform target;
        [SerializeField] private LTDFire firePrefab;

        [FormerlySerializedAs("_shootCooldown")]
        [Header("Shooting Settings")]
        [SerializeField] private float shootCooldown = 0.3f;
        
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

            if (_safeZoneDelayCoroutine != null)
                StopCoroutine(_safeZoneDelayCoroutine);

            _safeZoneDelayCoroutine = StartCoroutine(ResumeShootingAfterDelay(3f));
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

        private IEnumerator HandleShooting()
        {
            var fire = Instantiate(firePrefab, transform.position, Quaternion.identity);

            if (fire != null)
            {
                fire.transform.position = transform.position;
                Vector3 direction = (target.position - transform.position).normalized;
                fire.Shoot(direction, 30f);

                _lastShootTime = Time.time;

                yield return new WaitForSeconds(shootCooldown);
            }
        }

        #endregion
    }
}
