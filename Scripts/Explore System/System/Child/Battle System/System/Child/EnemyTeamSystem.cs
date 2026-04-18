using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Value.Type;
using Explore_System.System.Child.Battle_System.Object;
using Explore_System.System.Child.Battle_System.Object.Card;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
using Explore_System.System.Child.Battle_System.Object.Creature.Character;
using Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Main;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Child
{
    internal sealed class EnemyTeamSystem : MonoBehaviour
    {
        [field: Header("敵人位置")]
        [field: SerializeField] private BattleEnemySlot[] battleEnemySlots;

        private IEnumerator _coroutine;

        private void Awake()
        {
            Character.OnAttack += Hurt;
        }

        private void OnDisable()
        {
            if (_coroutine is not null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private void OnDestroy()
        {
            Character.OnAttack -= Hurt;
        }

        #region Attack
            public void Attack(Action haveCharacterAlive, Action characterAllDead)
            {
                #region 必要條件檢查
                    if (_coroutine is not null)
                    {
                        StopCoroutine(_coroutine);
                        _coroutine = null;
                    }
                #endregion

                _coroutine = AttackCoroutine(haveCharacterAlive, characterAllDead);
                StartCoroutine(_coroutine);
            }

            private IEnumerator AttackCoroutine(Action haveCharacterAlive, Action characterAllDead)
            {
                var completes = new Dictionary<BattleEnemyObject, bool>();
                
                #region 獲取活著並且可以戰鬥的敵人
                    foreach (var enemySlot in battleEnemySlots)
                    {
                        var enemyObject = enemySlot.battleEnemyObject;

                        #region 條件檢查
                            if (enemyObject is null)
                            {
                                continue;
                            }

                            if (enemyObject.death)
                            {
                                continue;
                            }
                        #endregion
                        
                        completes.Add(enemyObject, false);
                    }
                #endregion
                
                #region 執行敵人的邏輯
                    foreach (var enemyObject in completes.Keys.ToArray())
                    {
                        var onThisEnemyAttackComplete = false;
                        
                        #region 敵人攻擊
                            enemyObject.Attack(
                                haveCharacterAlive: () =>
                                {
                                    onThisEnemyAttackComplete = true;
                                    completes[enemyObject] = true;
                                },
                                characterAllDead: () =>
                                {
                                    #region 終止異步協程
                                        StopCoroutine(_coroutine);
                                        _coroutine = null;
                                    #endregion
                                    
                                    characterAllDead.Invoke();
                                });
                        #endregion

                        yield return new WaitUntil(() => onThisEnemyAttackComplete);
                    }
                #endregion
                
                #region 敵人回合結束
                    yield return new WaitUntil(() => completes.All(c => c.Value));
                    
                    haveCharacterAlive.Invoke();
                #endregion
            }
        #endregion

        #region Hurt
            private void Hurt(Card card, Action haveEnemyAlive, Action enemyAllDead)
            {
                if (_coroutine is not null)
                {
                    StopCoroutine(_coroutine);
                    _coroutine = null;
                }

                _coroutine = HurtCoroutine(card, haveEnemyAlive, enemyAllDead);
                StartCoroutine(_coroutine);
            }

            private IEnumerator HurtCoroutine(Card card, Action haveEnemyAlive, Action enemyAllDead)
            {
                if (card.CardData is not BattleCardSO battleCardData)
                {
                    Debug.Log($"{nameof(HurtCoroutine)} 的 {nameof(card)} 不能傳入除了 {nameof(BattleCard)} 以外的卡片。");
                    yield break;
                }
                
                switch (battleCardData.AttackType)
                {
                    case AttackType.Single:
                    {
                        var complete = false;
                        
                        foreach (var enemySlot in battleEnemySlots)
                        {
                            #region 檢查這個位置是否有敵人
                                if (enemySlot.battleEnemyObject is null)
                                {
                                    continue;
                                }
                            #endregion
                
                            #region 檢查這個位置的敵人是否活著
                                if (enemySlot.battleEnemyObject.death)
                                {
                                    continue;
                                }
                            #endregion
                            
                            var enemyObject = enemySlot.battleEnemyObject;
                            
                            enemyObject.Hurt(
                                battleCardData: battleCardData,
                                isAlive: () =>
                                {
                                    haveEnemyAlive.Invoke();
                                    complete = true;
                                },
                                isDeath: () =>
                                {
                                    if (IsAnyEnemyAlive())
                                    {
                                        haveEnemyAlive.Invoke();
                                        complete = true;
                                    }
                                    else
                                    {
                                        enemyAllDead.Invoke();
                                        complete = true;
                                    }
                                });
                            
                            yield return new WaitUntil(() => complete);
                            break;
                        }
                        
                        break;
                    }
                    case AttackType.All:
                    {
                        var completes = new Dictionary<BattleEnemyObject, bool>();
                        
                        foreach (var enemySlot in battleEnemySlots)
                        {
                            #region 檢查這個位置是否有敵人
                                if (enemySlot.battleEnemyObject is null)
                                {
                                    continue;
                                }
                            #endregion

                            #region 檢查這個位置的敵人是否活著
                                if (enemySlot.battleEnemyObject.death)
                                {
                                    continue;
                                }
                            #endregion
                            
                            var enemyObject = enemySlot.battleEnemyObject;
                            
                            completes.Add(enemyObject, false);
                            
                            enemyObject.Hurt(battleCardData,
                                isAlive: () =>
                                {
                                    completes[enemyObject] = true;
                                },
                                isDeath: () =>
                                {
                                    completes[enemyObject] = true;
                                });

                            yield return new WaitForSeconds(.1f);
                        }
                        
                        yield return new WaitUntil(() => completes.All(c => c.Value));
                        
                        if (IsAnyEnemyAlive())
                        {
                            haveEnemyAlive.Invoke();
                        }
                        else
                        {
                            enemyAllDead.Invoke();
                        }
                        
                        break;
                    }
                }
                
                _coroutine = null;
            }
        #endregion

        private bool IsAnyEnemyAlive()
        {
            var isAnyAlive = false;
            
            foreach (var enemySlot in battleEnemySlots)
            {
                #region 檢查這個位置是否有敵人
                    if (enemySlot.battleEnemyObject is null)
                    {
                        continue;
                    }
                #endregion
                
                #region 檢查這個位置的敵人是否活著
                    if (enemySlot.battleEnemyObject.death)
                    {
                        continue;
                    }
                #endregion

                isAnyAlive = true;
                break;
            }

            return isAnyAlive;
        }
    }
}