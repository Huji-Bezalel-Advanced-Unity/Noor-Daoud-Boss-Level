using BLE.Gamelogic.Providers;
using LTD.Core.BaseMono;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

namespace _LTD.Scripts.GameLogic.Controls
{
    public class LTDEnemyAIBasic:LTDBaseMono
    {
        [SerializeField] private Vector2 radiusRange = new Vector2(1f, 2f);
        [SerializeField] private Vector2 idleTimeRangeSeconds = new Vector2(1f, 2f);
        [SerializeField] private float walkSpeed = 1f;
        [SerializeField] private LTDEnemiesProvider enemiesProvider;
        private LTDEnemyStates _enemyState = LTDEnemyStates.Idle;
        
        private Vector2 _nextLocation = Vector2.zero;
        private float _nextIdleLength = 1f;
        private float _idleTime = 0f;

        private void Awake()
        {
            enemiesProvider.AddEnemy(transform);
        }

        private void OnDestroy()
        {
            enemiesProvider.RemoveEnemy(transform);
        }

        private void Update()
        {
            CheckState();
            DoAccordingToState();
        }

        private void CheckState()
        {
            if (_enemyState == LTDEnemyStates.Idle)
            {
                CheckExitIdle();
            }
            else if( _enemyState == LTDEnemyStates.Walk)
            {
                CheckArrive();
            }
        }

        private void CheckExitIdle()
        {
            _idleTime += Time.deltaTime;
            
            if (_idleTime >= _nextIdleLength)
            {
                ExitIdle();
            }
        }

        private void ExitIdle()
        {
            _enemyState = LTDEnemyStates.Walk;
            _idleTime = 0f;

            CalcualteNextWalk();
        }

        private void CalcualteNextWalk()
        {
            var randomCircle = UnityEngine.Random.insideUnitCircle;
            var radius = UnityEngine.Random.Range(radiusRange.x, radiusRange.y);
            _nextLocation = transform.position + (Vector3) randomCircle * radius;
        }

        private void CheckArrive()
        {
            var distance = Vector3.Distance(transform.position, _nextLocation);
            if (distance < 0.1f)
            {
                GoToIdle();
            }
        }

        private void GoToIdle()
        {
            _enemyState = LTDEnemyStates.Idle;
            _nextIdleLength = UnityEngine.Random.Range(idleTimeRangeSeconds.x, idleTimeRangeSeconds.y);
        }

        private void DoAccordingToState()
        {
            switch (_enemyState)
            {
                case LTDEnemyStates.Idle:
                    Idle();
                    break;
                
                case LTDEnemyStates.Walk:
                    Walk();
                    break;
                case LTDEnemyStates.Dead:
                    Dead();
                    break;
            }
        }

        private void Dead()
        {
        }

        private void Idle()
        {
        }

        private void Walk()
        {
            var direction = _nextLocation - (Vector2) transform.position;
            direction.Normalize();
            transform.position += (Vector3) direction * (walkSpeed * Time.deltaTime);
            
            transform.up = direction;
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PlyaerBullet"))
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }

    public enum LTDEnemyStates
    {
        Idle,
        Walk,
        Dead
    }
}
