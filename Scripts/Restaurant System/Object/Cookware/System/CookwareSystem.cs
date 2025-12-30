using System;
using System.Collections.Generic;
using Common;
using Common.Object;
using Common.Value.Type;
using Item;
using Item.Custom;
using Restaurant_System.Object.Cookware.Object.Cook_Game.System.Main;
using Restaurant_System.Object.Cookware.System.State_Machine;
using Restaurant_System.Object.Cookware.System.State_Machine.State;
using UnityEngine;

namespace Restaurant_System.Object.Cookware.System
{
    internal sealed class CookwareSystem : MonoBehaviour, InteractableObject
    {
        [field: Header("Type")]
        [field: SerializeField] private CookType CookwareType;
        
        [field: Header("Bubble")]
        [field: SerializeField] private GameObject EmptyBubblePrefab;
        [field: SerializeField] private GameObject CookBubblePrefab;
        [field: SerializeField] private GameObject GameTimeBubblePrefab;
        [field: SerializeField] private GameObject CompleteBubblePrefab;
        [field: SerializeField] private GameObject OvercookedBubblePrefab;
        [field: SerializeField] private Transform BubbleParent;
        
        [field: Header("Game")]
        [field: SerializeField] private float GameTimeDuration;
        [field: SerializeField] private GameObject CookGamePrefab;
        [field: SerializeField] private Transform GameParent;

        private GameObject _currentBubble;
        private ClickableBubble _currentBubbleScript;
        private ItemSO _currentCookItem;
        
        private readonly StateMachine _stateMachine = new();

        private readonly Queue<Action> _cleanUpActions = new();

        private bool _isCookGameComplete;
        private bool _interactable;

        public static event Action<CookType, Action<CustomItem>, Action> OnClickEmptyBubble;
        public static event Func<ItemSO, bool> TryAddItem;

        private void Start()
        {
            OnEmptyState();
        }

        private void OnDisable()
        {
            while (_cleanUpActions.Count > 0) _cleanUpActions.Dequeue()?.Invoke();
        }

        #region InteractableObject
            public void OnEnterDetect()
            {
                _currentBubbleScript?.SetInteractable(true);
            }
            
            public void OnExitDetect()
            {
                _currentBubbleScript?.SetInteractable(false);
            }
        #endregion

        #region StateMachine
            #region OnEmpty
                private void OnEmptyState()
                {
                    _stateMachine.ChangeState(new OnEmpty(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(EmptyBubblePrefab, BubbleParent);
                            _currentBubbleScript = _currentBubble.GetComponent<ClickableBubble>();
                            
                            _currentBubbleScript.OnClick += OnBubbleClicked;
                            _cleanUpActions.Enqueue(() => _currentBubbleScript.OnClick -= OnBubbleClicked);
                            
                            _currentBubbleScript.SetInteractable(_interactable);
                            return;

                            void OnBubbleClicked()
                            {
                                OnClickEmptyBubble?.Invoke(CookwareType,
                                    /* onConfirm */ cookItem =>
                                    {
                                        _currentCookItem = cookItem;
                                        OnCookState();
                                    },
                                    /* onCancel: */ () =>
                                    {
                                    });
                            }
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                }
            #endregion
            
            #region OnCook
                private void OnCookState()
                {
                    _stateMachine.ChangeState(new OnCook(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(CookBubblePrefab, BubbleParent);
                            _currentBubbleScript = _currentBubble.GetComponent<ClickableBubble>();
                            
                            _currentCookItem.GetCookTime(out var cookTime); // TODO 2025.12.03 從這裡繼續做
                            _currentBubbleScript.StartCountDown(cookTime / 2,
                                onComplete: () =>
                                {
                                    if (_isCookGameComplete) OnCompleteState();
                                    else OnGameTimeState();
                                });
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                }
            #endregion
            
            #region OnGameTime
                private void OnGameTimeState()
                {
                    _stateMachine.ChangeState(new OnGameTime(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(GameTimeBubblePrefab, BubbleParent);
                            _currentBubbleScript = _currentBubble.GetComponent<ClickableBubble>();
                            
                            _currentBubbleScript.OnClick += OnBubbleClicked;
                            _cleanUpActions.Enqueue(() => _currentBubbleScript.OnClick -= OnBubbleClicked);
                            
                            _currentBubbleScript.SetInteractable(_interactable);
                            _currentBubbleScript.StartCountDown(Mathf.Abs(GameTimeDuration),
                                onComplete: OnOvercookedState);
                            return;

                            void OnBubbleClicked()
                            {
                                var cookGame = Instantiate(CookGamePrefab, GameParent);
                                cookGame.GetComponent<CookGameSystem>().OnComplete += OnCookGameComplete;
                                _cleanUpActions.Enqueue(() => { if (cookGame != null) cookGame.GetComponent<CookGameSystem>().OnComplete -= OnCookGameComplete; });
                                return;

                                void OnCookGameComplete()
                                {
                                    _isCookGameComplete = true;
                                    OnCookState();
                                }
                            }
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                }
            #endregion
            
            #region OnComplete
                private void OnCompleteState()
                {
                    _stateMachine.ChangeState(new OnComplete(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(CompleteBubblePrefab, BubbleParent);
                            _currentBubbleScript = _currentBubble.GetComponent<ClickableBubble>();
                            
                            _currentBubbleScript.OnClick += OnBubbleClicked;
                            _cleanUpActions.Enqueue(() => _currentBubbleScript.OnClick -= OnBubbleClicked);
                            
                            _currentBubbleScript.SetInteractable(_interactable);
                            return;

                            void OnBubbleClicked()
                            {
                                var isAddItemToPlayerInventoryComplete = TryAddItem?.Invoke(_currentCookItem) ?? false;
                                if (isAddItemToPlayerInventoryComplete)
                                {
                                    OnEmptyState();
                                }
                                else
                                {
                                    Debug.Log($"無法添加 {_currentCookItem.ItemName} 至玩家背包。");
                                }
                            }
                        },
                        onExit: Reset));
                }
            #endregion
            
            #region OnOvercooked
                private void OnOvercookedState()
                {
                    _stateMachine.ChangeState(new OnOvercooked(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(OvercookedBubblePrefab, BubbleParent);
                            _currentBubbleScript = _currentBubble.GetComponent<ClickableBubble>();
                            
                            _currentBubbleScript.OnClick += OnClick;
                            _cleanUpActions.Enqueue(() => _currentBubbleScript.OnClick -= OnClick);
                            _currentBubbleScript.SetInteractable(_interactable);
                            return;

                            void OnClick()
                            {
                                Debug.Log(_currentCookItem);
                                _currentCookItem.GetOvercookedItem(out var overcookedItem);
                                Debug.Log(overcookedItem);
                                var result = TryAddItem?.Invoke(overcookedItem) ?? false;
                                
                                if (result)
                                {
                                    OnEmptyState();
                                }
                                else
                                {
                                    // TODO 煮過頭的料理無法添加進玩家的背包
                                }
                            }
                        },
                        onExit: Reset));
                }
            #endregion
        #endregion
        
        #region Unitily
            private void Reset()
            {
                _currentCookItem = null;
                _isCookGameComplete = false;
                
                Destroy(_currentBubble.gameObject);
                _currentBubble = null;
            }
        #endregion
    }
}