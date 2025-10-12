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

        private void OnEnable()
        {
            OpenButton.OnClick += OnOpenButtonClicked;
        }
        
        private void OnDisable()
        {
            OpenButton.OnClick -= OnOpenButtonClicked;
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