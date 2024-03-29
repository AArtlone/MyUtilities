﻿using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MyUtilities.DataSheets
{
    public class DSModelScriptGenerator
    {
        private const string DatasheetsPath = "Assets/Resources/Datasheets/";

        private const string DatasheetsModelsPath = "Assets/Scripts/Datasheets";
        private const string DatasheetsEditorModelsPath = "Assets/Scripts/Editor/Datasheets";

        private const string ModelFileName = "DSModel";
        private const string RecordFileName = "DSRecord";
        private const string IDFileName = "DSID";
        private const string EditorMenuFileName = "DSEditorMenu";

        public static void GenerateModelClasses(string modelName)
        {
            if (!CheckCSVFile(modelName))
                return;

            CreateDSModelFolder(modelName);

            CreateEditorDSModelFolder(modelName);

            GenerateClasses(modelName);

            AssetDatabase.Refresh();
        }

        private static void GenerateClasses(string modelName)
        {
            GenerateModelClass(modelName);

            GenerateRecordClass(modelName);

            GenerateIDClass(modelName);

            GenerateEditorMenuClass(modelName);
        }

        private static void CreateDSModelFolder(string modelName)
        {
            if (AssetDatabase.IsValidFolder(DatasheetsModelsPath + "/Example"))
            {
                Debug.LogWarning("Folder exists");
                return;
            }

            AssetDatabase.CreateFolder(DatasheetsModelsPath, modelName);
        }

        private static void CreateEditorDSModelFolder(string modelName)
        {
            if (AssetDatabase.IsValidFolder(DatasheetsEditorModelsPath + "/Example"))
            {
                Debug.LogWarning("Folder exists");
                return;
            }

            AssetDatabase.CreateFolder(DatasheetsEditorModelsPath, modelName);
        }

        private static void GenerateModelClass(string modelName)
        {
            string modelFilePath = GetFilePath(modelName, ModelFileName);

            Debug.Log("Creating Classfile: " + modelFilePath);

            if (File.Exists(modelFilePath))
            {
                Debug.LogWarning($"The {modelName}DSModel class already exists");
                return;
            }

            using (StreamWriter outFile = new StreamWriter(modelFilePath))
            {
                outFile.WriteLine("using MyUtilities.DataSheets;");
                outFile.WriteLine("using UnityEngine;");

                outFile.WriteLine("");

                outFile.WriteLine($"public class {modelName}DSModel: DSModelBase<{modelName}DSRecord, {modelName}DSID>");
                outFile.WriteLine("{");
                outFile.WriteLine($"\tprotected override {modelName}DSRecord CreateRecord(string[] csvFileLine)");
                outFile.WriteLine("\t{");
                outFile.WriteLine($"\t\tvar result = new {modelName}DSRecord(csvFileLine);");
                outFile.WriteLine("");
                outFile.WriteLine($"\t\treturn result;");
                outFile.WriteLine("\t}");

                outFile.WriteLine("}");
            }
        }

        private static void GenerateRecordClass(string modelName)
        {
            string recordFilePath = GetFilePath(modelName, RecordFileName);

            Debug.Log("Creating Classfile: " + recordFilePath);

            if (File.Exists(recordFilePath))
            {
                Debug.LogWarning($"The {modelName}DSRecord class already exists");
                return;
            }

            using (StreamWriter outFile = new StreamWriter(recordFilePath))
            {
                outFile.WriteLine("using MyUtilities.DataSheets;");
                outFile.WriteLine("using System;");

                outFile.WriteLine("");
                
                outFile.WriteLine("[Serializable]");
                outFile.WriteLine($"public class {modelName}DSRecord: DSRecordBase<{modelName}DSID>");
                outFile.WriteLine("{");
                outFile.WriteLine($"\tpublic {modelName}DSRecord(string[] csvFileLine)");
                outFile.WriteLine("\t{");
                outFile.WriteLine($"\t\trecordID = ({modelName}DSID)Enum.Parse(typeof({modelName}DSID), csvFileLine[0]);");
                outFile.WriteLine("\t}");
                outFile.WriteLine("}");
            }
        }

        private static void GenerateIDClass(string modelName)
        {
            string idFilePath = GetFilePath(modelName, IDFileName);

            Debug.Log("Creating Classfile: " + idFilePath);

            if (File.Exists(idFilePath))
            {
                Debug.LogWarning($"The {modelName}DSID class already exists");
                return;
            }

            string csvFilePath = $"Datasheets/{modelName}";

            List<string> enumLinesToAdd = CSVReader.GetEnumLines(csvFilePath);

            using (StreamWriter outFile = new StreamWriter(idFilePath))
            {
                outFile.WriteLine($"public enum {modelName}DSID");
                outFile.WriteLine("{");

                for (int i = 0; i < enumLinesToAdd.Count; i++)
                {
                    string line = "\t" + enumLinesToAdd[i];

                    if (i != enumLinesToAdd.Count - 1)
                        line += ",";

                    outFile.WriteLine(line);
                }

                outFile.WriteLine("}");
            }
        }

        private static void GenerateEditorMenuClass(string modelName)
        {
            string idFilePath = GetEditorFilePath(modelName, EditorMenuFileName);

            Debug.Log("Creating Classfile: " + idFilePath);

            if (File.Exists(idFilePath))
            {
                Debug.LogWarning($"The {modelName}DSID class already exists");
                return;
            }

            using (StreamWriter outFile = new StreamWriter(idFilePath))
            {
                outFile.WriteLine("using System.IO;");
                outFile.WriteLine("using UnityEditor;");
                outFile.WriteLine("using UnityEngine;");

                outFile.WriteLine("");

                outFile.WriteLine($"public class {modelName}DSEditorMenu : EditorWindow");
                outFile.WriteLine("{");

                outFile.WriteLine($"\tprivate const string CSVPath = \"Datasheets/{modelName}\";");
                outFile.WriteLine($"\tprivate const string ModelPath = \"DSModels/{modelName}Model.asset\";");

                outFile.WriteLine("");

                outFile.WriteLine($"\t[MenuItem(\"Window/{modelName}DSModel/GenerateModel\")]");
                outFile.WriteLine("\tpublic static void GenerateModel()");
                outFile.WriteLine("\t{");
                outFile.WriteLine("\t\tif (!AssetDatabase.IsValidFolder(\"Assets/Resources/DSModels\"))");
                outFile.WriteLine("\t\t\tAssetDatabase.CreateFolder(\"Assets/Resources\", \"DSModels\");");
                outFile.WriteLine("");
                outFile.WriteLine("\t\tstring path = \"Assets/Resources/\" + ModelPath;");
                outFile.WriteLine("\t\tbool exists = File.Exists(path);");
                outFile.WriteLine("");
                outFile.WriteLine("\t\tif (exists)");
                outFile.WriteLine("\t\t{");
                outFile.WriteLine("\t\t\tDebug.LogWarning(\"Model already exists at \" + path);");
                outFile.WriteLine("\t\t\treturn;");
                outFile.WriteLine("\t\t}");
                outFile.WriteLine("");
                outFile.WriteLine($"\t\t{modelName}DSModel model = CreateInstance<{modelName}DSModel>();");
                outFile.WriteLine("");
                outFile.WriteLine($"\t\tAssetDatabase.CreateAsset(model, path);");
                outFile.WriteLine($"\t\tAssetDatabase.SaveAssets();");
                outFile.WriteLine("");
                outFile.WriteLine($"\t\tUpdateModel();");
                outFile.WriteLine("\t}");

                outFile.WriteLine("");

                outFile.WriteLine($"\t[MenuItem(\"Window/{modelName}DSModel/UpdateModel\")]");
                outFile.WriteLine("\tpublic static void UpdateModel()");
                outFile.WriteLine("\t{");
                outFile.WriteLine("\t\tstring path = ModelPath.Split(new char [] {'.'})[0];");
                outFile.WriteLine("");
                outFile.WriteLine($"\t\t{modelName}DSModel model = Resources.Load<{modelName}DSModel>(path);");
                outFile.WriteLine("");
                outFile.WriteLine("\t\tif (model == null)");
                outFile.WriteLine("\t\t\treturn;");
                outFile.WriteLine("");
                outFile.WriteLine($"\t\tmodel.Initialize(CSVPath);");
                outFile.WriteLine("");
                outFile.WriteLine("\t\tEditorUtility.SetDirty(model);");
                outFile.WriteLine("\t\tAssetDatabase.SaveAssets();");
                outFile.WriteLine("\t}");

                outFile.WriteLine("}");
            }
        }

        private static string GetFilePath(string modelName, string fileName)
        {
            return DatasheetsModelsPath + $"/{modelName}/{modelName}{fileName}.cs";
        }

        private static string GetEditorFilePath(string modelName, string fileName)
        {
            return DatasheetsEditorModelsPath + $"/{modelName}/{modelName}{fileName}.cs";
        }

        private static bool CheckCSVFile(string modelName)
        {
            bool exists = File.Exists(DatasheetsPath + $"{modelName}.csv");

            if (!exists)
                Debug.LogWarning($"The CSV file with name {modelName} was not found in the Resources/Datasheets. Please make sure the file exists and has the correct name and file extension");

            return exists;
        }
    }
}
