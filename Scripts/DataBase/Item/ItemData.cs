using UnityEngine;

namespace DataBase.Item
{
    /// <summary>
    /// 用來整合所有有關於到物品的抽象類。
    /// </summary>
    public abstract class ItemData : ScriptableObject
    {
        [field: Header("資料設定"), Tooltip("物品名稱的唯一識別碼。"), SerializeField]
        public int ID { get; private set; }
        
        [field: Header("物品的名稱，用於 UI。"), SerializeField]
        public string Name { get; private set; }
        
        [field: Tooltip("物品在遊戲中的圖示，用於 UI。"), SerializeField]
        public Sprite Sprite { get; private set; }
        
        [field: Header("堆疊設定"), Tooltip("同種物品可不可以堆疊在一起。"), SerializeField]
        public bool Stackable { get; private set; }
        
        [field: Tooltip("- 如果物品可以堆疊，那最大的堆疊數量是多少。\n- 就算是不可以堆疊的物品，也要設置成 1"), SerializeField]
        public int MaxStack { get; private set; }
    }
}
