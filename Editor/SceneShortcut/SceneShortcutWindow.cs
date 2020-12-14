using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace MyUtilities.GUI
{
    public class SceneShortcutWindow : EditorWindow
    {
        [SerializeField] private List<SceneAsset> sceneAssets = default;

        private static SceneShortcutsAsset asset;

        [MenuItem("Window/SceneShortcut")]
        public static void ShowWindow()
        {
            asset = Resources.Load<SceneShortcutsAsset>("Sa");

            GetWindow<SceneShortcutWindow>();
        }

        private void OnGUI()
        {
            if (asset == null)
            {
                GUILayout.Label("SceneShortcuts asset is null", EditorStyles.boldLabel);
                return;
            }

            if (asset.sceneShortcuts.Count == 0)
            {
                GUILayout.Label("No shortcuts", EditorStyles.boldLabel);
                return;
            }

            for (int i = 0; i < asset.sceneShortcuts.Count; i++)
            {
                if (GUILayout.Button(asset.sceneShortcuts[i].name))
                {
                    var scenePath = AssetDatabase.GetAssetPath(asset.sceneShortcuts[i].sceneAsset);
                    EditorSceneManager.OpenScene(scenePath);
                }
            }
        }
    }
}
