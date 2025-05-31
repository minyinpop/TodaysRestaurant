using UnityEngine;

namespace Item.Ingredient
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Ingredient", fileName = "New Data", order = 1)]
    internal class Ingredient : ScriptableObject, ITem
    {
        [field: SerializeField] public InfoSettings InfoSettings { get; private set; }
        [field: SerializeField] public QuantitySettings QuantitySettings { get; private set; }
    }

    [System.Serializable]
    internal class InfoSettings
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    }

    [System.Serializable]
    internal class QuantitySettings
    {
        [field: SerializeField] public bool CanStack { get; private set; }
        [field: SerializeField] public int Quantity { get; private set; }
    }
}