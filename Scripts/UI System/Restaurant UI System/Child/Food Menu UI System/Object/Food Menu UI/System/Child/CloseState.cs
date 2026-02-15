using System;
using Common.Button;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child
{
    internal sealed class CloseState : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject ui;
        [field: SerializeField] private Button openButton;
        
        public event Action OnConfirm;

        private void Awake()
        {
            openButton.OnClick += OnConfirm;
        }

        private void Start()
        {
            ui.SetActive(true);
        }

        private void OnEnable()
        {
            openButton.SetInteractable(true);
        }

        private void OnDisable()
        {
            openButton.SetInteractable(false);
        }

        private void OnDestroy()
        {
            openButton.OnClick -= OnConfirm;
        }

        public void Hide()
        {
            ui.SetActive(false);
        }
    }
}