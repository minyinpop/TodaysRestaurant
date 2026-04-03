using System;
using System.Collections.Generic;
using Common.Level.Main;
using UnityEngine;

namespace Common.Database
{
    public static class LevelDatabase
    {
        private static readonly Dictionary<string, LevelSO> _levelDatabase = new();
        
        private static bool _initialized;
        
        private const string ResourcePath = "Level";

        public static void Initialize()
        {
            if (_initialized)
            {
                #region 開發提示
                    Debug.Log("物品資料庫已初始化過了！");
                #endregion
                
                return;
            }
            
            _initialized = true;

            foreach (var level in Resources.LoadAll<LevelSO>(ResourcePath))
            {
                _levelDatabase[level.LevelName] = level;
            }
        }

        public static bool GetLevel(string levelName, out LevelSO levelData)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException(nameof(_initialized));
            }

            if (_levelDatabase.TryGetValue(levelName, out levelData))
            {
                return true;
            }

            Debug.Log($"無法在關卡資料庫中查到名為 {levelName} 的關卡。");
            return false;
        }
    }
}