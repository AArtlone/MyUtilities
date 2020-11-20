using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace MyUtilities
{
    public class IOFileHandler
    {

        private static string dataPath = Application.dataPath;

        public static void SaveFile(string jsonData, string fileName, Action<bool> callback)
        {
            var savingThread = new Thread(() =>
            {
                WriteFile(fileName, jsonData, callback);
            });

            savingThread.Start();
        }

        public static void LoadFile(string fileName, Action<string> callback)
        {
            var thread = new Thread(() =>
            {
                ReadFile(callback, fileName);
            });

            thread.Start();
        }

        public static void CheckFileExists(string fileName, Action<bool> callback)
        {
            string filePath = GetFilePath(fileName);

            bool exists = false;

            try
            {
                exists = File.Exists(filePath);
            }
            catch (Exception exception)
            {
                if (Application.isEditor)
                    throw exception;
            }

            callback(exists);
        }

        private static void ReadFile(Action<string> callback, string fileName)
        {
            string path = GetFilePath(fileName);

            string data = null;

            try
            {
                data = File.ReadAllText(path);
            }
            catch (Exception exception)
            {
                if (Application.isEditor)
                    throw exception;
            }

            callback(data);
        }

        private static void WriteFile(string fileName, string data, Action<bool> doneCallback)
        {
            string path = GetFilePath(fileName);

            // Converting to JSON
            path += ".json";

            try
            {
                Debug.Log(path);
                File.WriteAllText(path, data, System.Text.Encoding.UTF8);
            }
            catch (Exception exception)
            {
                if (Application.isEditor)
                    Debug.LogException(exception);

                doneCallback(false);

                return;
            }

            doneCallback(true);
        }

        private static string GetFilePath(string fileName)
        {
            if (Application.isEditor)
            {
                string editorPath = Path.GetDirectoryName(dataPath);

                editorPath += "/SaveData";

                if (!Directory.Exists(editorPath))
                    Directory.CreateDirectory(editorPath);

                return Path.Combine(editorPath, fileName);
            }

            return Path.Combine(Application.persistentDataPath, fileName);
        }
    }
}