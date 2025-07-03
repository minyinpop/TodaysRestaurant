using UnityEngine;

namespace BATTLE.CARD.STATE_MACHINE.STATE
{
    internal class DeselectedState : ICardState
    {
        /// <summary>
        /// 
        /// </summary>
        public static event System.Func<CardBase, bool> Select;
        
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
            Debug.Log("鼠標進入卡片範圍");
        }

        public void OnPointerExit()
        {
            Debug.Log("鼠標退出卡片範圍");
        }

        public void OnPointerClick()
        {
            if (Select?.Invoke(Card) == true)
            {
                Debug.Log("卡片選擇成功");
            }
            else
            {
                Debug.Log("卡片選擇失敗");
            }
        }
    }
}