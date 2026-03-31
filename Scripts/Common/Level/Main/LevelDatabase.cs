using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Level.Main
{
    public static class LevelDatabase
    {
        private static readonly Dictionary<int, LevelSO> _levelDatabase = new();
        
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
            {
                Debug.Log("關卡資料庫已初始化。");
                return;
            }
            
            _initialized = true;

            foreach (var level in Resources.LoadAll<LevelSO>("Level"))
            {
                _levelDatabase[level.LevelID] = level;
            }
            
            Debug.Log($"LevelDatabase 初始化完成，數量: {_levelDatabase.Count}");
        }

        public static LevelSO GetLevel(int levelID)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException(nameof(_initialized));
            }

            return _levelDatabase.GetValueOrDefault(levelID);
        }
    }
}