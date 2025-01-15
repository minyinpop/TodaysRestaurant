using System.Collections.Generic;
using Item;
using Storage.Slot;
using UnityEngine;
using UnityEngine.Serialization;

namespace Storage
{
    [CreateAssetMenu(menuName = "Minyinpop/Storage Data", fileName = "New Storage Data", order = 2)]
    public class StorageDataSO : ScriptableObject
    {
        [field: Header("物品資料"), Tooltip("儲存格裡面的物品資料，數量表示可使用的格子的總數")]
        public List<StorageSlotInfo> storageSlotInfos;
        
        /// <summary>
        /// 物品添加
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ItemCore item)
        {
            for (var i = 0; i < storageSlotInfos.Count; i++)
            {
                if (storageSlotInfos[i].state is StorageSlotInfo.StorageSlotState.Locked)
                    continue;

                if (storageSlotInfos[i].item is null)
                {
                    storageSlotInfos[i] = new StorageSlotInfo
                    {
                        state = storageSlotInfos[i].state,
                        item = item,
                        itemAmount = storageSlotInfos[i].itemAmount + 1
                    };
                    
                    break;
                }
                
                if (storageSlotInfos[i].item != item)
                    continue;
                
                if (!storageSlotInfos[i].item.Stackable)
                    continue;

                if (storageSlotInfos[i].itemAmount >= storageSlotInfos[i].item.MaxStack)
                    continue;

                storageSlotInfos[i] = new StorageSlotInfo
                {
                    state = storageSlotInfos[i].state,
                    item = item,
                    itemAmount = storageSlotInfos[i].itemAmount + 1
                };

                break;
            }
        }
    }
}
