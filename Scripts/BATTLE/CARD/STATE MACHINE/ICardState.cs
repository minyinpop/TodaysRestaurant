namespace BATTLE.CARD.STATE_MACHINE
{
    internal interface ICardState
    {
        public void Enter(CardBase card);
        public void Exit();
        
        
        
        public void OnPointerEnter();
        public void OnPointerExit();
        public void OnPointerClick();
    }
}