using System;
using System.Collections.Generic;
using Audio_System.Data;
using Audio_System.Main;
using Common.Clickable_Bubble;
using Common.Interactable_Object;
using Common.Item.Data.Food;
using Common.Item.Data.Food.Custom_Food;
using Common.Value.Type;
using Input_System;
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
        [field: Header("系統狀態")]
        [field: SerializeField] private bool interactable;
                                public bool Interactable => interactable;
        
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

        private bool _initialized;

        private ClickableBubble _currentBubble;
        private CookGameSystem _currentCookGame;
        
        private IFood _currentCookItem;
        
        private readonly StateMachine _stateMachine = new();
        private IState _onEmptyState;
        private IState _onCookState;
        private IState _onGameTimeState;
        private IState _onCompleteState;
        private IState _onOvercookedState;

        private readonly Queue<Action> _cleanUpActions = new();

        private bool _isCookGameComplete;
        private bool _interactable = true;

        public static event Action OnGameTime;
        public static event Action OnCookComplete;
        public static event Action OnAddDish;

        public static event Action<CookType, Action<CustomFoodItem>, Action, Action> OpenCookSelectionUI;
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
            }
            
            _onEmptyState = InitializeEmptyState();
            _onCookState = InitializeCookState();
            _onGameTimeState = InitializeGameTimeState();
            _onCompleteState = InitializeCompleteState();
            _onOvercookedState = InitializeOvercookedState();
        }

        private void OnDisable()
        {
            while (_cleanUpActions.Count > 0)
            {
                _cleanUpActions.Dequeue()?.Invoke();
            }
        }

        public void StartSystem()
        {
            if (_initialized)
            {
                Debug.Log($"{name} 已經初始化過了。");
                return;
            }

            _initialized = true;
            _interactable = true;

            _stateMachine.InitializeState(_onEmptyState);
        }

        #region InteractableObject
            public void OnEnterDetect(PlayerObject playerObject)
            {
                _interactingPlayer = playerObject;
                _currentBubble?.SetInteractable(true);
            }

            public void OnExitDetect()
            {
                _interactingPlayer = null;
                _currentBubble?.SetInteractable(false);
                CloseCookSelectionUI?.Invoke();
            }

            public void Interact(PlayerObject playerObject)
            {
                _interactingPlayer = playerObject;
                _stateMachine.InteractState();
            }

            #endregion

        #region StateMachine
            #region OnEmpty
                private IState InitializeEmptyState()
                {
                    return new OnEmpty(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(emptyBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();

                            _currentBubble.OnClick += _stateMachine.InteractState;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= _stateMachine.InteractState);

                            _interactable = true;

                            _currentBubble.SetInteractable(_interactable && _interactingPlayer is not null);
                        },
                        onInteract: () =>
                        {
                            if (!_interactable)
                            {
                                return;
                            }

                            _interactable = false;

                            AudioSystem.Instance.InteractSFX.PlayOneShot(openSFXData);
                            
                            InputSystem.DisablePlayerWalk();
                            
                            OpenCookSelectionUI?.Invoke(cookwareType,
                                /* onConfirm */ cookItem =>
                                {
                                    _currentCookItem = cookItem;
                                    _stateMachine.ChangeState(_onCookState);
                                    
                                    InputSystem.EnablePlayerWalk();
                                },
                                /* onCancelStart: */ () =>
                                {
                                    AudioSystem.Instance.InteractSFX.PlayOneShot(closeSFXData);
                                },
                                /* onCancelEnd: */ () =>
                                {
                                    _interactable = true;
                                    
                                    InputSystem.EnablePlayerWalk();
                                });
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        });
                }
            #endregion
            
            #region OnCook
                private IState InitializeCookState()
                {
                    return new OnCook(
                        onEnter: () =>
                        {
                            if (cookingAMBData.Clip is null)
                            {
                                Debug.Log($"{name} 的 {nameof(CookwareSystem)} 沒有掛載烹飪中的環境音。");
                            }
                            else
                            {
                                audioSource.clip = cookingAMBData.Clip;
                                audioSource.loop = cookingAMBData.Loop;
                                audioSource.time = cookingAMBData.StartTime;
                                audioSource.Play();
                            }
                            
                            _currentBubble = Instantiate(cookBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();

                            // TODO 2025.12.03 從這裡繼續做
                            _currentBubble.StartCountDown(_currentCookItem.CookTime / 2,
                                onComplete: () =>
                                {
                                    _stateMachine.ChangeState(_isCookGameComplete
                                        ? _onCompleteState
                                        : _onGameTimeState);
                                });
                        },
                        onInteract: () =>
                        {
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        });
                }
            #endregion
            
            #region OnGameTime
                private IState InitializeGameTimeState()
                {
                    return new OnGameTime(
                        onEnter: () =>
                        {
                            OnGameTime?.Invoke();
                            
                            _currentBubble = Instantiate(gameTimeBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += _stateMachine.InteractState;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= _stateMachine.InteractState);

                            _interactable = true;
                            
                            _currentBubble.SetInteractable(_interactable && _interactingPlayer is not null);
                            _currentBubble.StartCountDown(
                                time: Mathf.Abs(gameTimeDuration),
                                onComplete: () => _stateMachine.ChangeState(_onOvercookedState));
                        },
                        onInteract: () =>
                        {
                            if (!_interactable)
                            {
                                return;
                            }
                            
                            _interactable = false;
                            
                            AudioSystem.Instance.InteractSFX.PlayOneShot(openSFXData);
                            
                            InputSystem.DisablePlayerWalk();
                            
                            _currentBubble.SetRaycastTarget(false);
                            
                            _currentCookGame = Instantiate(cookGamePrefab, gameParent).GetComponent<CookGameSystem>();
                            
                            _currentCookGame.OnComplete += OnCookGameComplete;
                            _cleanUpActions.Enqueue(() =>
                            {
                                if (_currentCookGame is not null)
                                {
                                    _currentCookGame.OnComplete -= OnCookGameComplete;
                                }
                            });
                            
                            _currentCookGame.OnCancel += OnCookGameCancel;
                            _cleanUpActions.Enqueue(() =>
                            {
                                if (_currentCookGame is not null)
                                {
                                    _currentCookGame.OnCancel -= OnCookGameCancel;
                                }
                            });
                            return;

                            void OnCookGameComplete()
                            {
                                _isCookGameComplete = true;
                                
                                AudioSystem.Instance.InteractSFX.PlayOneShot(closeSFXData);
                                
                                InputSystem.EnablePlayerWalk();
                                
                                _stateMachine.ChangeState(_onCookState);
                            }
                            
                            void OnCookGameCancel()
                            {
                                _interactable = true;
                                
                                AudioSystem.Instance.InteractSFX.PlayOneShot(closeSFXData);
                                
                                InputSystem.EnablePlayerWalk();
                                
                                _currentBubble.SetRaycastTarget(true);
                            }
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
                        });
                }
            #endregion
            
            #region OnComplete
                private IState InitializeCompleteState()
                {
                    return new OnComplete(
                        onEnter: () =>
                        {
                            OnCookComplete?.Invoke();
                            
                            _currentBubble = Instantiate(completeBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += _stateMachine.InteractState;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= _stateMachine.InteractState);

                            _interactable = true;
                            
                            _currentBubble.SetInteractable(_interactable && _interactingPlayer is not null);
                        },
                        onInteract: () =>
                        {
                            if (!_interactable)
                            {
                                return;
                            }
                            
                            if (OnAddDish is null)
                            {
                                throw new InvalidOperationException($"{nameof(OnAddDish)} 沒有其它 class 訂閱。");
                            }

                            if (_interactingPlayer is null)
                            {
                                throw new InvalidOperationException($"找不到 {nameof(_interactingPlayer)}。");
                            }
                            
                            _interactable = false;
                            
                            if (_interactingPlayer.TryAddItem(_currentCookItem))
                            {
                                OnAddDish.Invoke();
                                
                                AudioSystem.Instance.InteractSFX.PlayOneShot(closeSFXData);
                                
                                _stateMachine.ChangeState(_onEmptyState);
                            }
                            else
                            {
                                Debug.Log($"無法添加 {_currentCookItem.ItemName} 至玩家背包。");
                            }
                        },
                        onExit: Reset);
                }
            #endregion
            
            #region OnOvercooked
                private IState InitializeOvercookedState()
                {
                    return new OnOvercooked(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(overcookedBubblePrefab, bubbleParent).GetComponent<ClickableBubble>();
                            
                            _currentBubble.OnClick += _stateMachine.InteractState;
                            _cleanUpActions.Enqueue(() => _currentBubble.OnClick -= _stateMachine.InteractState);

                            _interactable = true;
                            
                            _currentBubble.SetInteractable(_interactable && _interactingPlayer is not null);
                        },
                        onInteract: () =>
                        {
                            if (!_interactable)
                            {
                                return;
                            }
                            
                            _interactable = false;

                            if (_interactingPlayer.TryAddItem(_currentCookItem))
                            {
                                AudioSystem.Instance.InteractSFX.PlayOneShot(closeSFXData);
                                
                                _stateMachine.ChangeState(_onEmptyState);
                            }
                            else
                            {
                                Debug.Log($"無法添加 {_currentCookItem.ItemName} 至玩家背包。");
                            }
                        },
                        onExit: Reset);
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