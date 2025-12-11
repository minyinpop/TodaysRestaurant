using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.Spine;
using Battle_System.Object.Card;
using Battle_System.Object.Card.Battle;
using Battle_System.Object.Creature.Crew;
using Battle_System.Object.Creature.Enemy;
using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Battle_System.System.Child
{
    internal sealed class CrewTeamSystem : MonoBehaviour
    {
        [field: Header("Character")]
        [field: SerializeField] private Crew Bernard;
        [field: SerializeField] private Crew Ray;
        [field: SerializeField] private Crew Muu;

        public /*readonly*/ List<Crew> AliveCharacters = new();
        
        public static event Action<List<CardType>, Action> RecycleCard;
        
        private IEnumerator HurtCor;

        private void Start()
        {
            // AliveCharacters.Add(Bernard);
            // AliveCharacters.Add(Ray);
            // CharacterOrder.Add(Muu);
        }

        private void OnEnable()
        {
            BattleCard.OnUse += Attack;
            Enemy.OnAttack += Hurt;
        }
        
        private void OnDisable()
        {
            BattleCard.OnUse -= Attack;
            Enemy.OnAttack -= Hurt;
            if (HurtCor is not null)
            {
                StopCoroutine(HurtCor);
                HurtCor = null;
            }
        }

        #region Attack
            private void Attack(ICard card, SpineAnimation anima, Action haveEnemyAlive, Action enemyAllDead)
            {
                card.GetCardType(out var cardType);
                switch (cardType)
                {
                    case CardType.BattleCard_Fork:
                    {
                        Bernard.Attack(card, anima, haveEnemyAlive, enemyAllDead);
                        break;
                    }
                    case CardType.BattleCard_Spoon:
                    {
                        Ray.Attack(card, anima, haveEnemyAlive, enemyAllDead);
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
                            var deadCharacters = new Dictionary<Crew, int>();
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
                                        completes[index] = true;
                                    });
                            }

                            yield return new WaitUntil(() => completes.All(c => c));
                            for (var i = 0; i < completes.Count; i++)
                                completes[i] = false;
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