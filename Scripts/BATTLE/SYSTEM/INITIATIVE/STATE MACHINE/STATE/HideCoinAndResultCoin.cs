namespace BATTLE.SYSTEM.INITIATIVE.STATE_MACHINE.STATE
{
    internal class HideCoinAndResultCoin : IInitiativeState
    {
        private InitiativeSystem InitiativeSystem;
        
        public void Enter(InitiativeSystem system)
        {
            InitiativeSystem = system;
        }
    }
}