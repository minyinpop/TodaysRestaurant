using System;
using Audio_System.Main;
using Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Main;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Child
{
    public sealed class TutorialMuuEnemyObject : BattleEnemyObject
    {
        public static event Action OnHalfHealth;
        
        public override void Attack(Action haveCharacterAlive, Action characterAllDead)
        {
            animation.Attack(
                onAttackPoint: () =>
                {
                    AudioSystem.Instance.AttackSFX.PlayOneShot(attackSFXData);
                    
                    InvokeOnAttack(enemyData.AttackType, enemyData.Damage, haveCharacterAlive.Invoke, characterAllDead.Invoke);
                },
                onComplete: () =>
                {
                    animation.Idle();
                });
        }

        public override void Hurt(int damage, Action isAlive, Action isDeath)
        {
            #region 檢查條件
                if (damage < 0)
                {
                    throw new ArgumentOutOfRangeException($"{name} 受到了負數的傷害。");
                }
            #endregion
            
            #region 扣除血量
            _health = Mathf.Max(_health - damage, 0);

            if (_health <= enemyData.Health / 2)
            {
                if (OnHalfHealth is null)
                {
                    throw new InvalidOperationException($"沒有 class 訂閱 {name} 的 {nameof(OnHalfHealth)}。");
                }

                OnHalfHealth.Invoke();
                return;
            }

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
    }
}