using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.DOTween.Basic;
using Common.Value.Type;
using DG.Tweening;
using Explore_System.System.Child.Battle_System.Object.Card_Slot;
using Explore_System.System.Child.Battle_System.Object.Card;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Child
{
    internal sealed class UseCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;

        private readonly List<CardSlot> CardSlots = new();

        private IEnumerator AddCor;
        private IEnumerator UseCor;
        private IEnumerator RecycleCor;

        private void OnEnable()
        {
            SelectedCardSystem.BeforeCloseUI += Add;
        }

        private void OnDisable()
        {
            SelectedCardSystem.BeforeCloseUI -= Add;
            if (UseCor is not null)
            {
                StopCoroutine(UseCor);
                UseCor = null;
            }
            
            if (RecycleCor is not null)
            {
                StopCoroutine(RecycleCor);
                RecycleCor = null;
            }
            
            if (AddCor is not null)
            {
                StopCoroutine(AddCor);
                AddCor = null;
            }
        }

        #region Add
            private void Add(List<Card> cards, Action onComplete = null)
            {
                AddCor = AddCoroutine();
                StartCoroutine(AddCor);
                return;

                IEnumerator AddCoroutine()
                {
                    var completes = new List<bool>();
                    
                    for (var i = 0; i < cards.Count; i++)
                    {
                        completes.Add(false);
                        
                        var index = i;
                        var slot = Instantiate(SlotPrefab, SpawnParent);
                        var slotScript = slot.GetComponent<CardSlot>();
                        var card = cards[index];

                        if (card is not BattleCard battleCard)
                        {
                            Debug.Log($"{card.name} 不是 {nameof(BattleCard)}，無法在 {nameof(UseCardSystem)} 裡使用，已自動跳過該卡片。");
                            continue;
                        }

                        CardSlots.Add(slotScript);
                        
                        slotScript.Set(battleCard);
                        
                        battleCard.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo),
                            onComplete: () =>
                            {
                                completes[index] = true;
                            });
                    }

                    yield return new WaitUntil(() => completes.All(c => c));
                    onComplete?.Invoke();
                }
            }
        #endregion
        
        #region Use
            public void Use(Action<bool> haveEnemyAlive, Action enemyAllDeath)
            {
                UseCor = UseCoroutine(haveEnemyAlive, enemyAllDeath);
                StartCoroutine(UseCor);
            }

            private IEnumerator UseCoroutine(Action<bool> haveEnemyAlive, Action enemyAllDeath)
            {
                var onUseComplete = false;
                var haveAnyEnemyAlive = false;
                
                var slot = CardSlots[0];
                
                slot.Get(out var card);
                card.Use(
                    haveEnemyAlive: () =>
                    {
                        onUseComplete = true;
                        haveAnyEnemyAlive = true;
                    },
                    enemyAllDeath: () =>
                    {
                        onUseComplete = true;
                    });
                card.DestroyCard(
                    onComplete: () =>
                    {
                        CardSlots.Remove(slot);
                        Destroy(slot.gameObject);
                    });
                
                yield return new WaitUntil(() => onUseComplete);
                yield return new WaitForSeconds(.25f);
                
                if (haveAnyEnemyAlive)
                {
                    haveEnemyAlive?.Invoke(CardSlots.Any());
                }
                else
                {
                    enemyAllDeath?.Invoke();
                }
                
                UseCor = null;
            }
        #endregion
        
        #region Recycle
            public void RecycleCard(BattleCardType targetType, Action onComplete)
            {
                RecycleCor = RecycleCardCoroutine();
                StartCoroutine(RecycleCor);
                return;
                
                IEnumerator RecycleCardCoroutine()
                {
                    var completes = new List<bool>();
                    
                    foreach (var slot in CardSlots)
                    {
                        slot.Get(out var card);
                        
                        if (card is not BattleCard battleCard)
                        {
                            slot.Set(card);
                            continue;
                        }
                        
                        if (battleCard.BattleCardData.BattleCardType != targetType)
                        {
                            slot.Set(card);
                            continue;
                        }
                        
                        completes.Add(false);
                        
                        card.DestroyCard(
                            onComplete: () =>
                            {
                                completes[completes.Count - 1] = true;
                            });
                    }
                    
                    yield return new WaitUntil(() => completes.All(c => c));
                    onComplete.Invoke();
                }
            }
        #endregion
    }
}