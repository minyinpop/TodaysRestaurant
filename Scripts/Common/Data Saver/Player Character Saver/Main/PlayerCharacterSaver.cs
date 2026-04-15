using System.IO;
using Common.Character;
using Common.Data_Saver.Player_Character_Saver.Child;
using UnityEngine;

namespace Common.Data_Saver.Player_Character_Saver.Main
{
    public static class PlayerCharacterSaver
    {
        
        #region 本地操作
            public static void SaveCharacterToLocal(CharacterSaveData saveData)
            {
                var folderPath = $"{Application.persistentDataPath}/SaveData";
                var filePath = $"{folderPath}/{saveData.CharacterType}.json";

                if (!Directory.Exists(folderPath))
                {
                    Debug.Log($"創建 SaveData 資料夾，位置：{folderPath}。");
                    Directory.CreateDirectory(folderPath);
                }

                if (File.Exists(filePath))
                {
                    Debug.Log($"無法覆寫 {saveData.CharacterType} 的角色資料，位置：{filePath}");
                }
                else
                {
                    var json = JsonUtility.ToJson(saveData);
                    
                    File.WriteAllText(filePath, json);
                }
            }

            public static bool LoadCharacterFromLocal(CharacterType characterType, out CharacterSaveData saveData)
            {
                var folderPath = $"{Application.persistentDataPath}/SaveData";
                var filePath = $"{folderPath}/{characterType}.json";

                if (!Directory.Exists(folderPath))
                {
                    Debug.Log($"創建 SaveData 資料夾，位置：{folderPath}。");
                    Directory.CreateDirectory(folderPath);
                }

                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                        saveData = JsonUtility.FromJson<CharacterSaveData>(json);
                    
                    return true;
                }

                Debug.Log($"無法獲取 {characterType} 角色資料，位置：{filePath}");

                saveData = null;
                return false;
            }
        #endregion
    }
}