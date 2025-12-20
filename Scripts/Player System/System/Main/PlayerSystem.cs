using System;
using System.Collections.Generic;
using Input_System.Main;
using Item;
using Player_System.System.Child;
using Player_System.System.Child.Detect_System.Main;
using Player_System.System.Child.Inventory.Main;
using Player_System.System.Main.State_Machine;
using Player_System.System.Main.State_Machine.State;
using Restaurant_System.Object.Cookware.System;
using Tool.Item_Giver;
using UnityEngine;

namespace Player_System.System.Main
{
    public sealed class PlayerSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private DetectSystem DetectSystem;
        [field: SerializeField] private InventorySystem InventorySystem;

        private readonly StateMachine StateMachine = new();
        
        private readonly Queue<Action> ActiveActions = new();

        private void Start()
        {
            OnIdle();
        }

        private void OnEnable()
        {
            #region InventorySystem
                InputSystem.OnPerformedHotbar += OnPerformedHotbar;
                ActiveActions.Enqueue(() => InputSystem.OnPerformedHotbar -= OnPerformedHotbar);
                
                InputSystem.OnClickedLeftButton += OnClickedLeftButton;
                ActiveActions.Enqueue(() => InputSystem.OnClickedLeftButton -= OnClickedLeftButton);
                
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

        private void OnPerformedHotbar(int hotbarIndex)
        {
            InventorySystem.OnPerformedHotbar(hotbarIndex);
        }

        private void OnClickedLeftButton()
        {
            InventorySystem.OnClickedLeftButton();
        }

        private bool TryAddItem(ItemSO item)
        {
            var result = InventorySystem.TryAddItem(item);
            return result;
        }

        #region StateMachine
            private void OnIdle()
            {
                StateMachine.ChangeState(new OnIdle(OnEnter, OnExit));
                return;

                void OnEnter()
                {
                    InputSystem.OnStartedPlayerWalk += OnWalk;
                    ActiveActions.Enqueue(() => InputSystem.OnStartedPlayerWalk -= OnWalk);
                    
                    AnimationSystem.Idle();
                }
                
                void OnExit()
                {
                    InputSystem.OnStartedPlayerWalk -= OnWalk;
                }
            }

            private void OnWalk()
            {
                StateMachine.ChangeState(new OnWalk(OnEnter, OnExit));
                return;

                void OnEnter()
                {
                    InputSystem.OnCancelPlayerWalk += OnIdle;
                    ActiveActions.Enqueue(() => InputSystem.OnCancelPlayerWalk -= OnIdle);

                    MoveSystem.WalkLeft += AnimationSystem.TurnsLeft;
                    ActiveActions.Enqueue(() => MoveSystem.WalkLeft -= AnimationSystem.TurnsLeft);
                    
                    MoveSystem.WalkRight += AnimationSystem.TurnsRight;
                    ActiveActions.Enqueue(() => MoveSystem.WalkRight -= AnimationSystem.TurnsRight);

                    MoveSystem.StartWalk();
                    AnimationSystem.Walk();
                }
                
                void OnExit()
                {
                    InputSystem.OnCancelPlayerWalk -= OnIdle;
                    MoveSystem.WalkLeft -= AnimationSystem.TurnsLeft;
                    MoveSystem.WalkRight -= AnimationSystem.TurnsRight;
                    MoveSystem.StopWalk();
                }
            }
        #endregion
    }
}