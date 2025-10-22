using System.Collections.Generic;
using System.Cook.Child.Cook_Menu.System.Main;
using System.Cook.Main.State_Machine;
using System.Cook.Main.State_Machine.State;
using UnityEngine;

namespace System.Cook.Main
{
    internal sealed class CookSystem : MonoBehaviour
    {
        [field: SerializeField] private CookMenuSystem CookMenuSystem;
        
        private readonly List<Action> ActiveActions = new();
        
        private void Start()
        {
            OnCookStart();
            // OnRoundStart();
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
                }
            }

            private void OnRoundStart()
            {
                StateMachine.ChangeState(new OnRoundStart(OnEnter, OnExit));
                return;
                
                void OnEnter()
                {
                    Debug.Log("Round Start.");
                }
                
                void OnExit()
                {
                }
            }
        #endregion
    }
}