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
using Tool.Item_Giver;
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
        
        private readonly StateMachine StateMachine = new();
        
        private readonly Queue<Action> ActiveActions = new();
        
        public static event Action RightButtonClicked;
        
        private void Start()
        {
            OnIdle();
        }

        private void OnEnable()
        {
            InputSystem.OnClickedLeftButton += ClickLeftButton;
            ActiveActions.Enqueue(() => InputSystem.OnClickedLeftButton -= ClickLeftButton);
            
            InputSystem.OnClickedRightButton += ClickRightButton;
            ActiveActions.Enqueue(() => InputSystem.OnClickedRightButton -= ClickRightButton);
            
            #region InventorySystem
                InputSystem.OnPerformedHotbar += PerformHotbar;
                ActiveActions.Enqueue(() => InputSystem.OnPerformedHotbar -= PerformHotbar);
                
                CookwareSystem.OnClickCompleteBubble += TryAddItem;
                ActiveActions.Enqueue(() => CookwareSystem.OnClickCompleteBubble -= TryAddItem);
                
                // Develop Only
                ItemGiver.OnClick += TryAddItem;
                ActiveActions.Enqueue(() => ItemGiver.OnClick -= TryAddItem);
                // ==================
            #endregion
        }

        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
        }
        
        #region InputSystem
            private void ClickLeftButton()
            {
                mouseSystem.OnClickedLeftButton();
            }

            private void ClickRightButton()
            {
                RightButtonClicked?.Invoke();
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
                    StateMachine.ChangeState(new OnIdle(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        InputSystem.OnStartedPlayerWalk += OnWalk;
                        ActiveActions.Enqueue(() => InputSystem.OnStartedPlayerWalk -= OnWalk);
                        
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
                    StateMachine.ChangeState(new OnWalk(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        InputSystem.OnCancelPlayerWalk += OnIdle;
                        ActiveActions.Enqueue(() => InputSystem.OnCancelPlayerWalk -= OnIdle);

                        moveSystem.WalkLeft += animationSystem.TurnsLeft;
                        ActiveActions.Enqueue(() => moveSystem.WalkLeft -= animationSystem.TurnsLeft);
                        
                        moveSystem.WalkRight += animationSystem.TurnsRight;
                        ActiveActions.Enqueue(() => moveSystem.WalkRight -= animationSystem.TurnsRight);

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