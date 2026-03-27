using System;
using Common.Enemy.Data;
using Common.Status_Bar;
using Common.Value;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Enemy
{
    [RequireComponent(typeof(AnimationSystem))]
    internal class Enemy : Creature
    {
        [field: Header("Component")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Object")]
        [field: SerializeField] private StatusBar HealthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private EnemySO enemyData;

        private int _health;

        public static event Action<Damage, Action, Action> OnAttack;

        private void Awake()
        {
            if (AnimationSystem is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(AnimationSystem)} cannot be null.");
            }
            
            if (HealthBar is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(HealthBar)} cannot be null.");
            }

            if (enemyData is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(enemyData)} cannot be null.");
            }

            _health = enemyData.Health;
        }

        private void Start()
        {
            #region 設定血條
                HealthBar.Initialize(_health, _health);
            #endregion
            
            #region 設定動畫
                AnimationSystem.Idle();
            #endregion
        }

        #region Attack
            public void Attack(Action haveCharacterAlive, Action characterAllDead)
            {
                AnimationSystem.Attack(
                    onAttackPoint: () =>
                    {
                        OnAttack?.Invoke(enemyData.Damage,
                            () =>
                            {
                                // haveCharacterAlive
                                haveCharacterAlive?.Invoke();
                            },
                            () =>
                            {
                                // characterAllDead
                                characterAllDead?.Invoke();
                            });
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
                #region 檢查條件
                    if (damage < 0)
                    {
                        throw new ArgumentOutOfRangeException($"{nameof(damage)} must be greater than 0.");
                    }
                #endregion
                
                #region 扣除血量
                    _health = Mathf.Max(_health - damage, 0);
                    
                    if (_health > 0)
                    {
                        AnimationSystem.Hurt(
                            onComplete: () =>
                            {
                                AnimationSystem.Idle();
                                isAlive?.Invoke();
                            });
                    }
                    else
                    {
                        AnimationSystem.Dead(
                            onComplete: () =>
                            {
                                isDeath?.Invoke();
                            });
                    }
                #endregion

                #region 更新血條
                    HealthBar.Subtract(damage);
                #endregion
            }
        #endregion
    }
}