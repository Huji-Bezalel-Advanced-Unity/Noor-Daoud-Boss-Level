using System.Collections;
using LTD.Core.BaseMono;
using LTD.Core.Managers;
using Pool;
using UnityEngine;

namespace BLE.Gamelogic.Enemies
{
    public class TheBoss : LTDBaseMono
    {
           [Header("References")]
        [SerializeField] private Transform target;

        private float _shootCooldown = 0.5f;
        private float _lastShootTime;

        private Coroutine _shootingCoroutine;

        private void Awake()
        {
            Events.SafeZone += StopShooting;
            Events.RedZone += KeepShooting;
        }
        private void Start()
        {
            _shootingCoroutine = StartCoroutine(HandleShootingCoroutine());
        }
        private void OnDestroy()
        {
            Events.SafeZone -= StopShooting;
            Events.RedZone -= KeepShooting;
        }

        #region Callbacks:

        private void KeepShooting()
        {
            if (_shootingCoroutine != null)
            {
                StopCoroutine(_shootingCoroutine);
            }

            _shootingCoroutine = StartCoroutine(HandleShootingCoroutine());

            this.StopAndStartCoroutine(ref _shootingCoroutine, HandleShootingCoroutine());
        }

        private void StopShooting()
        {
            // if (_shootingCoroutine != null)
            // {
            //     StopCoroutine(_shootingCoroutine);
            // }

            this.StopWithNullCheckCoroutine(ref _shootingCoroutine);
        }

        #endregion

        #region Coroutines:

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
            Bullet bullet = BulletPool.Instance.Get();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                Vector3 direction = (target.position - transform.position).normalized;
                Debug.Log($"direction = {direction}");

                bullet.Shoot(direction, "Bullet", 10f, "BossBullet");
                _lastShootTime = Time.time;

                yield return new WaitForSeconds(_shootCooldown);
            }
        }

        #endregion

    }
}