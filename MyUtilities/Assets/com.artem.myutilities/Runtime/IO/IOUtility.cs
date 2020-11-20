using System;
using UnityEngine;

namespace MyUtilities
{
    public class IOUtility : MonoBehaviour
    {

        public static void SaveData(object data, Action<bool> callback)
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

            IOFileHandler.SaveFile(jsonData, "PlayerData", callback);
        }

        public static void LoadData(Action<object> callback)
        {
            IOFileHandler.CheckFileExists("PlayerData", (bool exists) =>
            {
                if (!exists)
                {
                    callback(null);
                    return;
                }

                IOFileHandler.LoadFile("PlayerData", (string data) =>
                {
                    object playerData;

                    if (!TryToParseFromJson(data, out playerData))
                    {
                        callback(null);
                        return;
                    }

                    callback(playerData);
                });
            });
        }

        private static bool TryToParseToJson(object data, out string json)
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

        private static bool TryToParseFromJson(string json, out object playerData)
        {
            if (string.IsNullOrEmpty(json))
            {
                playerData = null;
                return false;
            }

            try
            {
                playerData = JsonUtility.FromJson<object>(json);
                return true;
            }
            catch (Exception exception)
            {
                if (Application.isEditor)
                    throw exception;

                playerData = null;
                return false;
            }
        }
    }
}