using System.Collections.Generic;
using System.Cook.Cookware.State_Machine;
using System.Cook.Cookware.State_Machine.State;
using Data.Item.Type.Dish;
using General.Object;
using UnityEngine;

namespace System.Cook.Cookware
{
    internal sealed class CookwareSystem : MonoBehaviour
    {
        [field: Header("Bubble")]
        [field: SerializeField] private GameObject EmptyBubblePrefab;
        [field: SerializeField] private Transform BubbleParent;

        private GameObject CurrentBubble;
        private Button CurrentBubble_Button;
        
        private readonly StateMachine StateMachine = new();

        private readonly List<Action> AllActiveActions = new();

        public static event Action<Action<DishSO>, Action> OnClickEmptyBubble;

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
                            CurrentBubble_Button = CurrentBubble.GetComponent<Button>();
                            CurrentBubble_Button.OnClick += OnBubbleClicked;
                            AllActiveActions.Add(() => CurrentBubble_Button.OnClick -= OnBubbleClicked);
                            CurrentBubble_Button.SetInteractable(true);
                            return;

                            void OnBubbleClicked()
                            {
                                OnClickEmptyBubble?.Invoke(
                                    /* onConfirm */ selectedDishData =>
                                    {
                                        Debug.Log(selectedDishData.name);
                                    },
                                    /* onCancel: */ () =>
                                    {
                                    });
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