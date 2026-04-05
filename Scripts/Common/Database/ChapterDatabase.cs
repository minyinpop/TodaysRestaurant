using System.Collections.Generic;
using Common.Character;
using Common.Dialogue.Main;
using UnityEngine;

namespace Common.Database
{
    public static class ChapterDatabase
    {
        private static readonly Dictionary<string, DialogueSO> _chapterDatabase = new();

        private static bool _initialized;
        
        private const string ResourcePath = "Chapter";
        
        public static void Initialize()
        {
            if (_initialized)
            {
                #region 開發提示
                    Debug.Log("章節資料庫已初始化過了！");
                #endregion
                
                return;
            }
            
            _initialized = true;

            foreach (var dialogue in Resources.LoadAll<DialogueSO>(ResourcePath))
            {
                _chapterDatabase[dialogue.name] = dialogue;
            }
        }
    }
}