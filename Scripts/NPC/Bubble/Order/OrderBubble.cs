using UnityEngine;

namespace NPC.Bubble.Order
{
    public abstract class OrderBubble : MonoBehaviour
    {
        /// <summary>
        /// 用於切換按鈕是否可以互動的類。
        /// </summary>
        /// <param name="interactable"> 是否可以互動。 </param>
        public virtual void ChangeButtonInteractable(bool interactable)
        {
        }
    }
}