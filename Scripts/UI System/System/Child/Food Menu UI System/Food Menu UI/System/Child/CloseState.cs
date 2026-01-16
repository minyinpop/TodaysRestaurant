using System;
using System.Collections.Generic;
using Common.Object;
using UnityEngine;

namespace UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Child
{
    internal sealed class CloseState : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject UI;
        
        [field: Header("Button")]
        [field: SerializeField] private Button OpenButton;
        
        public event Action OnClickOpenButton;

        private readonly List<Action> ActiveActions = new();

        private void OnEnable()
        {
            OpenButton.onClick += OnOpenButtonClicked;
            OpenButton.SetInteractable(true);
            ActiveActions.Add(() =>
            {
                OpenButton.SetInteractable(false);
                OpenButton.onClick -= OnOpenButtonClicked;
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

        public void Hide()
        {
            UI.SetActive(false);
        }
    }
}