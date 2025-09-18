using System.Battle_System.Object.Card.Type.Battle.System.Main;
using System.Battle_System.Object.Character.Type.Enemy.Base;
using System.Battle_System.Object.Character.Type.Friendly.Base;
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

        public static event Action OnAllDeath;
        
        private void OnEnable()
        {
            FriendlyBase.OnAttack += Hurt;
        }

        private void OnDisable()
        {
            FriendlyBase.OnAttack -= Hurt;
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
            private void Hurt(BattleCard card, Action onComplete = null)
            {
                if (CurrentCor is not null)
                {
                    StopCoroutine(CurrentCor);
                    CurrentCor = null;
                }

                CurrentCor = HurtCoroutine(card, onComplete);
                StartCoroutine(CurrentCor);
            }

            private IEnumerator HurtCoroutine(BattleCard card, Action onComplete = null)
            {
                card.GetDamage(out var damage);
                damage.GetValues(out var attackType, out var basicDamage);
                switch (attackType)
                {
                    case AttackType.Single:
                    {
                        Enemies[0].Hurt(basicDamage,
                            isDeath: () =>
                            {
                                Enemies.Remove(Enemies[0]);
                            },
                            onComplete: () =>
                            {
                                onComplete?.Invoke();
                            });
                        yield break;
                    }
                    case AttackType.All:
                    {
                        var completes = new List<bool>();
                        for (var i = 0; i < Enemies.Count; i++)
                        {
                            var index = i;
                            var enemy = Enemies[index];
                            completes.Add(false);
                            enemy.Hurt(basicDamage,
                                isDeath: () =>
                                {
                                    Enemies.Remove(enemy);
                                    if (Enemies.Any()) return;
                                    OnAllDeath?.Invoke();
                                },
                                onComplete: () =>
                                {
                                    completes[index] = true;
                                });
                        }

                        yield return new WaitUntil(() => completes.All(c => c));
                        onComplete?.Invoke();
                        yield break;
                    }
                }

                CurrentCor = null;
            }
        #endregion
    }
}