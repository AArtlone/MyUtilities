using System.IO;
using UnityEditor;
using UnityEngine;

public class ExampleDSEditorMenu : EditorWindow
{
	private const string CSVPath = "Datasheets/Example";
	private const string ModelPath = "DSModels/ExampleModel.asset";

	private static ExampleDSModel model;

	[MenuItem("Window/ExampleDSModel/GenerateModel")]
	public static void GenerateModel()
	{
		string path = "Assets/ScriptableObjects/" + ModelPath;
		bool exists = File.Exists(path);

		if (exists)
		{
			Debug.LogWarning("Model already exists at " + path);
			return;
		}

		model = CreateInstance<ExampleDSModel>();

		AssetDatabase.CreateAsset(model, path);
		AssetDatabase.SaveAssets();

		UpdateModel();
	}

	[MenuItem("Window/ExampleDSModel/UpdateModel")]
	public static void UpdateModel()
	{
		if (model == null)
			return;

		model.Initialize(CSVPath);

		EditorUtility.SetDirty(model);
		AssetDatabase.SaveAssets();
	}
}
