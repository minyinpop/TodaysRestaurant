using System;
using System.Collections;
using System.Collections.Generic;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Button;
using Common.Player.Child.Player_Team;
using Common.Value;
using DG.Tweening;
using Explore_System.System.Child.Battle_System.Object.Card_Slot;
using Explore_System.System.Child.Battle_System.Object.Card;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
using UI_System.Message_UI_System.Main;
using UnityEngine;
using UnityEngine.Serialization;

namespace Explore_System.System.Child.Battle_System.System.Child.Selected_Card_System.System
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class SelectedCardSystem : MonoBehaviour
    {
        [field: Header("動畫系統")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("介面動畫設定")]
        [field: SerializeField] private CanvasGroup UICanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup fadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup fadeOutSettings;
        
        [field: Header("卡片格子")]
        [field: SerializeField, FormerlySerializedAs("SpawnParent")] private Transform spawnParent;
        [field: SerializeField, FormerlySerializedAs("SlotPrefab")] private GameObject slotPrefab;
        
        [field: Header("卡片順序預製件")]
        [field: SerializeField, FormerlySerializedAs("CardOrderPrefabs")] private List<GameObject> cardOrderPrefabs;
        
        [field: Header("按鈕")]
        [field: SerializeField, FormerlySerializedAs("ConfirmButton")] private Button confirmButton;
        
        [field: Header("玩家團隊資料")]
        [field: SerializeField] private PlayerTeamSO playerTeamSO;

        private readonly List<CardSlot> _cardSlots = new();
        private readonly List<Card> _selectedCards = new();

        public event Action<Card> ReturnCardToHand;
        
        public event Action<Card> OnHoverCardEvent;
        public event Action<Card> OnHoverExitEvent;
        
        public static event Action<List<Card>, Action> BeforeCloseUI;
        public event Action AfterCloseUI;

        public static event Action OnOpen;
        public static event Action OnConfirm;

        private IEnumerator _onClickConfirmButtonCor;

        private void Awake()
        {
            confirmButton.OnClick += OnConfirmButtonClicked;
        }

        private void Start()
        {
            for (var i = 0; i < playerTeamSO.CharacterNumber; i++)
            {
                var slot = Instantiate(slotPrefab, spawnParent);
                var slotScript = slot.GetComponent<CardSlot>();
                _cardSlots.Add(slotScript);
            }
        }
        
        private void OnDisable()
        {
            if (_onClickConfirmButtonCor is not null)
            {
                StopCoroutine(_onClickConfirmButtonCor);
                _onClickConfirmButtonCor = null;
            }
        }

        private void OnDestroy()
        {
            confirmButton.OnClick -= OnConfirmButtonClicked;
        }

        public void OpenUI(Action onUIOpen = null, Action onUIClose = null)
        {
            #region 狀態廣播
                OnOpen?.Invoke();
            #endregion
            
            UICanvasGroup.gameObject.SetActive(true);
            AfterCloseUI = onUIClose;
            
            animation.DoFade_CanvasGroup(
                    canvasGroup: UICanvasGroup,
                    settings: fadeInSettings,
                    onComplete: () =>
                    {
                        confirmButton.SetInteractable(true);
                        onUIOpen?.Invoke();
                    });
        }

        private void CloseUI()
        {
            confirmButton.SetInteractable(false);
            
            foreach (var card in _selectedCards)
            {
                card.Interactable = false;
            }
            
            BeforeCloseUI?.Invoke(_selectedCards, () =>
            {
                animation.DoFade_CanvasGroup(
                    canvasGroup: UICanvasGroup,
                    settings: fadeOutSettings,
                    onComplete: () =>
                    {
                        AfterCloseUI?.Invoke();
                        _selectedCards.Clear();
                        UICanvasGroup.gameObject.SetActive(false);

                    });
            });
        }

        public bool TryAdd(Card card)
        {
            if (card is not BattleCard battleCard)
            {
                throw new ArgumentException($"{nameof(card)} 不是 {nameof(BattleCard)}，無法添加到 {nameof(SelectedCardSystem)}。");
            }

            for (var i = 0; i < _cardSlots.Count; i++)
            {
                var slot = _cardSlots[i];
                
                if (!slot.IsEmpty())
                {
                    continue;
                }
                
                slot.Set(battleCard);
                battleCard.SetCardOrder(cardOrderPrefabs[i]);
                battleCard.Move(slot.transform, new DoAnchorPos(Vector2.zero, .5f, true, Ease.OutExpo));
                
                battleCard.OnHover += OnHoverCard;
                battleCard.OnHoverExit += OnHoverExit;
                battleCard.OnClick += OnClickCard;
                return true;
            }

            return false;
        }
        
        private void OnHoverCard(Card card)
        {
            if (OnHoverCardEvent is null)
            {
                Debug.Log($"{OnHoverCardEvent} 沒有其它 class 訂閱。");
                return;
            }

            OnHoverCardEvent.Invoke(card);
        }

        private void OnHoverExit(Card card)
        {
            if (OnHoverExitEvent is null)
            {
                Debug.Log($"{OnHoverExitEvent} 沒有其它 class 訂閱。");
                return;
            }

            OnHoverExitEvent.Invoke(card);
        }

        private void OnClickCard(Card card)
        {
            if (ReturnCardToHand is null)
            {
                Debug.Log($"{nameof(ReturnCardToHand)} 沒有被其它 class 訂閱。");
                return;
            }
            
            if (card is BattleCard battleCard)
            {
                ReturnCardToHand.Invoke(battleCard);
                
                battleCard.OnHover -= OnHoverCard;
                battleCard.OnHoverExit -= OnHoverExit;
                battleCard.OnClick -= OnClickCard;
                
                battleCard.RemoveCardOrder();
                
                for (var i = 0; i < _cardSlots.Count; i++)
                {
                    var slot = _cardSlots[i];
                    if (!slot.Compare(battleCard)) continue;
                    _cardSlots.Remove(slot);
                    Destroy(slot.gameObject);
                    break;
                }
                        
                var newSlot = Instantiate(slotPrefab, spawnParent);
                var newSlotScript = newSlot.GetComponent<CardSlot>();
                
                _cardSlots.Add(newSlotScript);
                
                for (var i = 0; i < _cardSlots.Count; i++)
                {
                    var slot = _cardSlots[i];
                    
                    if (slot.IsEmpty())
                    {
                        continue;
                    }
                    
                    slot.Get(out var thisCard);

                    if (thisCard is not BattleCard thisBattleCard)
                    {
                        throw new InvalidOperationException($"{card.name} 不是 {nameof(BattleCard)}，所以無法添加回 {nameof(HandCardSystem)}。");
                    }

                    thisBattleCard.SetCardOrder(cardOrderPrefabs[i]);
                    slot.Set(thisCard);
                }
            }
            else
            {
                throw new InvalidOperationException($"{card.name} 是未在 {nameof(HandCardSystem)} 裡面登記的卡片類型，請聯絡團隊添加。");
            }
            
        }
        
        private void OnConfirmButtonClicked()
        {
            _onClickConfirmButtonCor = OnConfirmButtonClickCoroutine();
            StartCoroutine(_onClickConfirmButtonCor);
            return;

            IEnumerator OnConfirmButtonClickCoroutine()
            {
                #region 狀態廣播
                    OnConfirm?.Invoke();
                #endregion
                
                var onConfirm = false;
                var selectedCards = new List<Card>();
                foreach (var slot in _cardSlots)
                {
                    if (slot.IsEmpty()) continue;
                    slot.Get(out var card);
                    selectedCards.Add(card);
                }

                if (selectedCards.Count == _cardSlots.Count)
                {
                    foreach (var card in selectedCards)
                        _selectedCards.Add(card);
                    CloseUI();
                    _onClickConfirmButtonCor = null;
                    yield break;
                }

                if (selectedCards.Count == 0)
                {
                    confirmButton.SetInteractable(false);
                    MessageUISystem.ShowTipUI(
                        content: new PopUpUIContent(
                            message:"請選擇至少一張卡牌",
                            confirmButtonTitle: "確定",
                            cancelButtonTitle: string.Empty,
                            closeButtonTitle: string.Empty),
                        onConfirm: () =>
                        {
                            confirmButton.SetInteractable(true);
                        });
                }
                else
                {
                    foreach (var card in selectedCards)
                        card.Interactable = false;
                    
                    MessageUISystem.ShowSwitchUI(
                        content: new PopUpUIContent(
                            message: "還可以選擇卡片\n確定要直接開始戰鬥嗎？",
                            confirmButtonTitle: "確定",
                            cancelButtonTitle: "返回",
                            closeButtonTitle: string.Empty),
                        onConfirm: () =>
                        {
                            foreach (var card in selectedCards)
                                _selectedCards.Add(card);
                            onConfirm = true;
                        },
                        onCancel: () =>
                        {
                            for (var i = 0; i < selectedCards.Count; i++)
                            {
                                var slot = _cardSlots[i];
                                var card = selectedCards[i];
                                slot.Set(card);
                                card.Interactable = true;
                            }
                        });
                }

                yield return new WaitUntil(() => onConfirm);
                if (onConfirm) CloseUI();
                _onClickConfirmButtonCor = null;
            }
        }
    }
}