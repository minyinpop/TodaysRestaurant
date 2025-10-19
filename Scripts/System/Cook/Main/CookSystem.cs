using System.Cook.Cook_Menu.System.Main;
using System.Cook.Main.State_Machine;
using System.Cook.Main.State_Machine.State;
using UnityEngine;

namespace System.Cook.Main
{
    internal sealed class CookSystem : MonoBehaviour
    {
        [field: SerializeField] private CookMenuSystem CookMenuSystem;
        
        private void Start()
        {
            OnCookStart();
        }

        #region State Machine
            private readonly StateMachine CookStateMachine;
            private void OnCookStart()
            {
                CookStateMachine.ChangeState(new OnCookStart(OnEnter, OnExit));
                return;
                
                void OnEnter()
                {
                }
                
                void OnExit()
                {
                }
            }
        #endregion
    }
}