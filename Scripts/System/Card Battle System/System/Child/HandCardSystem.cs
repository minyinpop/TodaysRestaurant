using System.Card_Battle_System.Object.Card_Slot;
using System.Card_Battle_System.Object.Card.Base;
using System.Card_Battle_System.Object.Card.Type.Battle.System.Main;
using System.Collections;
using System.Collections.Generic;
using Data.DOTween.Basic;
using DG.Tweening;
using UnityEngine;

namespace System.Card_Battle_System.System.Child
{
    internal sealed class HandCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        [field: Header("Card Order")]
        [field: SerializeField] private List<GameObject> CardOrderPrefabs;
        
        private readonly List<CardSlot> CardSlots = new();
        private readonly List<BattleCard> SelectedCards = new();
        
        private const int MaxSelectedCards = 3;
        
        private IEnumerator AddCor;

        private void OnDisable()
        {
            if (AddCor is not null)
            {
                StopCoroutine(AddCor);
                AddCor = null;
            }
        }
        
        public void SetCardsInteractable(bool interactable)
        {
            foreach (var cardSlot in CardSlots)
                cardSlot.SetInteractable(interactable);
        }

        private void OnClickCard(ICard card)
        {
            if (card is not BattleCard battleCard) return;
            if (SelectedCards.Count >= MaxSelectedCards) return;

            SelectedCards.Add(battleCard);
            var orderPrefab = CardOrderPrefabs[SelectedCards.Count - 1];
            battleCard.SetCardOrder(orderPrefab);
        }

        #region Add
            public void Add(List<ICard> cards, Action onComplete = null)
            {
                AddCor = AddCoroutine(cards, onComplete);
                StartCoroutine(AddCor);
            }

            private IEnumerator AddCoroutine(List<ICard> cards, Action onComplete = null)
            {
                for (var i = 0; i < cards.Count; i++)
                {
                    var index = i;
                    var slot = Instantiate(SlotPrefab, SpawnParent);
                    var slotScript = slot.GetComponent<CardSlot>();
                    var card = cards[index];
                    
                    CardSlots.Add(slotScript);
                    slotScript.Set(card);
                    card.MoveToParent(slot.transform, new DoAnchorPos(Vector3.zero, .5f, true, Ease.OutExpo), () =>
                    {
                        if (index != cards.Count - 1) return;
                        onComplete?.Invoke();
                        AddCor = null;
                    });
                    
                    card.OnClick += OnClickCard;

                    yield return new WaitForSeconds(.25f);
                }
            }

            #endregion
    }
}