using System.Collections.Generic;
using UnityEngine;

namespace LTD.Core.Pool
{
    public class MononPool<T> : MonoSingleton<MononPool<T>> where T : MonoBehaviour, IPoolable
    {
        [SerializeField] private int initialPoolSize;
        [SerializeField] private T poolPrefab;
        [SerializeField] private Transform parentPrefab;
        private Stack<T> _available;

        private void Awake()
        {
            _available = new Stack<T>();
            AddItemsToPool();
        }

        public T Get()
        {
            if (_available.Count == 0)
            {
                AddItemsToPool();
            }

            var obj = _available.Pop();
            obj.gameObject.SetActive(true);
            obj.SetPoolable();
            return obj;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _available.Push(obj);
        }

        private void AddItemsToPool()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                var obj = Instantiate(poolPrefab);
                obj.gameObject.SetActive(false);
                _available.Push(obj);
            }
        }
    }
}