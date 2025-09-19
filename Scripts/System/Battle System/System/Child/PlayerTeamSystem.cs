using System.Battle_System.Object.Card.Base.Card_Type;
using System.Battle_System.Object.Card.Type.Battle.System.Main;
using System.Battle_System.Object.Mob.Type.Character.Base;
using System.Battle_System.Object.Mob.Type.Enemy.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Animation.Spine;
using Data.General.Damage.Base;
using Data.General.Damage.Child;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class PlayerTeamSystem : MonoBehaviour
    {
        [field: Header("Character")]
        [field: SerializeField] private CharacterBase Bernard;
        [field: SerializeField] private CharacterBase Ray;
        [field: SerializeField] private CharacterBase Muu;

        private readonly List<CharacterBase> CharacterOrder = new();
        
        private IEnumerator CurrentCor;

        public static event Action OnAllDeath;

        private void Start()
        {
            CharacterOrder.Add(Bernard);
            CharacterOrder.Add(Ray);
            // CharacterOrder.Add(Muu);
        }

        private void OnEnable()
        {
            BattleCard.OnUse += Attack;
            EnemyBase.OnAttack += Hurt;
        }
        
        private void OnDisable()
        {
            BattleCard.OnUse -= Attack;
            EnemyBase.OnAttack -= Hurt;
            if (CurrentCor is not null)
            {
                StopCoroutine(CurrentCor);
                CurrentCor = null;
            }
        }

        #region Attack
            private void Attack(BattleCard card, SkeletonAnimationSettings settings, Action haveEnemyAlive, Action enemyAllDeath)
            {
                switch (card)
                {
                    case IForkCard:
                    {
                        Bernard.Attack(card, settings, haveEnemyAlive, enemyAllDeath);
                        break;
                    }
                    case ISpoonCard:
                    {
                        Ray.Attack(card, settings, haveEnemyAlive, enemyAllDeath);
                        break;
                    }
                }
            }
        #endregion

        #region Hurt
            private void Hurt(Damage damage, Action onComplete = null)
            {
                if (CurrentCor is not null)
                {
                    StopCoroutine(CurrentCor);
                    CurrentCor = null;
                }
                
                CurrentCor = HurtCoroutine(damage, onComplete);
                StartCoroutine(CurrentCor);
            }

            private IEnumerator HurtCoroutine(Damage damage, Action onComplete = null)
            {
                damage.GetValues(out var attackType, out var basicDamage);
                switch (attackType)
                {
                    case AttackType.Single:
                    {
                        CharacterOrder[0].Hurt(basicDamage,
                            isDeath: () =>
                            {
                                CharacterOrder.Remove(CharacterOrder[0]);
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
                        for (var i = 0; i < CharacterOrder.Count; i++)
                        {
                            var index = i;
                            var character = CharacterOrder[index];
                            completes.Add(false);
                            character.Hurt(basicDamage, 
                                isDeath: () =>
                                {
                                    CharacterOrder.Remove(character);
                                    if (CharacterOrder.Any()) return;
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