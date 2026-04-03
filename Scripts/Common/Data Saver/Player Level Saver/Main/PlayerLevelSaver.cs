using System;
using System.IO;
using Common.Data_Saver.Player_Level_Saver.Child;
using UnityEngine;

namespace Common.Data_Saver.Player_Level_Saver.Main
{
    public static class PlayerLevelSaver
    {
        private const string Keyword = "Unlock_Level";
        private const string LocalPath = "/" + Keyword + ".json";

        public static void SaveUnlockLevelToLocal(LevelSaveData saveData)
        {
            var path = Application.persistentDataPath + LocalPath;
            var json = JsonUtility.ToJson(saveData);
            
            File.WriteAllText(path, json);
        }
        
        public static bool LoadUnlockLevelFromLocal(out LevelSaveData saveData)
        {
            var path = Application.persistentDataPath + LocalPath;

            if (!File.Exists(path))
            {
                saveData = null;
                return false;
            }

            try
            {
                var json = File.ReadAllText(path);
                saveData = JsonUtility.FromJson<LevelSaveData>(json);

                return true;
            }
            catch (Exception exception)
            {
                Debug.Log(exception);
                saveData = null;

                return false;
            }
        }
    }
}