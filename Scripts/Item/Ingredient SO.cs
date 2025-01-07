using UnityEngine;

namespace Item
{
    [CreateAssetMenu(menuName = "Item/New Ingredient", fileName = "New Data")]
    public class IngredientSO : ScriptableObject, ITem
    {
        // 編號
        [field: SerializeField] public int ID { get; private set; }
        // 圖片
        [field: SerializeField] public Sprite Sprite { get; private set; }
        // 可否堆疊
        [field: SerializeField] public bool Stackable { get; private set; }
        // 最大堆疊數
        [field: SerializeField] public int MaxStack { get; private set; }
    }
}
