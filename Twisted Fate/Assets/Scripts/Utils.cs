
using UnityEngine;

namespace Utils
{
    namespace Array
    {
        public static class ArrayUtils
        {
            public static T GetRandomElement<T>(this T[] array)
            {
                return array[UnityEngine.Random.Range(0, array.Length - 1)];
            }
        }
    }
}