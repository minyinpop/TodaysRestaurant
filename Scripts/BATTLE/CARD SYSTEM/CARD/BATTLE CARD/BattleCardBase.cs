using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.ANIMATION_SYSTEM;
using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.INTERFACE;
using BATTLE.CARD_SYSTEM.CARD.CURSOR_EVENT_SYSTEM;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD
{
    [RequireComponent(typeof(CursorEventSystem))]
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class BattleCardBase : MonoBehaviour, IBattleCard
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

        #region IBattleCard Interface
            public void Pickup()
            {
                
            }
        #endregion
        
        #region Cursor Event System
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
        #endregion
        
        #region Animation System
            private void ScaleUpWhenCursorEnter() => AnimationSystem.ScaleUpWhenCursorEnter();
            private void ScaleDownWhenCursorExit() => AnimationSystem.ScaleDownWhenCursorExit();
            private void PopUpWhenCursorClick() => AnimationSystem.PopUpWhenCursorClick();
            private void PopDownWhenCursorClick() => AnimationSystem.PopDownWhenCursorClick();
        #endregion
    }
}