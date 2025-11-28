using System.Collections.Generic;
using System.Economy.Child.Cookware.State_Machine;
using System.Economy.Child.Cookware.State_Machine.State;
using System.Economy.Child.Cookware.System.Child.Cook_Bubble.Main;
using System.Economy.Child.Cookware.System.Child.Cook_Game.System.Main;
using Data.General.Enum;
using Data.Item.Base;
using Data.Item.Type.Custom;
using Interface;
using UnityEngine;

namespace System.Economy.Child.Cookware.System.Main
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

        private Bubble CurrentBubble;
        private ITem CurrentCookItem;
        
        private readonly StateMachine StateMachine = new();

        private readonly Queue<Action> ActiveActions = new();

        private bool IsCookGameComplete;
        private bool Interactable;

        public static event Action<CookType, Action<CustomItem>, Action> OnClickEmptyBubble;
        public static event Func<ITem, bool> OnClickCompleteBubble;
        
        // TODO 5 審專用
        public static event Action<string, Action> ChangeScene;
        public static event Action<string, int> StartScenario;

        private void Start()
        {
            OnEmptyState();
        }

        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
        }

        #region InteractableObject
            public void OnEnterDetect()
            {
                CurrentBubble?.SetInteractable(true);
            }
            
            public void OnExitDetect()
            {
                CurrentBubble?.SetInteractable(false);
            }
        #endregion

        #region StateMachine
            #region OnEmpty
                private void OnEmptyState()
                {
                    StateMachine.ChangeState(new OnEmpty(
                        onEnter: () =>
                        {
                            CurrentBubble = Instantiate(EmptyBubblePrefab, BubbleParent).GetComponent<Bubble>();
                            CurrentBubble.OnClick += OnBubbleClicked;
                            ActiveActions.Enqueue(() => CurrentBubble.OnClick -= OnBubbleClicked);
                            CurrentBubble.SetInteractable(Interactable);
                            return;

                            void OnBubbleClicked()
                            {
                                OnClickEmptyBubble?.Invoke(CookwareType,
                                    /* onConfirm */ cookItem =>
                                    {
                                        CurrentCookItem = cookItem;
                                        OnCookState();
                                    },
                                    /* onCancel: */ () =>
                                    {
                                    });
                            }
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble.gameObject);
                            CurrentBubble = null;
                        }));
                }
            #endregion
            
            #region OnCook
                private void OnCookState()
                {
                    StateMachine.ChangeState(new OnCook(
                        onEnter: () =>
                        {
                            CurrentBubble = Instantiate(CookBubblePrefab, BubbleParent).GetComponent<Bubble>();
                            CurrentCookItem.GetCookTime(out var cookTime);
                            CurrentBubble.CountDown(cookTime / 2,
                                onComplete: () =>
                                {
                                    if (IsCookGameComplete) OnCompleteState();
                                    else OnGameTimeState();
                                });
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble.gameObject);
                            CurrentBubble = null;
                        }));
                }
            #endregion
            
            #region OnGameTime
                private void OnGameTimeState()
                {
                    StateMachine.ChangeState(new OnGameTime(
                        onEnter: () =>
                        {
                            CurrentBubble = Instantiate(GameTimeBubblePrefab, BubbleParent).GetComponent<Bubble>();
                            CurrentBubble.OnClick += OnBubbleClicked;
                            ActiveActions.Enqueue(() => CurrentBubble.OnClick -= OnBubbleClicked);
                            CurrentBubble.SetInteractable(Interactable);
                            CurrentBubble.CountDown(Mathf.Abs(GameTimeDuration),
                                onComplete: OnOvercookedState);
                            return;

                            void OnBubbleClicked()
                            {
                                var cookGame = Instantiate(CookGamePrefab, GameParent);
                                cookGame.GetComponent<CookGameSystem>().OnComplete += OnCookGameComplete;
                                ActiveActions.Enqueue(() => { if (cookGame != null) cookGame.GetComponent<CookGameSystem>().OnComplete -= OnCookGameComplete; });
                                return;

                                void OnCookGameComplete()
                                {
                                    IsCookGameComplete = true;
                                    OnCookState();
                                }
                            }
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble.gameObject);
                            CurrentBubble = null;
                        }));
                }
            #endregion
            
            #region OnComplete
                private void OnCompleteState()
                {
                    StateMachine.ChangeState(new OnComplete(
                        onEnter: () =>
                        {
                            CurrentBubble = Instantiate(CompleteBubblePrefab, BubbleParent).GetComponent<Bubble>();
                            CurrentBubble.OnClick += OnBubbleClicked;
                            ActiveActions.Enqueue(() => CurrentBubble.OnClick -= OnBubbleClicked);
                            CurrentBubble.SetInteractable(Interactable);
                            return;

                            void OnBubbleClicked()
                            {
                                var isAddItemToPlayerInventoryComplete = OnClickCompleteBubble?.Invoke(CurrentCookItem);
                                switch (isAddItemToPlayerInventoryComplete)
                                {
                                    case null:
                                    {
                                        throw new Exception();
                                    }
                                    case true:
                                    {
                                        OnEmptyState();
                                        
                                        // TODO 5 審專用
                                        ChangeScene?.Invoke("Dialogue ( Dev )",
                                            () =>
                                            {
                                                // onComplete
                                                StartScenario?.Invoke("Abnormal", 0);
                                            });
                                        break;
                                    }
                                    default:
                                    {
                                        CurrentCookItem.GetItemName(out var itemName);
                                        Debug.Log($"無法添加 {itemName} 至玩家背包。");
                                        break;
                                    }
                                }
                            }
                        },
                        onExit: () =>
                        {
                            CurrentCookItem = null;
                            IsCookGameComplete = false;
                            
                            Destroy(CurrentBubble.gameObject);
                            CurrentBubble = null;
                        }));
                }
            #endregion
            
            #region OnOvercooked
                private void OnOvercookedState()
                {
                    StateMachine.ChangeState(new OnOvercooked(
                        onEnter: () =>
                        {
                            CurrentBubble = Instantiate(OvercookedBubblePrefab, BubbleParent).GetComponent<Bubble>();
                            CurrentBubble.OnClick += OnEmptyState;
                            ActiveActions.Enqueue(() => CurrentBubble.OnClick -= OnEmptyState);
                            CurrentBubble.SetInteractable(Interactable);
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble.gameObject);
                            CurrentBubble = null;
                        }));
                }
            #endregion
        #endregion
    }
}