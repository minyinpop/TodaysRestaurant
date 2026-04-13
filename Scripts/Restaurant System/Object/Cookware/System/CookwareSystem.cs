using System;
using System.Collections.Generic;
using Audio_System.Data;
using Audio_System.Main;
using Common.Clickable_Bubble;
using Common.Interactable_Object;
using Common.Item.Data.Food;
using Common.Item.Data.Food.Custom_Food;
using Common.Value.Type;
using Player_System.Object;
using Restaurant_System.Object.Cookware.Object.Cook_Game.System.Main;
using Restaurant_System.Object.Cookware.System.State_Machine;
using Restaurant_System.Object.Cookware.System.State_Machine.State;
using UnityEngine;
using UnityEngine.Serialization;

namespace Restaurant_System.Object.Cookware.System
{
    internal sealed class CookwareSystem : MonoBehaviour, InteractableObject
    {
        [field: Header("廚具類型")]
        [field: SerializeField, FormerlySerializedAs("CookwareType")] private CookType cookwareType;
        
        [field: Header("互動氣泡")]
        [field: SerializeField, FormerlySerializedAs("EmptyBubblePrefab")] private GameObject emptyBubblePrefab;
        [field: SerializeField, FormerlySerializedAs("CookBubblePrefab")] private GameObject cookBubblePrefab;
        [field: SerializeField, FormerlySerializedAs("GameTimeBubblePrefab")] private GameObject gameTimeBubblePrefab;
        [field: SerializeField, FormerlySerializedAs("CompleteBubblePrefab")] private GameObject completeBubblePrefab;
        [field: SerializeField, FormerlySerializedAs("OvercookedBubblePrefab")] private GameObject overcookedBubblePrefab;
        [field: SerializeField, FormerlySerializedAs("BubbleParent")] private Transform bubbleParent;
        
        [field: Header("小遊戲")]
        [field: SerializeField, FormerlySerializedAs("GameTimeDuration")] private float gameTimeDuration;
        [field: SerializeField, FormerlySerializedAs("CookGamePrefab")] private GameObject cookGamePrefab;
        [field: SerializeField, FormerlySerializedAs("GameParent")] private Transform gameParent;
        
        [field: Header("聲音")]
        [field: SerializeField] private AudioSource audioSource;
        [field: SerializeField] private PlaySFXData openSFXData;
        [field: SerializeField] private PlaySFXData closeSFXData;
        [field: SerializeField] private PlayAMBData cookingAMBData;

        private ClickableBubble _currentBubble;
        private CookGameSystem _currentCookGame;
        
        private IFood _currentCookItem;
        
        private readonly StateMachine _stateMachine = new();

        private readonly Queue<Action> _cleanUpActions = new();

        private bool _isCookGameComplete;
        private bool _interactable;

        public static event Action OnGameTime;
        public static event Action OnCookComplete;
        public static event Action OnAddDish;

        public static event Action<CookType, Action<CustomFoodItem>, Action> OpenCookSelectionUI;
        public static event Action CloseCookSelectionUI;

        private PlayerObject _interactingPlayer;

        private void Awake()
        {
            if (emptyBubblePrefab is null)
            {
                Debug.Log($"{nameof(emptyBubblePrefab)} 沒有被掛載。");
                Destroy(gameObject);
                return;
            }
            
            if (emptyBubblePrefab is null)
            {
                Debug.Log($"{nameof(emptyBubblePrefab)} 沒有被掛載。");
                Destroy(gameObject);
                return;
            }
            
            if (cookBubblePrefab is null)
            {
                Debug.Log($"{nameof(cookBubblePrefab)} 沒有被掛載。");
                Destroy(gameObject);
                return;
            }
            
            if (gameTimeBubblePrefab is null)
            {
                Debug.Log($"{nameof(gameTimeBubblePrefab)} 沒有被掛載。");
                Destroy(gameObject);
                return;
            }
            
            if (completeBubblePrefab is null)
            {
                Debug.Log($"{nameof(completeBubblePrefab)} 沒有被掛載。");
                Destroy(gameObject);
                return;
            }
            
            if (overcookedBubblePrefab is null)
            {
                Debug.Log($"{nameof(overcookedBubblePrefab)} 沒有被掛載。");
                Destroy(gameObject);
                return;
            }
            
            if (cookGamePrefab is null)
            {
                Debug.Log($"{nameof(cookGamePrefab)} 沒有被掛載。");
                Destroy(gameObject);
                return;
            }
            
            if (gameParent is null)
            {
                Debug.Log($"{nameof(gameParent)} 沒有被掛載。");
                Destroy(gameObject);
                return;
            }
            
            if (audioSource is null)
            {
                Debug.Log($"{nameof(audioSource)} 沒有被掛載。");
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            OnEmptyState();
        }

        private void OnDisable()
        {
            while (_cleanUpActions.Count > 0) _cleanUpActions.Dequeue()?.Invoke();
        }

        #region InteractableObject
            public void OnEnterDetect(PlayerObject playerObject)
            {
                _interactingPlayer = playerObject;
                _currentBubble?.SetInteractable(true);
            }

            public void OnExitDetect(PlayerObject playerObject)
            {
                _interactingPlayer = null;
                _currentBubble?.SetInteractable(false);
                CloseCookSelectionUI?.Invoke();
            }

            public bool OnInteract(PlayerObject playerObject)
            {
                _interactingPlayer = playerObject;
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
                            _currentBubble = Instantiate(emptyBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += _stateMachine.InteractState;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= _stateMachine.InteractState);
                            
                            _currentBubble.SetInteractable(_interactable);
                        },
                        onInteract: () =>
                        {
                            AudioSystem.Instance.OtherSFX.PlayOneShot(openSFXData);
                            
                            OpenCookSelectionUI?.Invoke(cookwareType,
                                /* onConfirm */ cookItem =>
                                {
                                    _currentCookItem = cookItem;
                                    OnCookState();
                                },
                                /* onCancel: */ () =>
                                {
                                    AudioSystem.Instance.OtherSFX.PlayOneShot(closeSFXData);
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
                            _currentBubble = Instantiate(cookBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();

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
                            OnGameTime?.Invoke();
                            
                            _currentBubble = Instantiate(gameTimeBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += OnBubbleClicked;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= OnBubbleClicked);
                            
                            _currentBubble.SetInteractable(_interactable);
                            _currentBubble.StartCountDown(Mathf.Abs(gameTimeDuration),
                                onComplete: OnOvercookedState);
                            return;

                            void OnBubbleClicked()
                            {
                                _currentBubble.SetRaycastTarget(false);
                                
                                _currentCookGame = Instantiate(cookGamePrefab, gameParent).GetComponent<CookGameSystem>();
                                
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
                            OnCookComplete?.Invoke();
                            
                            _currentBubble = Instantiate(completeBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += OnBubbleClicked;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= OnBubbleClicked);
                            
                            _currentBubble.SetInteractable(_interactable);
                            return;

                            void OnBubbleClicked()
                            {
                                if (_interactingPlayer is null)
                                {
                                    Debug.Log($"{name} > {GetType().Name} > {nameof(OnCompleteState)} > {nameof(OnBubbleClicked)} > {nameof(_interactingPlayer)} is null.");
                                    Destroy(gameObject);
                                    return;
                                }
                                
                                if (_interactingPlayer.TryAddItem(_currentCookItem))
                                {
                                    OnAddDish?.Invoke();
                                    
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
                            _currentBubble = Instantiate(overcookedBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();
                            
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