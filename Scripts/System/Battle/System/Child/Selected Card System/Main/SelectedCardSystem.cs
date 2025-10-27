using System.Battle.Object.Card_Slot;
using System.Battle.Object.Card.Base;
using System.Battle.System.Child.Selected_Card_System.Child;
using System.Collections;
using System.Collections.Generic;
using System.Message.System.Main;
using Data.Animation.DOTween.Basic;
using Data.General;
using Data.Player;
using Data.Player.Main;
using DG.Tweening;
using UnityEngine;

namespace System.Battle.System.Child.Selected_Card_System.Main
{
    [RequireComponent(typeof(AnimationSystem))]
    internal sealed class SelectedCardSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
        
        [field: Header("Card Slot")]
        [field: SerializeField] private Transform SpawnParent;
        [field: SerializeField] private GameObject SlotPrefab;
        
        [field: Header("Card Order")]
        [field: SerializeField] private List<GameObject> CardOrderPrefabs;
        
        [field: Header("Object")]
        [field: SerializeField] private CanvasGroup UICanvasGroup;
        [field: SerializeField] private Button ConfirmButton;
        [field: SerializeField] private MessageSystem MessageSystem;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private readonly List<CardSlot> CardSlots = new();
        private readonly List<ICard> SelectedCards = new();

        public static event Action<ICard> ReturnCardToHand;
        public static event Action<List<ICard>, Action> BeforeCloseUI;
        public event Action AfterCloseUI;

        private IEnumerator OnClickConfirmButtonCor;

        private void Start()
        {
            PlayerData.GetCharacterNumber(out var number);
            for (var i = 0; i < number; i++)
            {
                var slot = Instantiate(SlotPrefab, SpawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                CardSlots.Add(slotScript);
            }
        }

        private void OnEnable()
        {
            ConfirmButton.OnClick += OnConfirmButtonClick;
            HandCardSystem.TryAddCardToSelected += TryAdd;
        }
        
        private void OnDisable()
        {
            ConfirmButton.OnClick -= OnConfirmButtonClick;
            HandCardSystem.TryAddCardToSelected -= TryAdd;
            if (OnClickConfirmButtonCor is not null)
            {
                StopCoroutine(OnClickConfirmButtonCor);
                OnClickConfirmButtonCor = null;
            }
        }

        #region UI
            public void OpenUI(Action onUIOpen = null, Action onUIClose = null)
            {
                UICanvasGroup.gameObject.SetActive(true);
                AfterCloseUI = onUIClose;

                AnimationSystem.FadeIn()
                    .OnComplete(() =>
                    {
                        ConfirmButton.SetInteractable(true);
                        onUIOpen?.Invoke();
                    });
            }

            private void CloseUI()
            {
                ConfirmButton.SetInteractable(false);
                foreach (var card in SelectedCards)
                    card.SetInteractable(false);
                BeforeCloseUI?.Invoke(SelectedCards, () => AnimationSystem.FadeOut()
                    .OnComplete(() =>
                    {
                        AfterCloseUI?.Invoke();
                        SelectedCards.Clear();
                        UICanvasGroup.gameObject.SetActive(false);
                    }));
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
            OnClickConfirmButtonCor = OnConfirmButtonClickCoroutine();
            StartCoroutine(OnClickConfirmButtonCor);
            return;

            IEnumerator OnConfirmButtonClickCoroutine()
            {
                var onConfirm = false;
                var onClose = false;
                var selectedCards = new List<ICard>();
                foreach (var slot in CardSlots)
                {
                    if (slot.IsEmpty()) continue;
                    slot.Get(out var card);
                    selectedCards.Add(card);
                }

                if (selectedCards.Count == CardSlots.Count)
                {
                    foreach (var card in selectedCards)
                        SelectedCards.Add(card);
                    CloseUI();
                    OnClickConfirmButtonCor = null;
                    yield break;
                }

                if (selectedCards.Count == 0)
                {
                    ConfirmButton.SetInteractable(false);
                    MessageSystem.ShowTipUI(
                        content: new PopUpUIContent(
                            message:"請選擇至少一張卡牌",
                            confirmButtonTitle: "確定",
                            cancelButtonTitle: string.Empty,
                            closeButtonTitle: string.Empty),
                        onConfirm: () =>
                        {
                            ConfirmButton.SetInteractable(true);
                        });
                }
                else
                {
                    MessageSystem.ShowSwitchUI(
                        content: new PopUpUIContent(
                            message: "還可以選擇卡片\n確定要直接開始戰鬥嗎？",
                            confirmButtonTitle: "確定",
                            cancelButtonTitle: "返回",
                            closeButtonTitle: string.Empty),
                        onShow: () =>
                        {
                            foreach (var card in selectedCards)
                                card.SetInteractable(false);
                        },
                        onConfirm: () =>
                        {
                            foreach (var card in selectedCards)
                                SelectedCards.Add(card);
                            onConfirm = true;
                        },
                        onCancel: () =>
                        {
                            for (var i = 0; i < selectedCards.Count; i++)
                            {
                                var slot = CardSlots[i];
                                var card = selectedCards[i];
                                slot.Set(card);
                                card.SetInteractable(true);
                            }
                        },
                        onClose: () =>
                        {
                            onClose = true;
                        });
                }

                yield return new WaitUntil(() => onConfirm && onClose);
                if (onConfirm) CloseUI();
                OnClickConfirmButtonCor = null;
            }
        }
    }
}