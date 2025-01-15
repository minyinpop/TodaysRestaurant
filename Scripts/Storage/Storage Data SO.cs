using System.Collections.Generic;
using Item;
using Storage.Slot;
using UnityEngine;

namespace Storage
{
    [CreateAssetMenu(menuName = "Minyinpop/Storage Data", fileName = "New Storage Data", order = 2)]
    public class StorageDataSO : ScriptableObject
    {
        [field: Header("物品資料"), Tooltip("儲存格裡面的物品資料，數量表示可使用的格子的總數"), SerializeField]
        public List<StorageSlotInfo> StorageSlotInfos { get; private set; }
        
        /// <summary>
        /// 物品添加
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ItemCore item)
        {
            for (var i = 0; i < StorageSlotInfos.Count; i++)
            {
                if (StorageSlotInfos[i].state is StorageSlotInfo.StorageSlotState.Locked)
                    continue;

                if (StorageSlotInfos[i].item is null)
                {
                    StorageSlotInfos[i] = new StorageSlotInfo
                    {
                        state = StorageSlotInfos[i].state,
                        item = item,
                        itemAmount = StorageSlotInfos[i].itemAmount + 1
                    };
                    
                    break;
                }
                
                if (StorageSlotInfos[i].item != item)
                    continue;
                
                if (!StorageSlotInfos[i].item.Stackable)
                    continue;

                if (StorageSlotInfos[i].itemAmount >= StorageSlotInfos[i].item.MaxStack)
                    continue;

                StorageSlotInfos[i] = new StorageSlotInfo
                {
                    state = StorageSlotInfos[i].state,
                    item = item,
                    itemAmount = StorageSlotInfos[i].itemAmount + 1
                };

                break;
            }
        }
    }
}
