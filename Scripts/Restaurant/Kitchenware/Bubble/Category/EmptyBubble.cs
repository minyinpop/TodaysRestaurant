using Restaurant.Kitchenware.Cook_Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware.Bubble.Category
{
    public class EmptyBubble : BubbleBase
    {
        [field: Header("自身組件")]
        [field: SerializeField] private Button Button { get; set; }
        
        private KitchenwareCookMenu CookMenu { get; set; }
        
        private void OnEnable() => Button.onClick.AddListener(OnClick);
        
        private void OnDisable() => Button.onClick.RemoveListener(OnClick);
        
        public override void PlayerEnter() => Button.interactable = true;
        
        public override void PlayerLeave() => Button.interactable = false;

        public override void Init(KitchenwareCookMenu cookMenu) => CookMenu = cookMenu;
    }
}