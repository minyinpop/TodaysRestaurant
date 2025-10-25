using System.Collections.Generic;
using System.Economy.Child.Cookware.System.Child.Cook_Bubble.Main;
using UnityEngine;
using UnityEngine.UI;

namespace System.Economy.Child.Cookware.System.Child.Cook_Bubble.Child
{
    internal sealed class OvercookBubble : Bubble
    {
        [field: Header("Button")]
        [field: SerializeField] private Button Button;
        
        [field: Header("Color")]
        [field: SerializeField] private List<Image> Images;
        [field: SerializeField] private Color CanInteractColor;
        [field: SerializeField] private Color CannotInteractColor;
        
        private readonly List<Action> ActiveActions = new();
        
        private void OnEnable()
        {
            Button.OnClick += OnClicked;
            ActiveActions.Add(() => Button.OnClick -= OnClicked);
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }
        
        public override void SetInteractable(bool interactable)
        {
            foreach (var image in Images) image.color = new Color(image.color.r, image.color.g, image.color.b, interactable ? CanInteractColor.a : CannotInteractColor.a);
            Button.SetInteractable(interactable);
        }
    }
}