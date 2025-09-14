using System.Battle_System.Object.Card_Slot;
using System.Battle_System.Object.Card.Base;
using System.Battle_System.Object.Card.Type.Battle.System.Main;
using System.Collections;
using System.Collections.Generic;
using Data.DOTween.Basic;
using Data.Player;
using DG.Tweening;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class CardPoolSystem : MonoBehaviour
    {
        [field: Header("Parent")]
        [field: SerializeField] private Transform SpawnParent;
        
        [field: Header("Card Slot")]
        [field: SerializeField] private CardSlot[] CardSlots;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO playerData;
        
        private IEnumerator SortCor;
        private IEnumerator RefillCor;

        private const float RefillDuration = .25f;
        private readonly DoAnchorPos RefillAnimation = new(Vector3.zero, .5f, true, Ease.OutExpo);

        private void OnDisable()
        {
            if (SortCor is not null)
            {
                StopCoroutine(SortCor);
                SortCor = null;
            }
            
            if (RefillCor is not null)
            {
                StopCoroutine(RefillCor);
                RefillCor = null;
            }
        }
        
        public void Refill(Action onComplete = null)
        {
            SortCor = SortCoroutine();
            RefillCor = RefillCoroutine(onComplete);
            
            StartCoroutine(SortCor);
        }

        private IEnumerator SortCoroutine()
        {
            var remainingCards = new List<ICard>();

            foreach (var slot in CardSlots)
            {
                if (!slot.Get(out var card)) continue;
                remainingCards.Add(card);
            }

            switch (remainingCards.Count)
            {
                case 0:
                {
                    StartCoroutine(RefillCor);
                    SortCor = null;
                    
                    break;
                }
                case > 0:
                {
                    for (var i = 0; i < remainingCards.Count; i++)
                    {
                        var index = i;
                        var slot = CardSlots[index];
                        var card = remainingCards[index];

                        card.Move(slot.transform, RefillAnimation, () =>
                        {
                            if (index != remainingCards.Count - 1) return;
                            StartCoroutine(RefillCor);
                            SortCor = null;
                        });

                        yield return new WaitForSeconds(RefillDuration);
                    }

                    break;
                }
            }
        }
        
        private IEnumerator RefillCoroutine(Action onComplete = null)
        {
            for (var i = 0; i < CardSlots.Length; i++)
            {
                var index = i;
                var slot = CardSlots[index];
                
                if (!slot.IsEmpty()) continue;

                if (playerData.GetRandomBattleCard(out var battleCardPrefab))
                {
                    var card = Instantiate(battleCardPrefab, SpawnParent);
                    var battleCard = card.GetComponent<BattleCard>();

                    slot.Set(battleCard);
                    battleCard.Move(slot.transform, RefillAnimation, () =>
                    {
                        if (index != CardSlots.Length - 1) return;
                        onComplete?.Invoke();
                        RefillCor = null;
                    });

                    yield return new WaitForSeconds(RefillDuration);
                }
                else
                {
                    // TODO 強制退出玩家到主介面，並重新 Reload 玩家的資料
                    throw new Exception("PlayerBattleDeckData.GetRandomBattleCard() is null.");
                }
            }
        }
        
        public void DrawCard(int number, out List<ICard> cards)
        {
            number = Mathf.Clamp(number, 0, CardSlots.Length);
            
            cards = new List<ICard>();
            
            for (var i = 0; i < number; i++)
            {
                var slot = CardSlots[i];
                slot.Get(out var card);
                cards.Add(card);
            }
        }
    }
}