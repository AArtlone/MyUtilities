using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace MyUtilities.GUI
{
    public class SceneShortcutWindow : EditorWindow
    {
        private static SceneShortcutsAsset asset;
        
        private string assetFileName;

        [MenuItem("Window/SceneShortcut")]
        public static void ShowWindow()
        {
            GetWindow<SceneShortcutWindow>();
        }

        private static void LoadShortcutsAsset(string path)
        {
            asset = Resources.Load<SceneShortcutsAsset>("Editor/" + path);
        }

        private void OnGUI()
        {
            GUILayout.Label("Please enter Shortcuts asset file name. It must be put into Resources/Editor", EditorStyles.textArea);

            GUILayout.Space(5f);

            assetFileName = EditorGUILayout.TextField("File Name", assetFileName);

            if (GUILayout.Button("Load Asset"))
                LoadShortcutsAsset(assetFileName);

            if (asset == null)
            {
                GUILayout.Label("SceneShortcuts asset is null", EditorStyles.boldLabel);
                return;
            }

            if (asset.sceneShortcuts == null || asset.sceneShortcuts.Count == 0)
            {
                GUILayout.Space(15f);
                GUILayout.Label("No shortcuts", EditorStyles.boldLabel);
                return;
            }

            DisplayShortcutsButtons();
        }

        private void DisplayShortcutsButtons()
        {
            for (int i = 0; i < asset.sceneShortcuts.Count; i++)
            {
                if (asset.sceneShortcuts[i].sceneAsset == null)
                    return;

                GUILayout.Space(15f);
                if (GUILayout.Button(asset.sceneShortcuts[i].name))
                {
                    var scenePath = AssetDatabase.GetAssetPath(asset.sceneShortcuts[i].sceneAsset);
                    EditorSceneManager.OpenScene(scenePath);
                }
            }
        }
    }
}
