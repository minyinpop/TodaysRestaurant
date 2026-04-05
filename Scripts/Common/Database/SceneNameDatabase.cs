using System;
using System.Collections.Generic;
using Common.Scene_Name;
using UnityEngine;

namespace Common.Database
{
    public static class SceneNameDatabase
    {
        private static readonly Dictionary<SceneNameType, SceneNameSO> _sceneNameDatabase = new();
        
        private static bool _initialized;
        
        private const string ResourcePath = "Scene Name";

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

            foreach (var sceneNameData in Resources.LoadAll<SceneNameSO>(ResourcePath))
            {
                _sceneNameDatabase[sceneNameData.SceneType] = sceneNameData;
            }
        }

        public static bool GetSceneName(SceneNameType type, out SceneNameSO data)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException(nameof(_initialized));
            }

            if (_sceneNameDatabase.TryGetValue(type, out data))
            {
                return true;
            }

            Debug.Log($"無法在場景名稱資料庫中查到名為 {type} 的場景。");
            return false;
        }
    }
}