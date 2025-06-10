using BATTLE.STATE_MACHINE.BASE;
using UnityEngine;

namespace BATTLE.STATE_MACHINE.CATEGORY
{
    internal class InitiativeSelectionState : IBattleState
    {
        public void Enter()
        {
            Debug.Log("Flip Coin!");
        }

        public void Exit()
        {
            
        }
    }
}