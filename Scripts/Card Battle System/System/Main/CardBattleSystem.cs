using Card_Battle_System.System.Child;
using Card_Battle_System.System.Main.State_Machine;
using Card_Battle_System.System.Main.State_Machine.State;
using UnityEngine;

namespace Card_Battle_System.System.Main
{
    internal sealed class CardBattleSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        
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