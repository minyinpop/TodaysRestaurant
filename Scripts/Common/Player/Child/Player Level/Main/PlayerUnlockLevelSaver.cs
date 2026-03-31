using System.IO;
using Common.Player.Child.Player_Level.Child;
using UnityEngine;

namespace Common.Player.Child.Player_Level.Main
{
    public static class PlayerUnlockLevelSaver
    {
        private const string Keyword = "Unlock_Level";

        private const string LocalPath = "/Unlock_Level.json";

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