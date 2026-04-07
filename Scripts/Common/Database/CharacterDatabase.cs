using System;
using System.Collections.Generic;
using Common.Character;
using Common.Data_Saver.Player_Character_Saver.Main;
using UnityEngine;

namespace Common.Database
{
    public static class CharacterDatabase
    {
        private static readonly Dictionary<CharacterType, CharacterSO> _characterDatabase = new();
        
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
                _characterDatabase[character.CharacterType] = character;
            }
        }
        
        public static bool GetCharacter(CharacterType characterType, out CharacterData characterData)
        {
            #region 必要條件檢查
                if (!_initialized)
                {
                    throw new InvalidOperationException(nameof(Initialize));
                }
            #endregion
            
            #region 從角色資料庫獲取資料
                if (_characterDatabase.TryGetValue(characterType, out var originalData))
                {
                    if (PlayerCharacterSaver.LoadCharacterFromLocal(characterType, out var saveData))
                    {
                        characterData = new CharacterData(
                                characterType: originalData.CharacterType,
                                health: saveData.Health,
                                moveSpeed: originalData.MoveSpeed,
                                cardTypes: originalData.CardTypes);
                    }
                    else
                    {
                        characterData = new CharacterData(
                            characterType: originalData.CharacterType,
                            health: originalData.MaxHealth,
                            moveSpeed: originalData.MoveSpeed,
                            cardTypes: originalData.CardTypes);
                    }
                    
                    return true;
                }

                Debug.Log($"無法在角色資料庫中查到名為 {characterType.ToString()} 的角色。");
                throw new InvalidOperationException(characterType.ToString());
            #endregion
        }
    }
}