using System.Collections.Generic;
using System.Economy.Child.Cookware.State_Machine;
using System.Economy.Child.Cookware.State_Machine.State;
using System.Economy.Child.Cookware.System.Child.Cook_Game.System.Main;
using Data.General.Enum;
using Data.Item.Data.Custom;
using Data.Item.Interface;
using Interface;
using Object.Clickable_Bubble.Interface;
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

        private GameObject CurrentBubble;
        private IClickableBubble CurrentBubbleScript;
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
                CurrentBubbleScript?.SetInteractable(true);
            }
            
            public void OnExitDetect()
            {
                CurrentBubbleScript?.SetInteractable(false);
            }
        #endregion

        #region StateMachine
            #region OnEmpty
                private void OnEmptyState()
                {
                    StateMachine.ChangeState(new OnEmpty(
                        onEnter: () =>
                        {
                            CurrentBubble = Instantiate(EmptyBubblePrefab, BubbleParent);
                            CurrentBubbleScript = CurrentBubble.GetComponent<IClickableBubble>();
                            
                            CurrentBubbleScript.OnClickBubble += OnBubbleClicked;
                            ActiveActions.Enqueue(() => CurrentBubbleScript.OnClickBubble -= OnBubbleClicked);
                            
                            CurrentBubbleScript.SetInteractable(Interactable);
                            return;

                            void OnBubbleClicked()
                            {Debug.Log(name+" is being clicked.");
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
                            CurrentBubble = Instantiate(CookBubblePrefab, BubbleParent);
                            CurrentBubbleScript = CurrentBubble.GetComponent<IClickableBubble>();
                            
                            CurrentCookItem.GetCookTime(out var cookTime); // TODO 2025.12.03 從這裡繼續做
                            CurrentBubbleScript.StartCountDown(cookTime / 2,
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
                            CurrentBubble = Instantiate(GameTimeBubblePrefab, BubbleParent);
                            CurrentBubbleScript = CurrentBubble.GetComponent<IClickableBubble>();
                            
                            CurrentBubbleScript.OnClickBubble += OnBubbleClicked;
                            ActiveActions.Enqueue(() => CurrentBubbleScript.OnClickBubble -= OnBubbleClicked);
                            
                            CurrentBubbleScript.SetInteractable(Interactable);
                            CurrentBubbleScript.StartCountDown(Mathf.Abs(GameTimeDuration),
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
                            CurrentBubble = Instantiate(CompleteBubblePrefab, BubbleParent);
                            CurrentBubbleScript = CurrentBubble.GetComponent<IClickableBubble>();
                            
                            CurrentBubbleScript.OnClickBubble += OnBubbleClicked;
                            ActiveActions.Enqueue(() => CurrentBubbleScript.OnClickBubble -= OnBubbleClicked);
                            
                            CurrentBubbleScript.SetInteractable(Interactable);
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
                            CurrentBubble = Instantiate(OvercookedBubblePrefab, BubbleParent);
                            CurrentBubbleScript = CurrentBubble.GetComponent<IClickableBubble>();
                            
                            CurrentBubbleScript.OnClickBubble += OnEmptyState;
                            ActiveActions.Enqueue(() => CurrentBubbleScript.OnClickBubble -= OnEmptyState);
                            CurrentBubbleScript.SetInteractable(Interactable);
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