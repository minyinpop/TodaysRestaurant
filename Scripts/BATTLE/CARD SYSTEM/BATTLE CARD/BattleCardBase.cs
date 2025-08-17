using BATTLE.CARD_SYSTEM.BATTLE_CARD.ANIMATION_SYSTEM;
using BATTLE.CARD_SYSTEM.CURSOR_EVENT_SYSTEM;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.BATTLE_CARD
{
    [RequireComponent(typeof(CursorEventSystem))]
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class BattleCardBase : MonoBehaviour
    {
        private CursorEventSystem CursorEventSystem;
        private AnimationSystem AnimationSystem;

        private bool IsSelected;

        public event System.Action OnSelected;
        public event System.Action OnDeselected;

        private void Awake()
        {
            CursorEventSystem = GetComponent<CursorEventSystem>();
            AnimationSystem = GetComponent<AnimationSystem>();
        }

        private void Start()
        {
            CursorEventSystem.OnCursorEnter += OnCursorEnter;
            CursorEventSystem.OnCursorExit += OnCursorExit;
            CursorEventSystem.OnCursorClick += OnCursorClick;
        }
        
        private void OnCursorEnter()
        {
            ScaleUpWhenCursorEnter();
        }
        
        private void OnCursorExit()
        {
            ScaleDownWhenCursorExit();
        }

        private void OnCursorClick()
        {
            IsSelected = !IsSelected;
            
            if (IsSelected)
            {
                OnSelected?.Invoke();
                PopUpWhenCursorClick();
            }
            else
            {
                OnDeselected?.Invoke();
                PopDownWhenCursorClick();
            }
        }
        
        #region Animation System
            private void ScaleUpWhenCursorEnter() => AnimationSystem.ScaleUpWhenCursorEnter();
            private void ScaleDownWhenCursorExit() => AnimationSystem.ScaleDownWhenCursorExit();
            private void PopUpWhenCursorClick() => AnimationSystem.PopUpWhenCursorClick();
            private void PopDownWhenCursorClick() => AnimationSystem.PopDownWhenCursorClick();
        #endregion
    }
}