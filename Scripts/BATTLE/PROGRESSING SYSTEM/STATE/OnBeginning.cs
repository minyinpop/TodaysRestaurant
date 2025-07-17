using BATTLE.PROGRESSING_SYSTEM.STATE_MACHINE;

namespace BATTLE.PROGRESSING_SYSTEM.STATE
{
    internal class OnBeginning : IState
    {
        private ProgressingSystem ProgressingSystem;
        
        public void Enter(ProgressingSystem system)
        {
            ProgressingSystem = system;
            // TODO 相機聚焦敵人，過了幾秒後再返回正常位置，接著再繼續執行其它程式，或是進到下個狀態
            Exit(); // 開發用
        }

        public void Exit()
        {
            ProgressingSystem.SpawnInitiativeCoin();
        }
    }
}