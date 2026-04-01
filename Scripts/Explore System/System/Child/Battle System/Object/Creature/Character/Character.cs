using System;
using System.Collections;
using Animation_System.Spine;
using Common.Character;
using Common.Data_Saver.Player_Character_Saver.Child;
using Common.Data_Saver.Player_Character_Saver.Main;
using Common.Database;
using Common.Status_Bar;
using Explore_System.System.Child.Battle_System.Object.Card;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Character
{
    [RequireComponent(typeof(AnimationSystem))]
    internal class Character : Creature
    {
        [field: Header("Systems")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Objects")]
        [field: SerializeField] private StatusBar healthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private CharacterSO characterData;
                                public CharacterData CharacterData { get; private set; }
        
        public static event Action<ICard, Action, Action> OnAttack;

        private IEnumerator _currentCoroutine;

        private void Awake()
        {
            #region 檢查必要條件
                if (AnimationSystem is null)
                {
                    throw new InvalidOperationException(nameof(AnimationSystem));
                }

                if (healthBar is null)
                {
                    throw new InvalidOperationException(nameof(healthBar));
                }

                if (characterData is null)
                {
                    throw new InvalidOperationException(nameof(characterData));
                }
            #endregion

            #region 嘗試從本地獲取該角色的資料
                if (PlayerCharacterSaver.LoadCharacterFromLocal(characterData.name, out var saveData))
                {
                    if (CharacterDatabase.GetCharacter(characterData.name, out var database))
                    {
                        CharacterData = new CharacterData(
                            characterName: database.CharacterName,
                            health: saveData.Health,
                            moveSpeed: database.MoveSpeed,
                            cardTypes: database.CardTypes);
                    }
                    else
                    {
                        throw new InvalidOperationException(nameof(characterData));
                    }
                }
            #endregion
                
            #region 從資料庫創建該角色的新資料
                else
                {
                    if (CharacterDatabase.GetCharacter(characterData.name, out var database))
                    {
                        CharacterData = new CharacterData(
                            characterName: database.CharacterName,
                            health: database.Health,
                            moveSpeed: database.MoveSpeed,
                            cardTypes: database.CardTypes);
                    }
                    else
                    {
                        throw new InvalidOperationException(nameof(characterData));
                    }
                }
            #endregion
            
            #region 初始化血調顯示
                healthBar.Initialize(
                            value: CharacterData.Health,
                            maxValue: characterData.MaxHealth);
            #endregion
        }

        private void OnEnable()
        {
            AnimationSystem.Idle();
        }

        private void OnDisable()
        {
            if (_currentCoroutine is not null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = null;
            }
        }

        #region Attack
            public void Attack(ICard card, SpineAnimation spine, Action haveEnemyAlive, Action enemyAllDead)
            {
                AnimationSystem.Attack(spine,
                    onAttackPoint: () =>
                    {
                        OnAttack?.Invoke(card, haveEnemyAlive, enemyAllDead);
                    },
                    onComplete: () =>
                    {
                        AnimationSystem.Idle();
                    });
            }
        #endregion

        #region Hurt
            public void Hurt(int damage, Action isAlive, Action isDeath)
            {
                #region 計算傷害
                    CharacterData.SubtractHealth(
                        damage: damage,
                        alive: () =>
                        {
                            AnimationSystem.Hurt(() =>
                            {
                                AnimationSystem.Idle();
                                isAlive?.Invoke();
                            });
                        },
                        dead: () =>
                        {
                            AnimationSystem.Dead(
                                onComplete: () =>
                                {
                                    isDeath?.Invoke();
                                });
                        });
                #endregion

                #region 更新顯示
                    healthBar.Subtract(damage);
                #endregion
                
                #region 儲存資料
                    var saveData = new CharacterSaveData(
                        characterName: CharacterData.CharacterName,
                        health: CharacterData.Health);
                    
                    PlayerCharacterSaver.SaveCharacterToLocal(saveData);
                #endregion
            }
        #endregion
    }
}