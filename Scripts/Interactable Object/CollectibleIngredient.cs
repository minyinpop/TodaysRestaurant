using Interactable_Object.Base;
using UnityEngine;
using Utility;

namespace Interactable_Object
{
    internal class CollectibleIngredient : InteractableObjectBase
    {
        [field: SerializeField] private SpriteRenderer SpriteRenderer { get; set; }
        private Material Material { get; set; }

        internal static event System.Action InteractEvent;

        private void Awake()
        {
            var info = $"\n遊戲物件：{gameObject.name}\n遊戲組件：{GetType().Name}\n";
            if (Tools.CheckNull(!SpriteRenderer, $"AttributeData 未被掛載！{info}")) enabled = false;
            Material = SpriteRenderer.material;
        }

        internal override void Select() => Material.EnableKeyword("OUTBASE_ON");
        internal override void Deselect() => Material.DisableKeyword("OUTBASE_ON");
        internal override bool Interact()
        {
            InteractEvent?.Invoke();
            Destroy(gameObject);
            return true;
        }
    }
}