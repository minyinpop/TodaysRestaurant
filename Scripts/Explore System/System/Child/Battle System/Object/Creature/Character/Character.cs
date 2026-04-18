using System;
using System.Collections;
using Animation_System.Spine;
using Audio_System.Main;
using Common.Character;
using Common.Data_Saver.Player_Character_Saver.Main;
using Common.Database;
using Common.Enemy.Data;
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
                        battleCardTypes: database.BattleCardTypes,
                        deathVFX: database.DeathVFX);

                    Debug.Log($"已從本地儲存的資料獲取 {characterType.ToString()} 的資料，並更新到物件上。");
                    Debug.Log($"血量：{saveData.Health}");
                }
                else
                {
                    CharacterData = new CharacterData(
                        characterType: database.CharacterType,
                        health: database.Health,
                        moveSpeed: database.MoveSpeed,
                        battleCardTypes: database.BattleCardTypes,
                        deathVFX: database.DeathVFX);
                    
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
                        if (OnAttack is null)
                        {
                            Debug.Log($"{nameof(OnAttack)} 沒有被其它 class 訂閱。");
                            return;
                        }

                        OnAttack.Invoke(card, haveEnemyAlive, enemyAllDead);
                    },
                    onComplete: () =>
                    {
                        AnimationSystem.Idle();
                    });
            }
        #endregion

        #region Hurt
            public void Hurt(EnemySO enemyData, Action isAlive, Action isDeath)
            {
                #region 計算傷害
                    CharacterData.SubtractHealth(
                        damage: enemyData.Damage,
                        alive: () =>
                        {
                            #region 播放受擊特效
                                var vfx = Instantiate(enemyData.AttackVFX.gameObject, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                                vfx.Play();
                                
                                Destroy(vfx.gameObject, vfx.main.duration);
                            #endregion
                            
                            #region 播放受擊音效
                                AudioSystem.Instance.AttackSFX.PlayOneShot(enemyData.AttackSFX);
                            #endregion
                            
                            AnimationSystem.Hurt(() =>
                            {
                                AnimationSystem.Idle();
                                isAlive.Invoke();
                            });
                        },
                        dead: () =>
                        {
                            #region 播放死亡特效
                                var vfx = Instantiate(CharacterData.DeathVFX.gameObject, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                                vfx.Play();
                                    
                                Destroy(vfx.gameObject, vfx.main.duration);
                            #endregion
                            
                            #region 播放受擊音效
                                AudioSystem.Instance.AttackSFX.PlayOneShot(enemyData.AttackSFX);
                            #endregion
                            
                            AnimationSystem.Dead(
                                onComplete: () =>
                                {
                                    isDeath.Invoke();
                                });
                        });
                #endregion

                #region 更新顯示
                    healthBar.Subtract(enemyData.Damage);
                #endregion
            }
        #endregion
    }
}