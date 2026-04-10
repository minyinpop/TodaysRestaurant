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
            #region 必要條件檢查
                if (_initialized)
                {
                    Debug.Log("場景名稱資料庫已初始化過了！");
                    return;
                }
            #endregion
            
            _initialized = true;

            foreach (var sceneNameData in Resources.LoadAll<SceneNameSO>(ResourcePath))
            {
                _sceneNameDatabase[sceneNameData.SceneType] = sceneNameData;
            }
        }

        public static bool GetSceneName(SceneNameType type, out SceneNameSO data)
        {
            #region 必要條件檢查
                if (!_initialized)
                {
                    throw new InvalidOperationException(nameof(_initialized));
                }

                if (_sceneNameDatabase.TryGetValue(type, out data))
                {
                    return true;
                }
            #endregion

            Debug.Log($"無法在場景名稱資料庫中查到名為 {type} 的場景。");
            return false;
        }
    }
}