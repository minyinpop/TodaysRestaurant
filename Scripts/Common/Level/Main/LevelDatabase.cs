using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Level.Main
{
    public static class LevelDatabase
    {
        private static readonly Dictionary<string, LevelSO> _levelDatabase = new();
        
        private static bool _initialized;

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

            foreach (var level in Resources.LoadAll<LevelSO>("Level"))
            {
                _levelDatabase[level.LevelName] = level;
            }
        }

        public static LevelSO GetLevel(string levelName)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException(nameof(_initialized));
            }

            return _levelDatabase.GetValueOrDefault(levelName);
        }
    }
}