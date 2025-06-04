using Item.Settings;
using UnityEngine;

namespace Item.Base
{
    [CreateAssetMenu(menuName = "Today's Restaurant/Item/Ingredient", fileName = "New Data", order = 1)]
    internal class Ingredient : ScriptableObject, ITem
    {
        [field: Header("資訊設定")]
        [field: SerializeField] internal InformationSettings InformationSettings { get; private set; }
        [field: Header("堆疊設定")]
        [field: SerializeField] internal StackSettings StackSettings { get; private set; }
        [field: Header("烹飪設定")]
        [field: SerializeField] internal CookSettings CookSettings { get; private set; }
    }
}