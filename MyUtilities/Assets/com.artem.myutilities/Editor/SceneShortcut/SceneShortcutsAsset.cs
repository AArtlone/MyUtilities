using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSettingsAsset", menuName = "SceneShortcuts")]
public class SceneShortcutsAsset : ScriptableObject
{
    public List<SceneShortcut> sceneShortcuts = default;
}

[System.Serializable]
public class SceneShortcut
{
    public string name;
    public SceneAsset sceneAsset;
}

