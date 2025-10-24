using System.Collections.Generic;
using System.Cook.Child.Cook_Menu.System.Main;
using System.Cook.Child.Customer.System;
using System.Cook.Child.Queue.System;
using System.Cook.Child.Seating.System;
using System.Cook.Main.State_Machine;
using System.Cook.Main.State_Machine.State;
using UnityEngine;

namespace System.Cook.Main
{
    internal sealed class CookSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private CookMenuSystem CookMenuSystem;
        [field: SerializeField] private CustomerSystem CustomerSystem;
        [field: SerializeField] private QueueSystem QueueSystem;
        [field: SerializeField] private SeatingSystem SeatingSystem;
        
        private readonly List<Action> ActiveActions = new();
        
        private void Start()
        {
            OnCookStart();
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
        }

        #region State Machine
            private readonly StateMachine StateMachine = new();
            private void OnCookStart()
            {
                StateMachine.ChangeState(new OnCookStart(OnEnter, OnExit));
                return;
                
                void OnEnter()
                {
                    CookMenuSystem.Show();
                    CookMenuSystem.OnClickOpenUIConfirmButton += OnRoundStart;
                    ActiveActions.Add(() => CookMenuSystem.OnClickOpenUIConfirmButton -= OnRoundStart);
                }
                
                void OnExit()
                {
                    CookMenuSystem.OnClickOpenUIConfirmButton -= OnRoundStart;
                }
            }

            private void OnRoundStart()
            {
                StateMachine.ChangeState(new OnRoundStart(OnEnter, OnExit));
                return;
                
                void OnEnter()
                {
                    // TODO 先從 QueueSystem 裡檢測是否還有排隊的空位，之後再決定要不要生成 Customer
                }
                
                void OnExit()
                {
                }
            }
        #endregion
    }
}