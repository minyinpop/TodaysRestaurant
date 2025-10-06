using System.Collections;
using System.Collections.Generic;
using System.Cook.Cookware.State_Machine;
using System.Cook.Cookware.State_Machine.State;
using Data.General;
using General.Object;
using UnityEngine;

namespace System.Cook.Cookware
{
    internal sealed class CookwareSystem : MonoBehaviour
    {
        [field: Header("Bubble")]
        [field: SerializeField] private GameObject EmptyBubblePrefab;
        [field: SerializeField] private GameObject CookBubblePrefab;
        [field: SerializeField] private GameObject GameTimeBubblePrefab;
        [field: SerializeField] private Transform BubbleParent;

        private GameObject CurrentBubble;
        private Button CurrentBubble_Button;
        
        private readonly StateMachine StateMachine = new();

        private readonly List<Action> AllActiveActions = new();

        private CookDish CookDish;

        private IEnumerator CountDownCor;

        public static event Action<Action<CookDish>, Action> OnClickEmptyBubble;

        private void Start()
        {
            OnEmptyState();
        }

        private void OnDisable()
        {
            if (CountDownCor is not null)
            {
                StopCoroutine(CountDownCor);
                CountDownCor = null;
            }
            
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
                            CurrentBubble_Button = CurrentBubble.GetComponent<Button>();
                            CurrentBubble_Button.OnClick += OnBubbleClicked;
                            AllActiveActions.Add(() => CurrentBubble_Button.OnClick -= OnBubbleClicked);
                            CurrentBubble_Button.SetInteractable(true);
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
                            CookDish.GetValues(out var cookDish, out var cookTime, out var price);
                            Debug.Log(cookDish);
                            Debug.Log(cookTime);
                            Debug.Log(price);
                            
                            Destroy(CurrentBubble);
                            CurrentBubble = null;
                            CurrentBubble_Button = null;
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
                            CurrentBubble_Button = CurrentBubble.GetComponent<Button>();
                            return;

                            IEnumerator CountDownCoroutine()
                            {
                                yield return new WaitForSeconds(1);
                            }
                        },
                        onExit: () =>
                        {
                        }));
                }
            #endregion
        #endregion
    }
}