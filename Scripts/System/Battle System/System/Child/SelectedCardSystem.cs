using System.Battle_System.Object.Card_Slot;
using System.Battle_System.Object.Card.Base;
using System.Collections;
using System.Collections.Generic;
using Data.DOTween.Basic;
using Data.Player;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace System.Battle_System.System.Child
{
    internal sealed class SelectedCardSystem : MonoBehaviour
    {
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        [field: Header("Card Order")]
        [field: SerializeField] private List<GameObject> CardOrderPrefabs;
        
        [field: Header("Object")]
        [field: SerializeField] private GameObject SelectedCardUI;
        [field: SerializeField] private Button ConfirmButton;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private readonly List<CardSlot> CardSlots = new();

        public static event Action<ICard> ReturnCardToHand;

        private void OnEnable()
        {
            HandCardSystem.TryAddCardToSelected += TryAdd;
            ConfirmButton.onClick.AddListener(OnConfirmButtonClick);
        }
        
        private void OnDisable()
        {
            HandCardSystem.TryAddCardToSelected -= TryAdd;
            ConfirmButton.onClick.RemoveListener(OnConfirmButtonClick);
        }

        #region SelectedCardUI
            public void OpenSelectedCardUI()
            {
                SelectedCardUI.SetActive(true);
                PlayerData.GetActiveCharacterNumber(out var number);
                for (var i = 0; i < number; i++)
                {
                    var slot = Instantiate(SlotPrefab, SpawnParent);
                    var slotScript = slot.GetComponent<CardSlot>();
                    CardSlots.Add(slotScript);
                }
            }

            public void CloseSelectedCardUI()
            {
                SelectedCardUI.SetActive(false);
            }
        #endregion

        private bool TryAdd(ICard card)
        {
            for (var i = 0; i < CardSlots.Count; i++)
            {
                var slot = CardSlots[i];
                if (!slot.IsEmpty()) continue;
                slot.Set(card);
                card.SetCardOrder(CardOrderPrefabs[i]);
                card.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo));
                card.OnClick += OnCardClicked;
                return true;
            }

            return false;
        }

        private void OnCardClicked(ICard card)
        {
            card.OnClick -= OnCardClicked;
            card.RemoveCardOrder();
            ReturnCardToHand?.Invoke(card);
            for (var i = 0; i < CardSlots.Count; i++)
            {
                var slot = CardSlots[i];
                if (!slot.Compare(card)) continue;
                CardSlots.Remove(slot);
                Destroy(slot.gameObject);
                break;
            }
                    
            var newSlot = Instantiate(SlotPrefab, SpawnParent);
            var newSlotScript = newSlot.GetComponent<CardSlot>();
            CardSlots.Add(newSlotScript);
            for (var i = 0; i < CardSlots.Count; i++)
            {
                var slot = CardSlots[i];
                if (slot.IsEmpty()) continue;
                slot.Get(out var thisCard);
                thisCard.SetCardOrder(CardOrderPrefabs[i]);
                slot.Set(thisCard);
            }
        }
        
        private void OnConfirmButtonClick()
        {
            for (var i = 0; i < CardSlots.Count; i++)
            {
                var slot = CardSlots[i];
                if (slot.IsEmpty())
                {
                    Debug.Log("還有卡片可以選擇");
                    return;
                }

                if (i == CardSlots.Count - 1)
                {
                    CloseSelectedCardUI();
                }
            }
        }

        private IEnumerator RENAME()
        {
            foreach (var slot in CardSlots)
            {
                slot.Get(out var card);
                // card.
            }

            yield break;
        }
    }
}