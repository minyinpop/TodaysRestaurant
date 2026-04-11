using System.IO;
using Common.Data_Saver.Player_Settings_Saver.Child;
using UnityEngine;

namespace Common.Data_Saver.Player_Settings_Saver.Main
{
    public static class PlayerSettingsSaver
    {
        private const string _keyword = "Settings";
        
        private static string _folderPath;
        private static string _filePath;

        public static void SaveSettingsToLocal(SettingsSaveData saveData)
        {
            CheckPath();
            CheckFolder();
            CheckFile();
            
            var json = JsonUtility.ToJson(saveData);
            File.WriteAllText(_filePath, json);
        }

        public static void LoadSettingsFromLocal(out SettingsSaveData saveData)
        {
            CheckPath();
            CheckFolder();
            CheckFile();
            
            var json = File.ReadAllText(_filePath);
            saveData = JsonUtility.FromJson<SettingsSaveData>(json);
        }

        private static void CheckPath()
        {
            if (string.IsNullOrEmpty(_folderPath))
            {
                _folderPath = $"{Application.persistentDataPath}/SaveData";
            }

            if (string.IsNullOrEmpty(_filePath))
            {
                _filePath = $"{_folderPath}/{_keyword}.json";
            }
        }

        private static void CheckFolder()
        {
            if (Directory.Exists(_folderPath))
            {
                return;
            }

            Directory.CreateDirectory(_folderPath);
            Debug.Log($"找不到 SaveData，已自動創建，位置：{_folderPath}。");
        }

        private static void CheckFile()
        {
            if (File.Exists(_filePath))
            {
                return;
            }
            
            var saveData = new SettingsSaveData
            {
                MasterVolume = 0,
                BGMVolume = 0,
                SFXVolume = 0
            };
            
            var json = JsonUtility.ToJson(saveData);
            File.WriteAllText(_filePath, json);
            
            Debug.Log($"找不到 {_keyword}.json，已自動創建，位置：{_filePath}。");
        }
    }
}