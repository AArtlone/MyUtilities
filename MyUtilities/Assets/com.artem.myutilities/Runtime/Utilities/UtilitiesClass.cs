using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MyUtilities
{
    public class UtilitiesClass
    {
        public static List<T> ShuffleList<T>(List<T> listToShuffle)
        {
            for (int i = 0; i < listToShuffle.Count; i++)
            {
                var temp = listToShuffle[i];
                int randomIndex = Random.Range(i, listToShuffle.Count);
                listToShuffle[i] = listToShuffle[randomIndex];
                listToShuffle[randomIndex] = temp;
            }
            return listToShuffle;
        }
#if UNITY_EDITOR
        public static void ClearLog()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
#endif
    }
}
