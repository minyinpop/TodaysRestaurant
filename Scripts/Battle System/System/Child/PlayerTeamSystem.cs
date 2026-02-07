using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.Spine;
using Battle_System.Object.Card;
using Battle_System.Object.Card.Battle;
using Battle_System.Object.Creature.Character;
using Battle_System.Object.Creature.Enemy;
using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Battle_System.System.Child
{
    internal sealed class PlayerTeamSystem : MonoBehaviour
    {
        [field: Header("Character")]
        [field: SerializeField] private Character Bernard;
        [field: SerializeField] private Character Ray;
        [field: SerializeField] private Character Muu;

        public /*readonly*/ List<Character> AliveCharacters = new();
        
        public static event Action<CardType[], Action> RecycleCard;
        
        private IEnumerator _hurtCoroutine;

        private void OnEnable()
        {
            BattleCard.OnUse += Attack;
            Enemy.OnAttack += Hurt;
        }
        
        private void OnDisable()
        {
            BattleCard.OnUse -= Attack;
            Enemy.OnAttack -= Hurt;
            if (_hurtCoroutine is not null)
            {
                StopCoroutine(_hurtCoroutine);
                _hurtCoroutine = null;
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
                _hurtCoroutine = HurtCoroutine();
                StartCoroutine(_hurtCoroutine);
                return;
                
                IEnumerator HurtCoroutine()
                {
                    switch (damage.AttackType)
                    {
                        case AttackType.Single:
                        {
                            var character = AliveCharacters[0];
                            character.Hurt(damage.BasicDamage,
                                isAlive: () =>
                                {
                                    haveCharacterAlive?.Invoke();
                                },
                                isDeath: () =>
                                {
                                    AliveCharacters.Remove(character);
                                    character.GetCharacterData(out var characterData);
                                    RecycleCard?.Invoke(characterData.CardTypes,
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
                            var deadCharacters = new Dictionary<Character, int>();
                            for (var i = 0; i < AliveCharacters.Count; i++)
                            {
                                var index = i;
                                var character = AliveCharacters[index];
                                completes.Add(false);
                                character.Hurt(damage.BasicDamage,
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
                                RecycleCard?.Invoke(characterData.CardTypes,
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

                    _hurtCoroutine = null;
                }
            }
        #endregion
    }
}