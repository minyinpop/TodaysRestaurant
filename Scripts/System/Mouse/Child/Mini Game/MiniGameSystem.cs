using System.General;
using System.Mouse.Child.Mini_Game.Object;
using General.Object;
using UnityEngine;

namespace System.Mouse.Child.Mini_Game
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class MiniGameSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Utensils")]
        [field: SerializeField] private GameObject UtensilsParent;
        [field: SerializeField] private Utensils Utensils;
        
        [field: Header("Button")]
        [field: SerializeField] private Button ResetButton;
        [field: SerializeField] private Button CloseButton;

        private void OnEnable()
        {
            ResetButton.OnClick += OnResetButtonClicked;
            ResetButton.SetInteractable(true);
            CloseButton.OnClick += OnCloseButtonClicked;
            CloseButton.SetInteractable(true);
        }
        
        private void OnDisable()
        {
            ResetButton.SetInteractable(false);
            ResetButton.OnClick -= OnResetButtonClicked;
            CloseButton.SetInteractable(false);
            CloseButton.OnClick -= OnCloseButtonClicked;
        }

        private void OnResetButtonClicked()
        {
            Utensils.transform.position = UtensilsParent.transform.position;
            Utensils.Reset();
        }
        
        private void OnCloseButtonClicked()
        {
            Destroy(gameObject);
        }
    }
}