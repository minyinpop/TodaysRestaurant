using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.ANIMATION_SYSTEM;
using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.DATA;
using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.INTERFACE;
using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.STATE_MACHINE;
using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.STATE_TYPE;
using BATTLE.CARD_SYSTEM.CARD.CURSOR_EVENT_SYSTEM;
using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD
{
    [RequireComponent(typeof(CursorEventSystem))]
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class BattleCardBase : MonoBehaviour, IBattleCard
    {
        [field: Header("Battle Card SO")]
        [field: SerializeField] private BattleCardSO BattleCardSO;
        
        private CursorEventSystem CursorEventSystem;
        private AnimationSystem AnimationSystem;

        private readonly StateMachine StateMachine = new();

        private bool Interactable;
        private bool IsSelected;

        public event System.Action OnSelected;
        public event System.Action OnDeselected;

        private void Awake()
        {
            CursorEventSystem = GetComponent<CursorEventSystem>();
            AnimationSystem = GetComponent<AnimationSystem>();

            InCardPoolState();
        }

        private void Start()
        {
            CursorEventSystem.OnCursorEnter += OnCursorEnter;
            CursorEventSystem.OnCursorExit += OnCursorExit;
            CursorEventSystem.OnCursorClick += OnCursorClick;
        }
        
        private void SetInteractableToTrue() => Interactable = true;
        private void SetInteractableToFalse() => Interactable = false;

        #region IBattleCard Interface
            public void Pickup()
            {
                
            }
        #endregion
        
        #region State Machine
            private void ChangeState(IState newState) => StateMachine.ChangeState(this, newState);
            private void InCardPoolState() => ChangeState(new InCardPool());
        #endregion
        
        #region Cursor Event System
            private void OnCursorEnter()
            {
                if (!Interactable) return;
                ScaleUpWhenCursorEnter();
            }
            
            private void OnCursorExit()
            {
                if (!Interactable) return;
                ScaleDownWhenCursorExit();
            }

            private void OnCursorClick()
            {
                if (!Interactable) return;
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
            private Tween ScaleUpWhenCursorEnter() => AnimationSystem.ScaleUpWhenCursorEnter();
            private Tween ScaleDownWhenCursorExit() => AnimationSystem.ScaleDownWhenCursorExit();
            private Tween PopUpWhenCursorClick() => AnimationSystem.PopUpWhenCursorClick();
            private Tween PopDownWhenCursorClick() => AnimationSystem.PopDownWhenCursorClick();
            private Tween TurnToFront() => AnimationSystem.TurnToFront();
            private Tween TurnToBack() => AnimationSystem.TurnToBack();
        #endregion
    }
}