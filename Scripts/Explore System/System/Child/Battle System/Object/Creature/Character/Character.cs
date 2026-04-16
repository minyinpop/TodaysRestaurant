using System;
using System.Collections;
using Animation_System.Spine;
using Common.Character;
using Common.Data_Saver.Player_Character_Saver.Main;
using Common.Database;
using Common.Status_Bar;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Character
{
    [RequireComponent(typeof(AnimationSystem))]
    internal class Character : MonoBehaviour
    {
        [field: Header("Systems")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Objects")]
        [field: SerializeField] private StatusBar healthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private CharacterType characterType;
        
        public CharacterData CharacterData { get; private set; }
        
        public static event Action<Card.Card, Action, Action> OnAttack;

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
            #endregion

            if (CharacterDatabase.GetCharacter(characterType, out var database))
            {
                if (PlayerCharacterSaver.LoadCharacterFromLocal(characterType, out var saveData))
                {
                    CharacterData = new CharacterData(
                        characterType: database.CharacterType,
                        health: saveData.Health,
                        moveSpeed: database.MoveSpeed,
                        cardTypes: database.CardTypes);

                    Debug.Log($"已從本地儲存的資料獲取 {characterType.ToString()} 的資料，並更新到物件上。");
                    Debug.Log($"血量：{saveData.Health}");
                }
                else
                {
                    CharacterData = new CharacterData(
                        characterType: database.CharacterType,
                        health: database.Health,
                        moveSpeed: database.MoveSpeed,
                        cardTypes: database.CardTypes);
                    
                    Debug.Log($"無法從本地儲存的資料獲取 {characterType.ToString()} 的資料，已套用進資料庫的資料。");
                    Debug.Log($"血量：{database.Health}");
                }
            }
            
            #region 初始化血調顯示
                if (CharacterData.Health > database.Health)
                {
                    healthBar.Initialize(
                                value: database.Health,
                                maxValue: database.Health);
                }
                else if (CharacterData.Health <= database.Health)
                {
                    healthBar.Initialize(
                        value: CharacterData.Health,
                        maxValue: database.Health);
                }
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
            public void Attack(Card.Card card, SpineAnimation spine, Action haveEnemyAlive, Action enemyAllDead)
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
                                isAlive.Invoke();
                            });
                        },
                        dead: () =>
                        {
                            AnimationSystem.Dead(
                                onComplete: () =>
                                {
                                    isDeath.Invoke();
                                });
                        });
                #endregion

                #region 更新顯示
                    healthBar.Subtract(damage);
                #endregion
            }
        #endregion
    }
}