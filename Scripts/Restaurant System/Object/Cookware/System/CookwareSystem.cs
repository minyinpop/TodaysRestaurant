using System;
using System.Collections.Generic;
using Common.Clickable_Bubble;
using Common.Interactable_Object;
using Common.Item.Data.Food;
using Common.Item.Data.Food.Custom_Food;
using Common.Value.Type;
using Player_System.System;
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

        private ClickableBubble _currentBubble;
        private CookGameSystem _currentCookGame;
        
        private IFood _currentCookItem;
        
        private readonly StateMachine _stateMachine = new();

        private readonly Queue<Action> _cleanUpActions = new();

        private bool _isCookGameComplete;
        private bool _interactable;

        public static event Action<CookType, Action<CustomFoodItem>, Action> OpenCookSelectionUI;
        public static event Action CloseCookSelectionUI;

        private PlayerSystem _interactingPlayer;

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
                _currentBubble?.SetInteractable(true);
            }

            public void OnExitDetect()
            {
                _currentBubble?.SetInteractable(false);
                CloseCookSelectionUI?.Invoke();
            }

            public bool OnInteract(PlayerSystem playerSystem)
            {
                _interactingPlayer = playerSystem;
                _stateMachine.InteractState();
                return false;
            }
        #endregion

        #region StateMachine
            #region OnEmpty
                private void OnEmptyState()
                {
                    _stateMachine.ChangeState(new OnEmpty(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(EmptyBubblePrefab, BubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += _stateMachine.InteractState;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= _stateMachine.InteractState);
                            
                            _currentBubble.SetInteractable(_interactable);
                        },
                        onInteract: () =>
                        {
                            OpenCookSelectionUI?.Invoke(CookwareType,
                                /* onConfirm */ cookItem =>
                                {
                                    _currentCookItem = cookItem;
                                    OnCookState();
                                },
                                /* onCancel: */ () =>
                                {
                                });
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
                            _currentBubble = Instantiate(CookBubblePrefab, BubbleParent).GetComponent<ClickableBubble>();

                            // TODO 2025.12.03 從這裡繼續做
                            _currentBubble.StartCountDown(_currentCookItem.CookTime / 2,
                                onComplete: () =>
                                {
                                    if (_isCookGameComplete) OnCompleteState();
                                    else OnGameTimeState();
                                });
                        },
                        onInteract: () =>
                        {
                            // TODO
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
                            _currentBubble = Instantiate(GameTimeBubblePrefab, BubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += OnBubbleClicked;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= OnBubbleClicked);
                            
                            _currentBubble.SetInteractable(_interactable);
                            _currentBubble.StartCountDown(Mathf.Abs(GameTimeDuration),
                                onComplete: OnOvercookedState);
                            return;

                            void OnBubbleClicked()
                            {
                                _currentBubble.SetRaycastTarget(false);
                                
                                _currentCookGame = Instantiate(CookGamePrefab, GameParent).GetComponent<CookGameSystem>();
                                
                                _currentCookGame.OnComplete += OnCookGameComplete;
                                _cleanUpActions.Enqueue(() =>
                                {
                                    if (_currentCookGame is not null)
                                        _currentCookGame.OnComplete -= OnCookGameComplete;
                                });
                                
                                _currentCookGame.OnCancel += OnCookGameCancel;
                                _cleanUpActions.Enqueue(() =>
                                {
                                    if (_currentCookGame is not null)
                                        _currentCookGame.OnCancel -= OnCookGameCancel;
                                });
                                return;

                                void OnCookGameComplete()
                                {
                                    _isCookGameComplete = true;
                                    OnCookState();
                                }
                                
                                void OnCookGameCancel()
                                {
                                    _currentBubble.SetRaycastTarget(true);
                                }
                            }
                        },
                        onInteract: () =>
                        {
                            // TODO
                        },
                        onExit: () =>
                        {
                            if (_currentCookGame is not null)
                            {
                                Destroy(_currentCookGame.gameObject);
                                _currentCookGame = null;
                            }
                            
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
                            _currentBubble = Instantiate(CompleteBubblePrefab, BubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += OnBubbleClicked;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= OnBubbleClicked);
                            
                            _currentBubble.SetInteractable(_interactable);
                            return;

                            void OnBubbleClicked()
                            {
                                if (_interactingPlayer.TryAddItem(_currentCookItem))
                                {
                                    OnEmptyState();
                                }
                                else
                                {
                                    Debug.Log($"無法添加 {_currentCookItem.ItemName} 至玩家背包。");
                                }
                            }
                        },
                        onInteract: () =>
                        {
                            // TODO
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
                            _currentBubble = Instantiate(OvercookedBubblePrefab, BubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += OnClick;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= OnClick);
                            _currentBubble.SetInteractable(_interactable);
                            return;

                            void OnClick()
                            {
                                if (_interactingPlayer.TryAddItem(_currentCookItem))
                                {
                                    OnEmptyState();
                                }
                                else
                                {
                                    Debug.Log($"無法添加 {_currentCookItem.ItemName} 至玩家背包。");
                                }
                            }
                        },
                        onInteract: () =>
                        {
                            // TODO
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