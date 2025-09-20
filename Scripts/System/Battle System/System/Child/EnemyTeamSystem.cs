using System.Battle_System.Object.Card.Base;
using System.Battle_System.Object.Mob.Type.Character.Base;
using System.Battle_System.Object.Mob.Type.Enemy.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.General.Damage.Child;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class EnemyTeamSystem : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private List<EnemyBase> AliveEnemies;

        private IEnumerator CurrentCor;
        
        private void OnEnable()
        {
            CharacterBase.OnAttack += Hurt;
        }

        private void OnDisable()
        {
            CharacterBase.OnAttack -= Hurt;
            if (CurrentCor is not null)
            {
                StopCoroutine(CurrentCor);
                CurrentCor = null;
            }
        }

        #region Attack
            public void Attack(Action haveCharacterAlive, Action characterAllDead)
            {
                if (CurrentCor is not null)
                {
                    StopCoroutine(CurrentCor);
                    CurrentCor = null;
                }

                CurrentCor = AttackCoroutine(haveCharacterAlive, characterAllDead);
                StartCoroutine(CurrentCor);
            }

            private IEnumerator AttackCoroutine(Action haveCharacterAlive, Action characterAllDead)
            {
                var enemies = AliveEnemies.ToList();
                for (var i = enemies.Count; i > 0; i--)
                {
                    var enemy = enemies.OrderBy(_ => UnityEngine.Random.value).First();
                    var attackComplete = false;
                    enemies.Remove(enemy);
                    enemy.Attack(
                        haveCharacterAlive: () =>
                        {
                            if (enemies.Any())
                                attackComplete = true;
                            else
                            {
                                StopCoroutine(CurrentCor);
                                CurrentCor = null;
                                haveCharacterAlive?.Invoke();
                            }
                        },
                        characterAllDead: () =>
                        {
                            StopCoroutine(CurrentCor);
                            CurrentCor = null;
                            characterAllDead?.Invoke();
                        });
                    yield return new WaitUntil(() => attackComplete);
                }
            }
        #endregion

        #region Hurt
            private void Hurt(ICard card, Action haveEnemyAlive, Action enemyAllDead)
            {
                if (CurrentCor is not null)
                {
                    StopCoroutine(CurrentCor);
                    CurrentCor = null;
                }

                CurrentCor = HurtCoroutine(card, haveEnemyAlive, enemyAllDead);
                StartCoroutine(CurrentCor);
            }

            private IEnumerator HurtCoroutine(ICard card, Action haveEnemyAlive, Action enemyAllDead)
            {
                card.GetDamage(out var damage);
                damage.GetValues(out var attackType, out var basicDamage);
                switch (attackType)
                {
                    case AttackType.Single:
                    {
                        var enemy = AliveEnemies[0];
                        enemy.Hurt(basicDamage,
                            isAlive: () =>
                            {
                                haveEnemyAlive?.Invoke();
                            },
                            isDeath: () =>
                            {
                                AliveEnemies.Remove(enemy);
                                if (AliveEnemies.Any())
                                    haveEnemyAlive?.Invoke();
                                else
                                    enemyAllDead?.Invoke();
                            });
                        break;
                    }
                    case AttackType.All:
                    {
                        var isAnyEnemyAlive = false;
                        var completes = new List<bool>();
                        for (var i = 0; i < AliveEnemies.Count; i++)
                        {
                            var index = i;
                            var enemy = AliveEnemies[index];
                            completes.Add(false);
                            enemy.Hurt(basicDamage,
                                isAlive: () =>
                                {
                                    isAnyEnemyAlive = true;
                                    completes[index] = true;
                                },
                                isDeath: () =>
                                {
                                    AliveEnemies.Remove(enemy);
                                    completes[index] = true;
                                });
                        }
                        
                        yield return new WaitUntil(() => completes.All(c => c));
                        if (isAnyEnemyAlive)
                            haveEnemyAlive?.Invoke();
                        else
                            enemyAllDead?.Invoke();
                        break;
                    }
                }
                
                CurrentCor = null;
            }
        #endregion
    }
}