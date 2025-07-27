namespace BATTLE.SYSTEM.INITIATIVE.STATE_MACHINE.STATE
{
    internal class ReadyToTossCoin : IInitiativeState
    {
        private InitiativeSystem InitiativeSystem;
        
        public void Enter(InitiativeSystem system)
        {
            InitiativeSystem = system;
            InitiativeSystem.SpawnCoin();
            InitiativeSystem.MoveCoinToReadyPoint();
        }
    }
}