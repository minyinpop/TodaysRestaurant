using System.Battle.Object.Card.Base;
using System.Battle.Object.Card.Type.Battle;
using System.Battle.Object.Mob.Type.Character.Base;
using System.Battle.Object.Mob.Type.Enemy.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.Animation.Spine;
using Data.General;
using Data.General.Damage.Base;
using Data.General.Damage.Child;
using UnityEngine;

namespace System.Battle.System.Child
{
    internal sealed class CharacterTeamSystem : MonoBehaviour
    {
        [field: Header("Character")]
        [field: SerializeField] private CharacterBase Bernard;
        [field: SerializeField] private CharacterBase Ray;
        [field: SerializeField] private CharacterBase Muu;

        private readonly List<CharacterBase> AliveCharacters = new();
        
        public static event Action<List<CardType>, Action> RecycleCard;
        
        private IEnumerator HurtCor;

        private void Start()
        {
            AliveCharacters.Add(Bernard);
            AliveCharacters.Add(Ray);
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
            if (HurtCor is not null)
            {
                StopCoroutine(HurtCor);
                HurtCor = null;
            }
        }

        #region Attack
            private void Attack(ICard card, SkeletonAnimationSettings settings, Action haveEnemyAlive, Action enemyAllDead)
            {
                card.GetCardType(out var cardType);
                switch (cardType)
                {
                    case CardType.BattleCard_Fork:
                    {
                        Bernard.Attack(card, settings, haveEnemyAlive, enemyAllDead);
                        break;
                    }
                    case CardType.BattleCard_Spoon:
                    {
                        Ray.Attack(card, settings, haveEnemyAlive, enemyAllDead);
                        break;
                    }
                }
            }
        #endregion

        #region Hurt
            private void Hurt(Damage damage, Action haveCharacterAlive, Action characterAllDead)
            {
                HurtCor = HurtCoroutine();
                StartCoroutine(HurtCor);
                return;
                
                IEnumerator HurtCoroutine()
                {
                    damage.GetValues(out var attackType, out var basicDamage);
                    switch (attackType)
                    {
                        case AttackType.Single:
                        {
                            var character = AliveCharacters[0];
                            character.Hurt(basicDamage,
                                isAlive: () =>
                                {
                                    haveCharacterAlive?.Invoke();
                                },
                                isDeath: () =>
                                {
                                    AliveCharacters.Remove(character);
                                    character.GetCharacterData(out var characterData);
                                    characterData.GetUseCardType(out var cardTypes);
                                    RecycleCard?.Invoke(cardTypes,
                                        () =>
                                        {
                                            // onComplete
                                            if (AliveCharacters.Any())
                                                haveCharacterAlive?.Invoke();
                                            else
                                                characterAllDead?.Invoke();
                                        });
                                });
                            yield break;
                        }
                        case AttackType.All:
                        {
                            var isAnyCharacterAlive = false;
                            var completes = new List<bool>();
                            var deadCharacters = new Dictionary<CharacterBase, int>();
                            for (var i = 0; i < AliveCharacters.Count; i++)
                            {
                                var index = i;
                                var character = AliveCharacters[index];
                                completes.Add(false);
                                character.Hurt(basicDamage,
                                    isAlive: () =>
                                    {
                                        isAnyCharacterAlive = true;
                                        completes[index] = true;
                                    },
                                    isDeath: () =>
                                    {
                                        AliveCharacters.Remove(character);
                                        deadCharacters.Add(character, index);
                                    });
                                Debug.Log($"{character.name} is dead.");
                                Debug.Log(deadCharacters.Any());
                            }

                            foreach (var (character, index) in deadCharacters)
                            {
                                character.GetCharacterData(out var characterData);
                                characterData.GetUseCardType(out var cardTypes);
                                RecycleCard?.Invoke(cardTypes,
                                    () =>
                                    {
                                        // onComplete.
                                        completes[index] = true;
                                    });
                                yield return new WaitUntil(() => completes[index]);
                            }
                            
                            yield return new WaitUntil(() => completes.All(c => c));
                            if (isAnyCharacterAlive)
                                haveCharacterAlive?.Invoke();
                            else
                                characterAllDead?.Invoke();
                            yield break;
                        }
                    }

                    HurtCor = null;
                }
            }
        #endregion
    }
}