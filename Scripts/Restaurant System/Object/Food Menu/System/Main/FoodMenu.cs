using System;
using System.Collections.Generic;
using Restaurant_System.System.Child.Food_Menu_System.System.Child;
using Restaurant_System.System.Child.Food_Menu_System.System.Child.Open_UI_System.Main;
using UnityEngine;

namespace Restaurant_System.System.Child.Food_Menu_System.System.Main
{
    internal sealed class FoodMenu : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private OpenPage OpenPage;
        [field: SerializeField] private ClosePage ClosePage;

        private readonly List<Action> ActiveActions = new();

        public event Action OnClickOpenUIConfirmButton;

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }

        public void Show()
        {
            ClosePage.Show(onComplete: OnUIShowComplete);
            return;

            void OnUIShowComplete()
            {
                ClosePage.OnClickOpenButton += OnOpenButtonClicked;
                ActiveActions.Add(() => ClosePage.OnClickOpenButton -= OnOpenButtonClicked);
            }

            void OnOpenButtonClicked()
            {
                ClosePage.Hide();
                OpenPage.Show();
                OpenPage.OnClickOpenUIConfirmButton += OnOpenUIConfirmButtonClicked;
                ActiveActions.Add(() => OpenPage.OnClickOpenUIConfirmButton -= OnOpenUIConfirmButtonClicked);
                return;

                void OnOpenUIConfirmButtonClicked()
                {
                    OpenPage.Hide(onComplete: () => OnClickOpenUIConfirmButton?.Invoke());
                }
            }
        }
    }
}