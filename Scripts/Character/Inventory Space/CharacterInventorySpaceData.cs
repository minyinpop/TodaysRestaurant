using System.Collections.Generic;
using UnityEngine;

namespace Character.Inventory_Space
{
    [CreateAssetMenu(fileName = "New Character Inventory Data", menuName = "Character Data/Inventory", order = 2)]
    public class CharacterInventorySpaceData : ScriptableObject
    {
        [field: Header("背包設定"), Tooltip("- 當前背包等級的索引。\n- 用來指示每個等級會有多少的儲物格。\n- 數值請勿超過 Storage Slot Quantity Per Level。"), SerializeField]
        public int BagLevelIndex { get; private set; }
        
        [field: Tooltip("- 當前背包等級所可用的儲物格數量。\n- 參數的值請勿超過 StorageSlotUI 的數量。"), SerializeField]
        public List<int> StorageSlotQuantityPerLevel { get; private set; }
    }
}
