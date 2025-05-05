using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware.Bubble.Category
{
    public class KitchenwareCookBubble : BubbleBase
    {
        [field: Header("自身組件")]
        [field: SerializeField] private Button Button { get; set; }
        [field: SerializeField] private Image PlayImage { get; set; }
        
        public override void PlayerEnter() => Button.interactable = true;
        
        public override void PlayerLeave() => Button.interactable = false;
    }
}