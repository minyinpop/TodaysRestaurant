using System.Collections.Generic;
using System.Economy.Child.Cookware.System.Child.Cook_Game.Object;
using System.Economy.Child.Cookware.System.Child.Cook_Game.System.Child;
using Common;
using Data.Animation.DOTween.Basic;
using Tool;
using UnityEngine;

namespace System.Economy.Child.Cookware.System.Child.Cook_Game.System.Main
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class CookGameSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale ZoomIn;
        [field: SerializeField] private DoScale ZoomOut;
        
        private readonly List<Action> AllActions = new();

        public event Action OnComplete;

        private void Start()
        {
            DoAnimation.DoScale_WorldSpace(gameObject.transform, ZoomIn,
                onComplete: () =>
                {
                    DetectSystem.UtensilsDetected += UtensilsDetected;
                    AllActions.Add(() => DetectSystem.UtensilsDetected -= UtensilsDetected);
                    
                    Utensils.OnPick += OnUtensilsPicked;
                    AllActions.Add(() =>
                    {
                        Utensils.Disable();
                        Utensils.OnPick -= OnUtensilsPicked;
                    });
                    
                    ProgressBar.OnMaxValue += OnProgressComplete;
                    AllActions.Add(() => ProgressBar.OnMaxValue -= OnProgressComplete);
                    
                    ResetButton.OnClick += OnResetButtonClicked;
                    ResetButton.SetInteractable(true);
                    AllActions.Add(() =>
                    {
                        ResetButton.OnClick -= OnResetButtonClicked;
                        ResetButton.SetInteractable(false);
                    });
                    
                    CloseButton.OnClick += OnCloseButtonClicked;
                    CloseButton.SetInteractable(true);
                    AllActions.Add(() =>
                    {
                        CloseButton.OnClick -= OnCloseButtonClicked;
                        CloseButton.SetInteractable(false);
                    });
                });
        }
        
        private void OnDisable()
        {
            ClearAllActions();
        }
        
        #region Detect Area
            [field: Header("Detect")]
            [field: SerializeField] private DetectSystem DetectSystem;

            private bool IsUtensilsInDetectArea;

            private void UtensilsDetected(bool isDetected)
            {
                IsUtensilsInDetectArea = isDetected;
            }
        #endregion
        
        #region Utensils
            [field: Header("Utensils")]
            [field: SerializeField] private Transform UtensilsParent;
            [field: SerializeField] private Utensils Utensils;

            private void OnUtensilsPicked(Vector2 desiredVel)
            {
                if (!IsUtensilsInDetectArea) return;
                var velocity = new Vector2(Mathf.Abs(desiredVel.x), Mathf.Abs(desiredVel.y));
                var value = velocity.magnitude;
                ProgressBar.Add(value);
            }
        #endregion
        
        #region Progress Bar
            [field: Header("Progress Bar")]
            [field: SerializeField] private ProgressBar ProgressBar;

            private void OnProgressComplete()
            {
                ClearAllActions();
                DoAnimation.DoScale_WorldSpace(gameObject.transform, ZoomOut,
                    onComplete: () =>
                    {
                        OnComplete?.Invoke();
                        Destroy(gameObject);
                    });
            }
        #endregion

        #region Button
            [field: Header("Button")]
            [field: SerializeField] private Button ResetButton;
            [field: SerializeField] private Button CloseButton;
            
            private void OnResetButtonClicked()
            {
                Utensils.transform.position = UtensilsParent.position;
                Utensils.Reset();
            }
            
            private void OnCloseButtonClicked()
            {
                ClearAllActions();
                DoAnimation.DoScale_WorldSpace(gameObject.transform, ZoomOut,
                    onComplete: () => Destroy(gameObject));
            }
        #endregion

        private void ClearAllActions()
        {
            if (AllActions.Count == 0) return;
            foreach (var action in AllActions) action();
            AllActions.Clear();
        }
    }
}