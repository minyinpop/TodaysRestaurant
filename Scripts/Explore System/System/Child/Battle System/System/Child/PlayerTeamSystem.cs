using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.Spine;
using Common.Data_Saver.Player_Character_Saver.Child;
using Common.Data_Saver.Player_Character_Saver.Main;
using Common.Value;
using Common.Value.Type;
using Explore_System.System.Child.Battle_System.Object.Card;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
using Explore_System.System.Child.Battle_System.Object.Creature.Character;
using Explore_System.System.Child.Battle_System.Object.Creature.Enemy;
using Explore_System.System.Child.Battle_System.Object.Creature.Enemy.Main;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Child
{
    internal sealed class PlayerTeamSystem : MonoBehaviour
    {
        [field: Header("Character")]
        [field: SerializeField] private Character bernard;
        [field: SerializeField] private Character ray;
        [field: SerializeField] private Character muu;

        public /*readonly*/ List<Character> AliveCharacters = new();
        
        public static event Action<CardType[], Action> RecycleCard;
        
        private IEnumerator _hurtCoroutine;

        private void Awake()
        {
            BattleCard.OnUse += Attack;
            BattleEnemyObject.OnAttack += Hurt;
        }
        
        private void OnDisable()
        {
            if (_hurtCoroutine is not null)
            {
                StopCoroutine(_hurtCoroutine);
                _hurtCoroutine = null;
            }
        }
        
        private void OnDestroy()
        {
            BattleCard.OnUse -= Attack;
            BattleEnemyObject.OnAttack -= Hurt;
        }
        
        #region Attack
            private void Attack(ICard card, SpineAnimation anima, Action haveEnemyAlive, Action enemyAllDead)
            {
                card.GetCardType(out var cardType);
                switch (cardType)
                {
                    case CardType.BattleCard_Fork:
                    {
                        bernard.Attack(card, anima, haveEnemyAlive, enemyAllDead);
                        break;
                    }
                    case CardType.BattleCard_Spoon:
                    {
                        ray.Attack(card, anima, haveEnemyAlive, enemyAllDead);
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
                                    RecycleCard?.Invoke(character.CharacterData.CardTypes,
                                        () =>
                                        {
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
                                RecycleCard?.Invoke(character.CharacterData.CardTypes,
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

        public void SaveData()
        {
            #region 儲存資料
            var saveData = new CharacterSaveData(
                characterType: bernard.CharacterData.CharacterType,
                health: bernard.CharacterData.Health);
            
            PlayerCharacterSaver.SaveCharacterToLocal(saveData);
            #endregion
        }
    }
}