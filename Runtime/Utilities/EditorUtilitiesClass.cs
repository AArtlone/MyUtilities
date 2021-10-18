using UnityEditor;
using UnityEngine;

namespace MyUtilities
{
    public class EditorUtilitiesClass
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/Create Empty At Zero/Game Object", false, -1)]
        public static void CreateGameObjectAtZero()
        {
            var obj = new GameObject("---------------");
            Selection.activeObject = obj;
        }

        [MenuItem("GameObject/Create Empty At Zero/Sprite", false, -1)]
        public static void CreateSpriteAtZero()
        {
            var obj = new GameObject("---------------", typeof(SpriteRenderer));
            Selection.activeObject = obj;
        }
#endif
    }
}
