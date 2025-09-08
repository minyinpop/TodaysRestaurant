using Card_Battle_System.Object.Card.Base;
using UnityEngine;

namespace Card_Battle_System.Object.Card_Slot
{
    internal sealed class CardSlot : MonoBehaviour
    {
        private ICard Card;

        /// <summary>
        /// 嘗試添加卡片，並回傳是否添加成功
        /// </summary>
        /// <param name="card">卡片的資料</param>
        /// <returns>是否添加成功</returns>
        public bool Set(ICard card)
        {
            if (!IsEmpty()) return false;
            
            Card = card;
            return true;
        }

        /// <summary>
        /// 嘗試獲取儲存的卡片，並回傳是否獲取成功
        /// </summary>
        /// <param name="card">回傳卡片的資料</param>
        /// <returns>是否獲取成功</returns>
        public bool Get(out ICard card)
        {
            if (IsEmpty())
            {
                card = null;
                return false;
            }

            card = Card;
            Card = null;
            return true;
        }

        /// <summary>
        /// 判斷卡槽是否為空
        /// </summary>
        /// <returns>是否為空</returns>
        public bool IsEmpty()
        {
            return Card is null;
        }
    }
}