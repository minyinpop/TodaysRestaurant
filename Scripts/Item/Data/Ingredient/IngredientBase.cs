using Item.Base;
using UnityEngine;

namespace Item.Data.Ingredient
{
    [CreateAssetMenu(menuName = "Today's Restaurant/Item/Ingredient", fileName = "New Data", order = 1)]
    internal class IngredientBase : ItemBase
    {
        [field: SerializeField] internal InfoSettings InfoSettings { get; private set; }
        [field: SerializeField] internal StackSettings StackSettings { get; private set; }

        internal override InfoSettings GetInfoSettings() => InfoSettings;
        internal override StackSettings GetStackSettings() => StackSettings;
    }
}