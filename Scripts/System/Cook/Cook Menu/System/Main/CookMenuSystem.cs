using System.Collections.Generic;
using System.Cook.Cook_Menu.System.Child;
using System.Cook.Cook_Menu.System.Child.Open_UI_System.Main;
using UnityEngine;

namespace System.Cook.Cook_Menu.System.Main
{
    internal sealed class CookMenuSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private OpenUISystem OpenUISystem;
        [field: SerializeField] private CloseUISystem CloseUISystem;

        private readonly List<Action> ActiveActions = new();
        
        private void OnEnable()
        {
            CloseUISystem.OnClickOpenButton += OnOpenButtonClicked;
            ActiveActions.Add(() => CloseUISystem.OnClickOpenButton -= OnOpenButtonClicked);
        }
        
        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }

        private void OnOpenButtonClicked()
        {
            CloseUISystem.Close();
            OpenUISystem.Open();
        }
    }
}