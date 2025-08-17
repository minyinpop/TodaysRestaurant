namespace BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.STATE_MACHINE
{
    internal interface IState
    {
        public void OnEnter(BattleCardBase card);
    }
}