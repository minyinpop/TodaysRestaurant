using System.Collections.Generic;
using System.Cook.Child.Cook_Menu.System.Child;
using System.Cook.Child.Cook_Menu.System.Child.Open_UI_System.Main;
using UnityEngine;

namespace System.Cook.Child.Cook_Menu.System.Main
{
    internal sealed class CookMenuSystem : MonoBehaviour
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
            CloseUISystem.Show();
            CloseUISystem.OnClickOpenButton += OnOpenButtonClicked;
            ActiveActions.Add(() => CloseUISystem.OnClickOpenButton -= OnOpenButtonClicked);
            return;
            
            void OnOpenButtonClicked()
            {
                CloseUISystem.Hide();
                OpenUISystem.Show();
                OpenUISystem.OnClickOpenUIConfirmButton += OnClickOpenUIConfirmButton;
                ActiveActions.Add(() => OpenUISystem.OnClickOpenUIConfirmButton -= OnClickOpenUIConfirmButton);
            }
        }
    }
}