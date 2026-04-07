using System;
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
                var path = Application.persistentDataPath + "/" + saveData.CharacterType + ".json";
                var json = JsonUtility.ToJson(saveData);
                
                File.WriteAllText(path, json);
            }

            public static bool LoadCharacterFromLocal(CharacterType characterType, out CharacterSaveData saveData)
            {
                var path = Application.persistentDataPath + "/" + characterType + ".json";

                if (!File.Exists(path))
                {
                    saveData = null;
                    return false;
                }

                try
                {
                    var json = File.ReadAllText(path);
                        saveData = JsonUtility.FromJson<CharacterSaveData>(json);
                    
                    return true;
                }
                catch (Exception exception)
                {
                    Debug.Log(exception);
                    saveData = null;
                    
                    return false;
                }
            }
        #endregion
    }
}