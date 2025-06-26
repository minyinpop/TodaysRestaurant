using BATTLE.CARD.BATTLE;
using UnityEngine.EventSystems;

namespace BATTLE.CARD.STATE.TYPE
{
    internal class SelectedState : ICardState
    {
        private BattleCardBase Card { get; set; }
        
        public void Enter(BattleCardBase card)
        {
            Card = card;
        }
        
        public void Exit()
        {
            
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Card.OnPointerClick();
        }
    }
}