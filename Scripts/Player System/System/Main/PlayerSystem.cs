using System;
using System.Collections.Generic;
using Input_System.Main;
using Item;
using Player_System.System.Child;
using Player_System.System.Child.Detect_System.Main;
using Player_System.System.Child.Mouse_System.Main;
using Player_System.System.Main.State_Machine;
using Player_System.System.Main.State_Machine.State;
using Restaurant_System.Object.Cookware.System;
using Restaurant_System.Object.Creature.Customer.System.Main;
using Tool.Item_Giver;
using UI_System.System.Main;
using UnityEngine;

namespace Player_System.System.Main
{
    public sealed class PlayerSystem : MonoBehaviour
    {
        [field: Header("System Components")]
        [field: SerializeField] private MouseSystem mouseSystem;
        [field: SerializeField] private InventorySystem inventorySystem;
        
        [field: Header("Character Components")]
        [field: SerializeField] private MoveSystem moveSystem;
        [field: SerializeField] private AnimationSystem animationSystem;
        [field: SerializeField] private DetectSystem detectSystem;
        
        private readonly StateMachine _stateMachine = new();
        
        private readonly Queue<Action> _cleanUpActions = new();
        
        private void Start()
        {
            OnIdle();
        }

        private void OnEnable()
        {
            #region Input
                InputSystem.OnClickedLeftButton += ClickLeftButton;
                _cleanUpActions.Enqueue(() => InputSystem.OnClickedLeftButton -= ClickLeftButton);
                
                InputSystem.OnClickedRightButton += ClickRightButton;
                _cleanUpActions.Enqueue(() => InputSystem.OnClickedRightButton -= ClickRightButton);
                
                InputSystem.OnPerformedHotbar += PerformHotbar;
                _cleanUpActions.Enqueue(() => InputSystem.OnPerformedHotbar -= PerformHotbar);
            #endregion
            
            #region TryAddItem
                CookwareSystem.TryAddItem += TryAddItem;
                _cleanUpActions.Enqueue(() => CookwareSystem.TryAddItem -= TryAddItem);
                
                Customer.GivingServingNote += TryAddItem;
                _cleanUpActions.Enqueue(() => Customer.GivingServingNote -= TryAddItem);
            
                // Develop Only
                ItemGiver.OnClick += TryAddItem;
                _cleanUpActions.Enqueue(() => ItemGiver.OnClick -= TryAddItem);
                // ==================
            #endregion
        }

        private void OnDisable()
        {
            while (_cleanUpActions.Count > 0) _cleanUpActions.Dequeue()?.Invoke();
        }
        
        #region InputSystem
            private void ClickLeftButton()
            {
                mouseSystem.OnClickedLeftButton();
            }

            private void ClickRightButton()
            {
                UISystem.ClickRightButton();
            }
        #endregion
        
        #region InventorySystem
            private void PerformHotbar(int hotbarIndex)
            {
                inventorySystem.PerformHotbar(hotbarIndex);
            }

            private bool TryAddItem(ItemSO item)
            {
                var result = inventorySystem.TryAddItem(item);
                return result;
            }
        #endregion

        #region StateMachine
            #region OnIdle
                private void OnIdle()
                {
                    _stateMachine.ChangeState(new OnIdle(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        InputSystem.OnStartedPlayerWalk += OnWalk;
                        _cleanUpActions.Enqueue(() => InputSystem.OnStartedPlayerWalk -= OnWalk);
                        
                        animationSystem.Idle();
                    }
                    
                    void OnExit()
                    {
                        InputSystem.OnStartedPlayerWalk -= OnWalk;
                    }
                }
            #endregion

            #region OnWalk
                private void OnWalk()
                {
                    _stateMachine.ChangeState(new OnWalk(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        InputSystem.OnCancelPlayerWalk += OnIdle;
                        _cleanUpActions.Enqueue(() => InputSystem.OnCancelPlayerWalk -= OnIdle);

                        moveSystem.WalkLeft += animationSystem.TurnsLeft;
                        _cleanUpActions.Enqueue(() => moveSystem.WalkLeft -= animationSystem.TurnsLeft);
                        
                        moveSystem.WalkRight += animationSystem.TurnsRight;
                        _cleanUpActions.Enqueue(() => moveSystem.WalkRight -= animationSystem.TurnsRight);

                        moveSystem.StartWalk();
                        animationSystem.Walk();
                    }
                    
                    void OnExit()
                    {
                        InputSystem.OnCancelPlayerWalk -= OnIdle;
                        moveSystem.WalkLeft -= animationSystem.TurnsLeft;
                        moveSystem.WalkRight -= animationSystem.TurnsRight;
                        moveSystem.StopWalk();
                    }
                }
            #endregion
        #endregion
    }
}