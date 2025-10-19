using System.Collections.Generic;
using General.Object;
using UnityEngine;

namespace System.Cook.Cook_Menu.System.Child
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
            ActiveActions.Add(() => OpenButton.OnClick -= OnOpenButtonClicked);
            OpenButton.SetInteractable(true);
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
        
        public void Open()
        {
            UI.SetActive(true);
        }

        public void Close()
        {
            UI.SetActive(false);
        }
    }
}