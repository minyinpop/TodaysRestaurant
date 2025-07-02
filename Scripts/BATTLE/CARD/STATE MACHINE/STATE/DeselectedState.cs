using UnityEngine;

namespace BATTLE.CARD.STATE_MACHINE.STATE
{
    internal class DeselectedState : ICardState
    {
        private CardBase Card;
        
        public void Enter(CardBase card)
        {
            Card = card;
        }

        public void Exit()
        {
            
        }
        
        
        
        public void OnPointerEnter()
        {
            Debug.Log("鼠標進入卡片範圍");
        }

        public void OnPointerExit()
        {
            Debug.Log("鼠標退出卡片範圍");
        }

        public void OnPointerClick()
        {
            Debug.Log("鼠標點擊卡片");
        }
    }
}