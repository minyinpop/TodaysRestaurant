using Card_Battle_System.Main_System.State_Machine;
using Card_Battle_System.Main_System.State_Machine.State;
using UnityEngine;

namespace Card_Battle_System.Main_System
{
    internal sealed class CardBattleSystem : MonoBehaviour
    {
        private readonly StateMachine StateMachine = new();
        
        private void Start()
        {
            OnBattleStart();
        }

        #region StateMachine
            #region OnBattleStart
                private void OnBattleStart()
                {
                    StateMachine.ChangeState(new OnBattleStart(OnBattleStartEnter, OnBattleStartExit));
                }

                private void OnBattleStartEnter()
                {
                }

                private void OnBattleStartExit()
                {
                }
            #endregion
        #endregion
    }
}