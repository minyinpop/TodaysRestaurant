using Item.Base;
using UnityEngine;

namespace Item.Data.Ingredient
{
    [CreateAssetMenu(menuName = "Today's Restaurant/Item/Ingredient", fileName = "New Data", order = 1)]
    internal class IngredientBase : ItemBase
    {
        [field: SerializeField] internal InfoSettings InfoSettings { get; private set; }
        [field: SerializeField] internal StackSettings StackSettings { get; private set; }
    }

    [System.Serializable]
    internal class InfoSettings
    {
        [field: SerializeField] internal string Name { get; private set; }
        [field: SerializeField] internal Sprite Sprite { get; private set; }
    }
    
    [System.Serializable]
    internal class StackSettings
    {
        [field: SerializeField] internal bool CanStack { get; private set; }
        [field: Range(1, 99), SerializeField] internal int MaxStack { get; private set; }
    }
}