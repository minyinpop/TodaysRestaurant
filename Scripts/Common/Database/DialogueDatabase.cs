using System;
using System.Collections.Generic;
using Common.Dialogue.Data;
using Common.Dialogue.Main;
using UnityEngine;

namespace Common.Database
{
    public static class DialogueDatabase
    {
        private static readonly Dictionary<DialogueType, DialogueSO> _dialogueDatabase = new();

        private static bool _initialized;
        
        private const string ResourcePath = "Dialogue";
        
        public static void Initialize()
        {
            if (_initialized)
            {
                #region 開發提示
                    Debug.Log("對話資料庫已初始化過了！");
                #endregion
                
                return;
            }
            
            _initialized = true;

            foreach (var dialogue in Resources.LoadAll<DialogueSO>(ResourcePath))
            {
                _dialogueDatabase[dialogue.DialogueType] = dialogue;
            }
        }

        public static bool GetDialogue(DialogueType type, out DialogueSO data)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException(nameof(_initialized));
            }

            if (_dialogueDatabase.TryGetValue(type, out data))
            {
                return true;
            }

            Debug.Log($"無法在對話資料庫中查到名為 {type} 的對話。");
            return false;
        }
    }
}