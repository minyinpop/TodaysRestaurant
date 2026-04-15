using System;
using System.Collections.Generic;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Camera_System;
using Common.Button;
using Common.Prograss_Bar;
using Restaurant_System.Object.Cookware.Object.Cook_Game.Object;
using Restaurant_System.Object.Cookware.Object.Cook_Game.System.Child;
using UnityEngine;

namespace Restaurant_System.Object.Cookware.Object.Cook_Game.System.Main
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class CookGameSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private DoAnimation DoAnimation;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoScale ZoomIn;
        [field: SerializeField] private DoScale ZoomOut;
        
        private readonly Queue<Action> _cleanUpActions = new();
        
        public event Action OnComplete;
        public event Action OnCancel;

        public static event Action OnGameStart;
        public static event Action OnGameFinish;

        private void Start()
        {
            CameraSystem.MoveTo(
                target: gameObject.transform,
                followOffset: new Vector3(0, 0, -4.5f),
                lookAtOffset: Vector3.zero);
            
            DoAnimation.DoScale_WorldSpace(gameObject.transform, ZoomIn,
                onComplete: () =>
                {
                    DetectSystem.UtensilsDetected += UtensilsDetected;
                    _cleanUpActions.Enqueue(() => DetectSystem.UtensilsDetected -= UtensilsDetected);
                    
                    Utensils.OnPick += OnUtensilsPicked;
                    _cleanUpActions.Enqueue(() =>
                    {
                        Utensils.Disable();
                        Utensils.OnPick -= OnUtensilsPicked;
                    });
                    
                    ProgressBar.OnMaxValue += OnProgressComplete;
                    _cleanUpActions.Enqueue(() => ProgressBar.OnMaxValue -= OnProgressComplete);
                    
                    ResetButton.OnClick += OnResetButtonClicked;
                    ResetButton.SetInteractable(true);
                    _cleanUpActions.Enqueue(() =>
                    {
                        ResetButton.OnClick -= OnResetButtonClicked;
                        ResetButton.SetInteractable(false);
                    });
                    
                    CloseButton.OnClick += OnCloseButtonClicked;
                    CloseButton.SetInteractable(true);
                    _cleanUpActions.Enqueue(() =>
                    {
                        CloseButton.OnClick -= OnCloseButtonClicked;
                        CloseButton.SetInteractable(false);
                    });
                });
        }

        private void OnEnable()
        {
            OnGameStart?.Invoke();
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
                OnGameFinish?.Invoke();
                
                ClearAllActions();
                DoAnimation.DoScale_WorldSpace(gameObject.transform, ZoomOut, () => OnComplete?.Invoke());
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
                DoAnimation.DoScale_WorldSpace(gameObject.transform, ZoomOut, () => OnCancel?.Invoke());
            }
        #endregion

        private void ClearAllActions()
        {
            while (_cleanUpActions.Count > 0) _cleanUpActions.Dequeue()?.Invoke();
            CameraSystem.MoveBack();
        }
    }
}