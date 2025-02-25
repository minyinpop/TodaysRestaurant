using Item.Base;
using UnityEngine;

namespace Item.Category
{
    /// <summary>
    /// 用來設定食材的資料，繼承物品接口。
    /// </summary>
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Item/Ingredient", order = 1)]
    public class IngredientData : ScriptableObject, ITem
    {
        [field: Header("基本設定"), Tooltip("物品編號。"), SerializeField]
        public int ID { get; private set; }
        
        [field: Tooltip("物品圖片。"), SerializeField]
        public Sprite Sprite { get; private set; }
        
        [field: Header("堆疊設定"), Tooltip("堆疊許可。"), SerializeField]
        public bool Stackable { get; private set; }
        
        [field: Tooltip("最大堆疊量。 ( 就算是不可堆疊，也要設定成 1 )"), SerializeField]
        public int MaxStack { get; private set; }
    }
}
