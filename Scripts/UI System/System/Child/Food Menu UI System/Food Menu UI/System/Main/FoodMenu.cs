using System;
using System.Collections.Generic;
using UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Child;
using UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Child.Open_Page.Main;
using UnityEngine;

namespace UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Main
{
    internal sealed class FoodMenu : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private OpenState openState;
        [field: SerializeField] private CloseState closeState;

        private readonly Queue<Action> _activeActions = new();

        private void OnDisable()
        {
            while (_activeActions.Count > 0) _activeActions.Dequeue()?.Invoke();
        }

        public void Initialize(Action onClose)
        {
            closeState.OnClickOpenButton += OnOpenButtonClicked;
            _activeActions.Enqueue(() => closeState.OnClickOpenButton -= OnOpenButtonClicked);
            return;

            void OnOpenButtonClicked()
            {
                closeState.Hide();
                openState.Show();
                openState.OnClickOpenUIConfirmButton += OnOpenUIConfirmButtonClicked;
                _activeActions.Enqueue(() => openState.OnClickOpenUIConfirmButton -= OnOpenUIConfirmButtonClicked);
                return;

                void OnOpenUIConfirmButtonClicked()
                {
                    openState.Hide(onClose);
                }
            }
        }
    }
}