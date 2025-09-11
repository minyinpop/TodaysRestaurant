using Initiative_System.System.Child;
using Initiative_System.System.Main.State_Machine;
using Initiative_System.System.Main.State_Machine.State;
using UnityEngine;

namespace Initiative_System.System.Main
{
    internal sealed class InitiativeSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private ScreenMaskSystem ScreenMaskSystem;
        [field: SerializeField] private InitiativeCoinSystem InitiativeCoinSystem;

        private readonly StateMachine StateMachine = new();

        private void Start()
        {
            OnPrepareToToss();
        }

        #region State Machine
            #region OnPrepareToToss
                private void OnPrepareToToss()
                {
                    StateMachine.ChangeState(new OnPrepareToToss(OnPrepareToToss_Enter, OnPrepareToToss_Exit));
                }

                private void OnPrepareToToss_Enter()
                {
                    ScreenMaskSystem.FadeIn(() =>
                    {
                        // TODO Show Coin
                    });
                }
                
                private void OnPrepareToToss_Exit()
                {
                }
            #endregion
        #endregion
    }
}