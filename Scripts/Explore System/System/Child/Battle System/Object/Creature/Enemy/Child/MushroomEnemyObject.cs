using System;
using Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Main;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Child
{
    public sealed class MushroomEnemyObject : BattleEnemyObject
    {
        public override void Attack(Action haveCharacterAlive, Action characterAllDead)
        {
            animation.Attack(
                onAttackPoint: () =>
                {
                    InvokeOnAttack(enemyData.Damage, haveCharacterAlive.Invoke, characterAllDead.Invoke);
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