using System;
using System.Collections.Generic;
using Common.Character;
using Common.Data_Saver.Player_Character_Saver.Main;
using UnityEngine;

namespace Common.Database
{
    public static class CharacterDatabase
    {
        private static readonly Dictionary<string, CharacterSO> _characterDatabase = new();
        
        private static bool _initialized;
        
        private const string ResourcePath = "Character";
        
        public static void Initialize()
        {
            if (_initialized)
            {
                #region 開發提示
                    Debug.Log("角色資料庫已初始化過了！");
                #endregion
                
                return;
            }
            
            _initialized = true;

            foreach (var character in Resources.LoadAll<CharacterSO>(ResourcePath))
            {
                _characterDatabase[character.name] = character;
            }
        }
        
        public static bool GetCharacter(string characterName, out CharacterData characterData)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException(nameof(Initialize));
            }
            
            if (_characterDatabase.TryGetValue(characterName, out var originalData))
            {
                PlayerCharacterSaver.LoadCharacterFromLocal(characterName, out var saveData);
                
                characterData = new CharacterData(
                        characterName: originalData.name,
                        health: saveData.Health,
                        moveSpeed: originalData.MoveSpeed,
                        cardTypes: originalData.CardTypes);
                return true;
            }

            Debug.Log($"無法在角色資料庫中查到名為 {characterName} 的角色。");
            characterData = null;
            return false;
        }
    }
}