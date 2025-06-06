namespace BATTLE.CARD.MANAGER
{
    internal interface ICardSystemHandler
    {
        public void BeginDrag(Card draggedCard);
        public void EndDrag();
    }
}