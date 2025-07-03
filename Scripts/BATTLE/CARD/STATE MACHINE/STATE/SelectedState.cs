namespace BATTLE.CARD.STATE_MACHINE.STATE
{
    internal class SelectedState : ICardState
    {
        private CardBase Card;
        
        public void Enter(CardBase card)
        {
            Card = card;
        }

        public void Exit()
        {
            Card = null;
        }
        
        
        
        public void OnPointerEnter()
        {
            
        }

        public void OnPointerExit()
        {
            
        }

        public void OnPointerClick()
        {
            
        }
    }
}