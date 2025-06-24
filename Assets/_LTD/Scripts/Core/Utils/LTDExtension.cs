
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LTD.Core.Utils
{
    public static class LTDExtension
    {
        #region Coroutines:

        public static void StopAndStartCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutine, IEnumerator routine)
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
            }
            coroutine = monoBehaviour.StartCoroutine(routine);
        }
        public static void StopWithNullCheckCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
            }
        }
        public static void StopWithNullifyCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                monoBehaviour.StopCoroutine(coroutine);
            }
            coroutine = null;
        }

        #endregion

        #region Random object from Array,List,Dictionary:

        public static T GetRandomFromArray<T>(this T[] array)
        {
            int random = UnityEngine.Random.Range(0, array.Length);
            if (array.Length == 0) return default;

            return array[random];
        }
        public static T GetRandomFromList<T>(this List<T> list)
        {
            int random = UnityEngine.Random.Range(0, list.Count);
            if (list.Count == 0) return default;

            return list[random];
        }
        public static KeyValuePair<TKey, TValue> GetRandomFromDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            if (dictionary.Count == 0) return default;

            int randomIndex = UnityEngine.Random.Range(0, dictionary.Count);
            var randomElement = dictionary.ElementAt(randomIndex);

            return randomElement;
        }

        #endregion
    }
}
