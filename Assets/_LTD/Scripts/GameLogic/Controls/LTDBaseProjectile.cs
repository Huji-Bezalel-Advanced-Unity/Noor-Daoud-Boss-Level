using System.Collections;
using LTD.Core.BaseMono;
using UnityEngine;

namespace LTD.GameLogic.Controls
{
    public class LTDBaseProjectile : LTDBaseMono,ILTDBaseProjectile
    {
        [SerializeField] private float speed;

        private Transform _target;

        public void FlyTowardsEnemy(Transform target)
        {
            _target = target;
            Initialize();
            StartCoroutine(FlyCoroutine());
        }

        public virtual void Initialize()
        {
            transform.up = _target.position - transform.position;
        }

        private IEnumerator FlyCoroutine()
        {
            while (_target != null)
            {
                yield return null;
                MoveStepTowardsTarget();
            }
        }

        public virtual void MoveStepTowardsTarget()
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }
}