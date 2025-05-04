using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware.Bubble.Category
{
    public class KitchenwareEmptyBubble : BubbleBase
    {
        [field: Header("自身組件")]
        [field: SerializeField] private Button Button { get; set; }

        public override void PlayerEnter() => Button.interactable = true;
        
        public override void PlayerLeave() => Button.interactable = false;
    }
}