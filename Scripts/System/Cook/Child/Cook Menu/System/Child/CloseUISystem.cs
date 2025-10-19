using System.Collections.Generic;
using General.Object;
using UnityEngine;

namespace System.Cook.Child.Cook_Menu.System.Child
{
    internal sealed class CloseUISystem : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject UI;
        
        [field: Header("Button")]
        [field: SerializeField] private Button OpenButton;
        
        public event Action OnClickOpenButton;

        private readonly List<Action> ActiveActions = new();

        private void OnEnable()
        {
            OpenButton.OnClick += OnOpenButtonClicked;
            OpenButton.SetInteractable(true);
            ActiveActions.Add(() =>
            {
                OpenButton.SetInteractable(false);
                OpenButton.OnClick -= OnOpenButtonClicked;
            });
        }
        
        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }

        private void OnOpenButtonClicked()
        {
            OnClickOpenButton?.Invoke();
        }
        
        public void Show()
        {
            UI.SetActive(true);
        }

        public void Hide()
        {
            UI.SetActive(false);
        }
    }
}