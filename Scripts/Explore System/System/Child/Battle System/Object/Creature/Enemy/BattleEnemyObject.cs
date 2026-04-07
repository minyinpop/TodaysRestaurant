using System;
using Common.Enemy.Data;
using Common.Status_Bar;
using Common.Value;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Enemy
{
    [RequireComponent(typeof(AnimationSystem))]
    public class BattleEnemyObject : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private new AnimationSystem animation;
        
        [field: Header("Object")]
        [field: SerializeField] private StatusBar healthBar;
        
        [field: Header("Data")]
        [field: SerializeField] private EnemySO enemyData;

        private int _health;

        public static event Action<Damage, Action, Action> OnAttack;
        
        public bool death { get; private set; }

        private void Awake()
        {
            #region 必要條件檢查
                if (animation is null)
                {
                    throw new InvalidOperationException(nameof(animation));
                }
                
                if (healthBar is null)
                {
                    throw new InvalidOperationException(nameof(healthBar));
                }

                if (enemyData is null)
                {
                    throw new InvalidOperationException(nameof(enemyData));
                }
            #endregion

            _health = enemyData.Health;
        }

        private void Start()
        {
            #region 設定血條
                healthBar.Initialize(_health, _health);
            #endregion
            
            #region 設定動畫
                animation.Idle();
            #endregion
        }

        #region Attack
            public void Attack(Action haveCharacterAlive, Action characterAllDead)
            {
                animation.Attack(
                    onAttackPoint: () =>
                    {
                        OnAttack?.Invoke(enemyData.Damage, haveCharacterAlive.Invoke, characterAllDead.Invoke);
                    },
                    onComplete: () =>
                    {
                        animation.Idle();
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
                        animation.Hurt(
                            onComplete: () =>
                            {
                                animation.Idle();
                                
                                isAlive.Invoke();
                            });
                    }
                    else
                    {
                        animation.Dead(
                            onComplete: () =>
                            {
                                death = true;
                                
                                isDeath.Invoke();
                            });
                    }
                #endregion

                #region 更新血條
                    healthBar.Subtract(damage);
                #endregion
            }
        #endregion
    }
}