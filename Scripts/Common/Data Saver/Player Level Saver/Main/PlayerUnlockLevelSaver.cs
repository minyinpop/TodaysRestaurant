using System.IO;
using Common.Data_Saver.Player_Level_Saver.Child;
using UnityEngine;

namespace Common.Data_Saver.Player_Level_Saver.Main
{
    public static class PlayerUnlockLevelSaver
    {
        private const string Keyword = "Unlock_Level";
        private const string LocalPath = "/" + Keyword + ".json";

        public static void SaveUnlockLevelToLocal(UnlockLevelSaveData saveData)
        {
            var path = Application.persistentDataPath + LocalPath;
            var json = JsonUtility.ToJson(saveData);
            
            File.WriteAllText(path, json);
        }
        
        public static UnlockLevelSaveData LoadUnlockLevelFromLocal()
        {
            var path = Application.persistentDataPath + LocalPath;
            var json = File.ReadAllText(path);
            
            return JsonUtility.FromJson<UnlockLevelSaveData>(json);
        }
    }
}