using System.Battle_System.Object.Card.Type.Battle.System.Main;
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
        [field: SerializeField] private List<EnemyBase> Enemies;

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
            public void Attack(Action onComplete = null)
            {
                if (CurrentCor is not null)
                {
                    StopCoroutine(CurrentCor);
                    CurrentCor = null;
                }

                CurrentCor = AttackCoroutine(onComplete);
                StartCoroutine(CurrentCor);
            }

            private IEnumerator AttackCoroutine(Action onComplete = null)
            {
                var enemies = Enemies.ToList();
                for (var i = enemies.Count; i > 0; i--)
                {
                    var randomIndex = UnityEngine.Random.Range(0, enemies.Count);
                    var enemy = enemies[randomIndex];
                    var attackComplete = false;
                    enemies.Remove(enemy);
                    enemy.Attack(() =>
                    {
                        attackComplete = true;
                        if (enemies.Count != 0) return;
                        onComplete?.Invoke();
                    });
                    yield return new WaitUntil(() => attackComplete);
                }

                CurrentCor = null;
            }
        #endregion

        #region Hurt
            private void Hurt(BattleCard card, Action haveEnemyAlive, Action enemyAllDeath)
            {
                if (CurrentCor is not null)
                {
                    StopCoroutine(CurrentCor);
                    CurrentCor = null;
                }

                CurrentCor = HurtCoroutine(card, haveEnemyAlive, enemyAllDeath);
                StartCoroutine(CurrentCor);
            }

            private IEnumerator HurtCoroutine(BattleCard card, Action haveEnemyAlive, Action enemyAllDeath)
            {
                card.GetDamage(out var damage);
                damage.GetValues(out var attackType, out var basicDamage);
                switch (attackType)
                {
                    case AttackType.Single:
                    {
                        var enemy = Enemies[0];
                        enemy.Hurt(basicDamage,
                            isAlive: () =>
                            {
                                haveEnemyAlive?.Invoke();
                            },
                            isDeath: () =>
                            {
                                Enemies.Remove(enemy);
                                if (Enemies.Any()) return;
                                enemyAllDeath?.Invoke();
                            });
                        break;
                    }
                    case AttackType.All:
                    {
                        var isAnyEnemyAlive = false;
                        var completes = new List<bool>();
                        for (var i = 0; i < Enemies.Count; i++)
                        {
                            var index = i;
                            var enemy = Enemies[index];
                            completes.Add(false);
                            enemy.Hurt(basicDamage,
                                isAlive: () =>
                                {
                                    isAnyEnemyAlive = true;
                                    completes[index] = true;
                                },
                                isDeath: () =>
                                {
                                    Enemies.Remove(enemy);
                                    completes[index] = true;
                                });
                        }
                        
                        yield return new WaitUntil(() => completes.All(c => c));
                        if (isAnyEnemyAlive)
                            haveEnemyAlive?.Invoke();
                        else
                            enemyAllDeath?.Invoke();
                        break;
                    }
                }
            }
        #endregion
    }
}