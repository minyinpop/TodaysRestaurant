using UnityEngine;

namespace Bubble.Order
{
    public abstract class OrderBubble : MonoBehaviour
    {
        /// <summary>
        /// 用於切換按鈕是否可以互動的方法。
        /// 不需要傳入任何 Property。
        /// 用於氣泡內部的條件式判斷。
        /// </summary>
        public virtual void ChangeButtonInteractable()
        {
        }
        
        /// <summary>
        /// 用於切換按鈕是否可以互動的方法。
        /// 需要傳入 bool。
        /// 用於直接切換氣泡的互動。
        /// </summary>
        /// <param name="interactable"> 可不可以互動的 bool。 </param>
        public virtual void ChangeButtonInteractable(bool interactable)
        {
        }
    }
}
