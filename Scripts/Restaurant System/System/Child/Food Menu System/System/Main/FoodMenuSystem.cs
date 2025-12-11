using System;
using System.Collections.Generic;
using Restaurant_System.System.Child.Food_Menu_System.System.Child;
using Restaurant_System.System.Child.Food_Menu_System.System.Child.Open_UI_System.Main;
using UnityEngine;

namespace Restaurant_System.System.Child.Food_Menu_System.System.Main
{
    internal sealed class FoodMenuSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private OpenUISystem OpenUISystem;
        [field: SerializeField] private CloseUISystem CloseUISystem;

        private readonly List<Action> ActiveActions = new();

        public event Action OnClickOpenUIConfirmButton;

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }

        public void Show()
        {
            CloseUISystem.Show(onComplete: OnUIShowComplete);
            return;

            void OnUIShowComplete()
            {
                CloseUISystem.OnClickOpenButton += OnOpenButtonClicked;
                ActiveActions.Add(() => CloseUISystem.OnClickOpenButton -= OnOpenButtonClicked);
            }

            void OnOpenButtonClicked()
            {
                CloseUISystem.Hide();
                OpenUISystem.Show();
                OpenUISystem.OnClickOpenUIConfirmButton += OnOpenUIConfirmButtonClicked;
                ActiveActions.Add(() => OpenUISystem.OnClickOpenUIConfirmButton -= OnOpenUIConfirmButtonClicked);
                return;

                void OnOpenUIConfirmButtonClicked()
                {
                    OpenUISystem.Hide(onComplete: () => OnClickOpenUIConfirmButton?.Invoke());
                }
            }
        }
    }
}