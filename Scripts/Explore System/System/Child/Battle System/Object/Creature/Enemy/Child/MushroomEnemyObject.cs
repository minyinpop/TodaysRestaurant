using System;
using Audio_System.Main;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
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
                    AudioSystem.Instance.AttackSFX.PlayOneShot(attackSFXData);
                    
                    InvokeOnAttack(haveCharacterAlive.Invoke, characterAllDead.Invoke);
                },
                onComplete: () =>
                {
                    animation.Idle();
                });
        }
        
        public override void Hurt(BattleCardSO battleCardData, Action isAlive, Action isDeath)
        {
            #region 檢查條件
                if (battleCardData.Damage < 0)
                {
                    throw new ArgumentOutOfRangeException($"{name} 受到了負數的傷害。");
                }
            #endregion
            
            #region 扣除血量
                _health = Mathf.Max(_health - battleCardData.Damage, 0);
                
                if (_health > 0)
                {
                    #region 播放受擊特效
                        var vfx = Instantiate(battleCardData.AttackVFX.gameObject, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                        vfx.Play();
                        
                        Destroy(vfx.gameObject, vfx.main.duration);
                    #endregion
                    
                    #region 播放受擊音效
                        AudioSystem.Instance.AttackSFX.PlayOneShot(battleCardData.UseSFXData);
                    #endregion
                    
                    animation.Hurt(
                        onComplete: () =>
                        {
                            animation.Idle();
                            
                            isAlive.Invoke();
                        });
                }
                else
                {
                    #region 播放死亡特效
                        var vfx = Instantiate(enemyData.DeathVFX.gameObject, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                        vfx.Play();
                        
                        Destroy(vfx.gameObject, vfx.main.duration);
                    #endregion
                    
                    #region 播放受擊音效
                        AudioSystem.Instance.AttackSFX.PlayOneShot(battleCardData.UseSFXData);
                    #endregion
                    
                    animation.Dead(
                        onComplete: () =>
                        {
                            death = true;
                            
                            isDeath.Invoke();
                        });
                }
            #endregion

            #region 更新血條
                healthBar.Subtract(battleCardData.Damage);
            #endregion
        }
    }
}