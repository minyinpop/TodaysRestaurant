using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.ANIMATION_SYSTEM;
using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.STATE_MACHINE;
using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.STATE_TYPE;
using BATTLE.CARD_SYSTEM.CARD.CARD_SKIN_SYSTEM;
using BATTLE.CARD_SYSTEM.CARD.CURSOR_EVENT_SYSTEM;
using BATTLE.CARD_SYSTEM.MANAGER;
using DG.Tweening;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD
{
    [RequireComponent(typeof(CursorEventSystem))]
    [RequireComponent(typeof(AnimationSystem))]
    [RequireComponent(typeof(CardSkinSystem))]
    internal abstract class BattleCardBase : MonoBehaviour, ICard
    {
        [field: Header("Component")]
        [field: SerializeField] private CursorEventSystem CursorEventSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private CardSkinSystem CardSkinSystem;

        private readonly StateMachine StateMachine = new();

        private void Awake()
        {
            CursorEventSystem.OnCursorEnter += OnCursorEnter;
            CursorEventSystem.OnCursorExit += OnCursorExit;
            CursorEventSystem.OnCursorClick += OnCursorClick;
        }

        private void SetParent(CardSlot cardSlot)
        {
            transform.SetParent(cardSlot.transform);
        }
        
        #region ICard
            public void OnSpawnInCardPool(CardSlot cardSlot)
            {
                SetParent(cardSlot);
                MoveToCardPoolSlotWhenSpawn();
                
                SetFrontImage();
                SetBackImage();
            }

            public void OnDrawCardAndShow(CardSlot cardSlot)
            {
                SetParent(cardSlot);
                MoveToShowPointWhenDraw();
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
            private Tween MoveToShowPointWhenDraw() => AnimationSystem.MoveToShowPointWhenDraw();
        #endregion
        
        #region Card Skin System
            private void SetFrontImage() => CardSkinSystem.SetFrontImage();
            private void SetBackImage() => CardSkinSystem.SetBackImage();
        #endregion
    }
}