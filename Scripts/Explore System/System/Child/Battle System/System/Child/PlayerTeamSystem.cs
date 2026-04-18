using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.Spine;
using Common.Data_Saver.Player_Character_Saver.Child;
using Common.Data_Saver.Player_Character_Saver.Main;
using Common.Enemy_Battle_Object.Main;
using Common.Enemy_Data;
using Common.Value.Type;
using Explore_System.System.Child.Battle_System.Object.Card;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
using Explore_System.System.Child.Battle_System.Object.Creature.Character;
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
        
        public static event Action<BattleCardType[], Action> RecycleCard;
        
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
            private void Attack(Card card, SpineAnimation anima, Action haveEnemyAlive, Action enemyAllDead)
            {
                if (card.CardData is not BattleCardSO battleCardData)
                {
                    Debug.Log($"{card.name} 不是 {nameof(BattleCardSO)}，無法在 {nameof(Attack)} 裡使用。");
                    return;
                }

                switch (battleCardData.BattleCardType)
                {
                    case BattleCardType.Fork:
                    {
                        bernard.Attack(card, anima, haveEnemyAlive, enemyAllDead);
                        break;
                    }
                    case BattleCardType.Spoon:
                    {
                        ray.Attack(card, anima, haveEnemyAlive, enemyAllDead);
                        break;
                    }
                }
            }
        #endregion

        #region Hurt
            private void Hurt(EnemySO enemyData, Action haveCharacterAlive, Action characterAllDead)
            {
                _hurtCoroutine = HurtCoroutine();
                StartCoroutine(_hurtCoroutine);
                return;
                
                IEnumerator HurtCoroutine()
                {
                    switch (enemyData.AttackType)
                    {
                        case AttackType.Single:
                        {
                            var character = AliveCharacters[0];
                            
                            character.Hurt(enemyData,
                                isAlive: () =>
                                {
                                    haveCharacterAlive?.Invoke();
                                },
                                isDeath: () =>
                                {
                                    AliveCharacters.Remove(character);
                                    RecycleCard?.Invoke(character.CharacterData.BattleCardTypes,
                                        () =>
                                        {
                                            if (AliveCharacters.Any())
                                            {
                                                haveCharacterAlive?.Invoke();
                                            }
                                            else
                                            {
                                                characterAllDead?.Invoke();
                                            }
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
                                character.Hurt(enemyData,
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

                                yield return new WaitForSeconds(.1f);
                            }

                            yield return new WaitUntil(() => completes.All(c => c));
                            
                            for (var ii = 0; ii < completes.Count; ii++)
                            {
                                completes[ii] = false;
                            }
                            
                            foreach (var (character, index) in deadCharacters)
                            {
                                RecycleCard?.Invoke(character.CharacterData.BattleCardTypes,
                                    () =>
                                    {
                                        // onComplete.
                                        completes[index] = true;
                                    });
                                yield return new WaitUntil(() => completes[index]);
                            }

                            if (isAnyCharacterAlive)
                            {
                                haveCharacterAlive?.Invoke();
                            }
                            else
                            {
                                characterAllDead?.Invoke();
                            }
                            
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