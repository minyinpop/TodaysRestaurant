using System;
using System.Collections.Generic;
using Object.Bubble.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Object.Bubble.Object
{
    internal sealed class BasicBubble : MonoBehaviour, IBubble
    {
        [field: Header("Button")]
        [field: SerializeField] private Button Button;
        
        [field: Header("Color")]
        [field: SerializeField] private List<Image> Images;
        [field: SerializeField] private Color CanInteractColor;
        [field: SerializeField] private Color CannotInteractColor;
        
        private readonly List<Action> ActiveActions = new();
        
        public event Action OnClickBubble;

        private void OnEnable()
        {
            Button.OnClick += OnClick;
            ActiveActions.Add(() => Button.OnClick -= OnClick);
            return;

            void OnClick() { OnClickBubble?.Invoke(); }
        }
        
        private void OnDisable()
        {
            foreach (var action in ActiveActions) action();
            ActiveActions.Clear();
        }

        public void SetInteractable(bool interactable)
        {
            foreach (var image in Images) image.color = new Color(image.color.r, image.color.g, image.color.b, interactable ? CanInteractColor.a : CannotInteractColor.a);
            Button.SetInteractable(interactable);
        }

        public void StartCountDown(float time, Action onComplete)
        {
            //throw new NotImplementedException();
        }
    }
}