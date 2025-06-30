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
        [SerializeField] private LTDFire firePrefab ;
        
        private float _shootCooldown = 0.2f;
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
             if (_shootingCoroutine != null)
             {
                 StopCoroutine(_shootingCoroutine);
             }
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
            var fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            if (fire != null)
            {
                fire.transform.position = transform.position;
                Vector3 direction = (target.position - transform.position).normalized;
                Debug.Log($"direction = {direction}");

                fire.Shoot(direction, 10);
                _lastShootTime = Time.time;

                yield return new WaitForSeconds(_shootCooldown);
            }
        }

        #endregion

    }
}