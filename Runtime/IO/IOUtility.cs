using System;
using UnityEngine;

namespace MyUtilities
{
    public class IOUtility<T>
    {
        public static void SaveData(T data, string fileName, Action<bool> callback)
        {
            if (data == null)
            {
                if (Application.isEditor)
                    throw new Exception("Passed data is null");

                callback(false);

                return;
            }

            string jsonData;

            if (!TryToParseToJson(data, out jsonData))
            {
                callback(false);

                return;
            }

            IOFileHandler.SaveFile(jsonData, fileName, callback);
        }

        public static void LoadData(string fileName, Action<T> callback)
        {
            IOFileHandler.CheckFileExists(fileName, (bool exists) =>
            {
                if (!exists)
                {
                    Debug.LogWarning("File does not exist");
                    callback(default(T));
                    return;
                }

                IOFileHandler.LoadFile(fileName, (string data) =>
                {
                    T playerData;

                    if (!TryToParseFromJson(data, out playerData))
                    {
                        callback(default(T));
                        return;
                    }

                    callback(playerData);
                });
            });
        }

        private static bool TryToParseToJson(T data, out string json)
        {
            try
            {
                json = JsonUtility.ToJson(data);

                return true;
            }
            catch (Exception exception)
            {
                if (Application.isEditor)
                    throw exception;

                json = null;

                return false;
            }
        }

        private static bool TryToParseFromJson(string json, out T playerData)
        {
            if (string.IsNullOrEmpty(json))
            {
                playerData = default(T);
                return false;
            }

            try
            {
                playerData = JsonUtility.FromJson<T>(json);
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogWarning("Parsing from JSON failed");

                if (Application.isEditor)
                    throw exception;

                playerData = default(T);
                return false;
            }
        }
    }
}