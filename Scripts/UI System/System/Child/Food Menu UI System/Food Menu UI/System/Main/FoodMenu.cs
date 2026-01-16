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
        [field: SerializeField] private OpenPage OpenPage;
        [field: SerializeField] private ClosePage ClosePage;

        private readonly Queue<Action> _activeActions = new();

        private void OnDisable()
        {
            while (_activeActions.Count > 0) _activeActions.Dequeue()?.Invoke();
        }

        public void Initialize(Action onClose)
        {
            ClosePage.Show(onComplete: OnUIShowComplete);
            return;

            void OnUIShowComplete()
            {
                ClosePage.OnClickOpenButton += OnOpenButtonClicked;
                _activeActions.Enqueue(() => ClosePage.OnClickOpenButton -= OnOpenButtonClicked);
            }

            void OnOpenButtonClicked()
            {
                ClosePage.Hide();
                OpenPage.Show();
                OpenPage.OnClickOpenUIConfirmButton += OnOpenUIConfirmButtonClicked;
                _activeActions.Enqueue(() => OpenPage.OnClickOpenUIConfirmButton -= OnOpenUIConfirmButtonClicked);
                return;

                void OnOpenUIConfirmButtonClicked()
                {
                    OpenPage.Hide(onClose);
                }
            }
        }
    }
}