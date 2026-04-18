using System;
using System.IO;
using Common.Scene_Starter;
using UnityEngine;

namespace Title_System
{
    public sealed class TitleSystem : SceneStarter
    {
        private void Awake()
        {
            var folderPath = $"{Application.persistentDataPath}/SaveData";
            
            if (PlayerPrefs.GetInt("DeleteAllLocalData") == 1)
            {
                if (Directory.Exists(folderPath))
                {
                    try
                    {
                        Directory.Delete($"{Application.persistentDataPath}/SaveData", true);
                    }
                    catch (Exception exception)
                    {
                        Debug.Log(exception);
                    }
                }
                
                PlayerPrefs.DeleteKey("DeleteAllLocalData");
                PlayerPrefs.Save();
            }
        }
    }
}