using System.Collections.Generic;
using System.Cook.Cook_Bubble;
using System.Cook.Cookware.State_Machine;
using System.Cook.Cookware.State_Machine.State;
using Data.General;
using UnityEngine;

namespace System.Cook.Cookware
{
    internal sealed class CookwareSystem : MonoBehaviour
    {
        [field: Header("Bubble")]
        [field: SerializeField] private GameObject EmptyBubblePrefab;
        [field: SerializeField] private GameObject CookBubblePrefab;
        [field: SerializeField] private GameObject GameTimeBubblePrefab;
        [field: SerializeField] private GameObject OvercookedBubblePrefab;
        [field: SerializeField] private Transform BubbleParent;
        
        [field: Header("")]
        [field: SerializeField] private float GameTimeDuration;

        private GameObject CurrentBubble;
        private CookBubble CurrentBubble_CookBubble;
        
        private readonly StateMachine StateMachine = new();

        private readonly List<Action> AllActiveActions = new();

        private CookDish CookDish;

        public static event Action<Action<CookDish>, Action> OnClickEmptyBubble;

        private void Start()
        {
            OnEmptyState();
        }

        private void OnDisable()
        {
            foreach (var action in AllActiveActions) action?.Invoke();
            AllActiveActions.Clear();
        }

        #region StateMachine
            #region OnEmpty
                private void OnEmptyState()
                {
                    StateMachine.ChangeState(new OnEmpty(
                        onEnter: () =>
                        {
                            CurrentBubble = Instantiate(EmptyBubblePrefab, BubbleParent);
                            CurrentBubble_CookBubble = CurrentBubble.GetComponent<CookBubble>();
                            CurrentBubble_CookBubble.OnClick += OnBubbleClicked;
                            AllActiveActions.Add(() => CurrentBubble_CookBubble.OnClick -= OnBubbleClicked);
                            CurrentBubble_CookBubble.SetInteractable(true);
                            return;

                            void OnBubbleClicked()
                            {
                                OnClickEmptyBubble?.Invoke(
                                    /* onConfirm */ cookDish =>
                                    {
                                        CookDish = cookDish;
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
                            CurrentBubble_CookBubble = null;
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
                            CurrentBubble_CookBubble = CurrentBubble.GetComponent<CookBubble>();
                            CookDish.GetValues(out _, out var cookTime, out _);
                            CurrentBubble_CookBubble.CoutDown(cookTime / 2,
                                onComplete: OnGameTimeState);
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble);
                            CurrentBubble = null;
                            CurrentBubble_CookBubble = null;
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
                            CurrentBubble_CookBubble = CurrentBubble.GetComponent<CookBubble>();
                            CurrentBubble_CookBubble.OnClick += OnBubbleClicked;
                            AllActiveActions.Add(() => CurrentBubble_CookBubble.OnClick -= OnBubbleClicked);
                            CurrentBubble_CookBubble.SetInteractable(true);
                            CurrentBubble_CookBubble.CoutDown(Mathf.Abs(GameTimeDuration),
                                onComplete: OnOvercookedState);
                            return;

                            void OnBubbleClicked()
                            {
                            }
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble);
                            CurrentBubble = null;
                            CurrentBubble_CookBubble = null;
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
                            CurrentBubble_CookBubble = CurrentBubble.GetComponent<CookBubble>();
                            CurrentBubble_CookBubble.OnClick += OnEmptyState;
                            AllActiveActions.Add(() => CurrentBubble_CookBubble.OnClick -= OnEmptyState);
                            CurrentBubble_CookBubble.SetInteractable(true);
                        },
                        onExit: () =>
                        {
                            Destroy(CurrentBubble);
                            CurrentBubble = null;
                            CurrentBubble_CookBubble = null;
                        }));
                }
            #endregion
        #endregion
    }
}