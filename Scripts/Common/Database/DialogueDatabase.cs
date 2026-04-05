using System.Collections.Generic;
using Common.Dialogue.Main;
using UnityEngine;

namespace Common.Database
{
    public static class DialogueDatabase
    {
        private static readonly Dictionary<string, DialogueSO> _dialogueDatabase = new();

        private static bool _initialized;
        
        private const string ResourcePath = "Dialogue";
        
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
                _dialogueDatabase[dialogue.name] = dialogue;
            }
        }
    }
}