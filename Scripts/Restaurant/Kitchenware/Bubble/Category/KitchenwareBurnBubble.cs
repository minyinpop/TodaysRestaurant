using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware.Bubble.Category
{
    public class KitchenwareBurnBubble : BubbleBase
    {
        [field: Header("自身組件")]
        [field: SerializeField] public Button Button { get; private set; }
        
        private KitchenwareCook KitchenwareCook { get; set; }
        
        private void OnEnable() => Button.onClick.AddListener(OnClick);
        
        private void OnDisable() => Button.onClick.RemoveListener(OnClick);

        public override void OnClick() => KitchenwareCook.OnClickBubble();
        
        public override void Init(KitchenwareCook cook) => KitchenwareCook = cook;
    }
}