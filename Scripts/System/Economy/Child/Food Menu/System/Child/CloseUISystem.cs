using System.Collections.Generic;
using System.General;
using Data.Animation.DOTween.Basic;
using UnityEngine;

namespace System.Economy.Child.Food_Menu.System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class CloseUISystem : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject UI;
        [field: SerializeField] private CanvasGroup UI_CanvasGroup;
        
        [field: Header("Button")]
        [field: SerializeField] private Button OpenButton;
        
        [field: Header("Animation")]
        [field: SerializeField] private DoAnimation DoAnimation;
        [field: SerializeField] private DoFade_CanvasGroup ShowSettings;
        
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
        
        public void Show(Action onComplete)
        {
            UI.SetActive(true);
            DoAnimation.DoFade_CanvasGroup(UI_CanvasGroup, ShowSettings,
                onComplete: () => onComplete?.Invoke());
        }

        public void Hide()
        {
            UI.SetActive(false);
        }
    }
}