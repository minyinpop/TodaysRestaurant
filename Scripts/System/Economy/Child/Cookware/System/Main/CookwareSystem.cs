using System.Collections.Generic;
using System.Economy.Child.Cookware.State_Machine;
using System.Economy.Child.Cookware.State_Machine.State;
using System.Economy.Child.Cookware.System.Child.Cook_Bubble.Main;
using System.Economy.Child.Cookware.System.Child.Cook_Game.System.Main;
using Data.General;
using Data.General.Enum;
using UnityEngine;

namespace System.Economy.Child.Cookware.System.Main
{
    internal sealed class CookwareSystem : MonoBehaviour
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
        
        private GameObject CurrentBubble;
        private Bubble CurrentBubble_Bubble;
        
        [field: Header("Game")]
        [field: SerializeField] private float GameTimeDuration;
        [field: SerializeField] private GameObject CookGamePrefab;
        [field: SerializeField] private Transform GameParent;

        private readonly StateMachine StateMachine = new();

        private readonly List<Action> AllActions = new();

        private CookFood CookFood;

        private bool IsCookGameComplete;
        private bool Interactable;

        public static event Action<CookType, Action<CookFood>, Action> OnClickEmptyBubble;

        private void Start()
        {
            OnEmptyState();
        }

        private void OnDisable()
        {
            foreach (var action in AllActions) action?.Invoke();
            AllActions.Clear();
        }

        public void SetInteractable(bool interactable)
        {
            Interactable = interactable;
            CurrentBubble_Bubble.SetInteractable(Interactable);
        }

        #region StateMachine
            #region OnEmpty
                private void OnEmptyState()
                {
                    StateMachine.ChangeState(new OnEmpty(
                        onEnter: () =>
                        {
                            CurrentBubble = Instantiate(EmptyBubblePrefab, BubbleParent);
                            CurrentBubble_Bubble = CurrentBubble.GetComponent<Bubble>();
                            CurrentBubble_Bubble.OnClick += OnBubbleClicked;
                            AllActions.Add(() => CurrentBubble_Bubble.OnClick -= OnBubbleClicked);
                            CurrentBubble_Bubble.SetInteractable(Interactable);
                            return;

                            void OnBubbleClicked()
                            {
                                OnClickEmptyBubble?.Invoke(CookwareType,
                                    /* onConfirm */ cookDish =>
                                    {
                                        CookFood = cookDish;
                                        OnCookState();
                                    },
                                    /* onCancel: */ () =>
                                    {
                                    });
                            }
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble);
                            CurrentBubble = null;
                            CurrentBubble_Bubble = null;
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
                            CurrentBubble_Bubble = CurrentBubble.GetComponent<Bubble>();
                            CookFood.GetValues(out _, out var cookTime, out _);
                            CurrentBubble_Bubble.CoutDown(cookTime / 2,
                                onComplete: () =>
                                {
                                    if (IsCookGameComplete) OnCompleteState();
                                    else OnGameTimeState();
                                });
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble);
                            CurrentBubble = null;
                            CurrentBubble_Bubble = null;
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
                            CurrentBubble_Bubble = CurrentBubble.GetComponent<Bubble>();
                            CurrentBubble_Bubble.OnClick += OnBubbleClicked;
                            AllActions.Add(() => CurrentBubble_Bubble.OnClick -= OnBubbleClicked);
                            CurrentBubble_Bubble.SetInteractable(Interactable);
                            CurrentBubble_Bubble.CoutDown(Mathf.Abs(GameTimeDuration),
                                onComplete: OnOvercookedState);
                            return;

                            void OnBubbleClicked()
                            {
                                var cookGame = Instantiate(CookGamePrefab, GameParent);
                                cookGame.GetComponent<CookGameSystem>().OnComplete += OnCookGameComplete;
                                AllActions.Add(() =>
                                {
                                    if (cookGame != null)
                                        cookGame.GetComponent<CookGameSystem>().OnComplete -= OnCookGameComplete;
                                });
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
                            Destroy(CurrentBubble);
                            CurrentBubble = null;
                            CurrentBubble_Bubble = null;
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
                            CurrentBubble_Bubble = CurrentBubble.GetComponent<Bubble>();
                            CurrentBubble_Bubble.OnClick += OnBubbleClicked;
                            AllActions.Add(() => CurrentBubble_Bubble.OnClick -= OnBubbleClicked);
                            CurrentBubble_Bubble.SetInteractable(Interactable);
                            return;

                            void OnBubbleClicked()
                            {
                                IsCookGameComplete = false;
                                OnEmptyState();
                            }
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble);
                            CurrentBubble = null;
                            CurrentBubble_Bubble = null;
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
                            CurrentBubble_Bubble = CurrentBubble.GetComponent<Bubble>();
                            CurrentBubble_Bubble.OnClick += OnEmptyState;
                            AllActions.Add(() => CurrentBubble_Bubble.OnClick -= OnEmptyState);
                            CurrentBubble_Bubble.SetInteractable(Interactable);
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble);
                            CurrentBubble = null;
                            CurrentBubble_Bubble = null;
                        }));
                }
            #endregion
        #endregion
    }
}