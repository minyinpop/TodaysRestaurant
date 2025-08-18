using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.ANIMATION_SYSTEM;
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
        [field: Header("Component")]
        [field: SerializeField] private CursorEventSystem CursorEventSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;

        private StateMachine StateMachine = new();

        private void Awake()
        {
            CursorEventSystem.OnCursorEnter += OnCursorEnter;
            CursorEventSystem.OnCursorExit += OnCursorExit;
            CursorEventSystem.OnCursorClick += OnCursorClick;
        }
        
        #region IBattleCard
            public void OnSpawnInCardPool()
            {
                MoveToCardPoolSlotWhenSpawn();
            }
        #endregion
        
        #region State Machine
            private void ChangeState(IState newState) => StateMachine.ChangeState(this, newState);
            private void InCardPoolState() => ChangeState(new InCardPool());
        #endregion
        
        #region Cursor Event System
            private void OnCursorEnter()
            {
            }
                
            private void OnCursorExit()
            {
            }
                
            private void OnCursorClick()
            {
            }
        #endregion
        
        #region Animation System
            private Tween MoveToCardPoolSlotWhenSpawn() => AnimationSystem.MoveToCardPoolSlotWhenSpawn();
        #endregion
    }
}