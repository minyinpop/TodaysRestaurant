using System.Collections.Generic;
using Storage.Slot.Abstract.Base;
using UnityEngine;

namespace Storage.Root.Frontend
{
    public class Storage : MonoBehaviour
    {
        [Tooltip("儲物介面的資料庫。")]
        [SerializeField]
        private Backend.ScriptableObject.Storage storageData;
        
        [Tooltip("儲物介面的所有儲物格，\n先後會影響儲物順序。")]
        [SerializeField]
        private List<StorageSlot> slots;

        /// <summary>
        /// 刷新所有的儲物格介面。
        /// </summary>
        public void Refresh()
        {
            Debug.Log($"更新 {gameObject.name} 介面");
            
            for (var i = 0; i < slots.Count; i++)
            {
                // 檢查儲物介面的資料庫是否與儲物介面一樣。
                if (i > storageData.slotDataList.Count - 1)
                {
                    Debug.LogWarning($"{storageData.name} 的資料數量比儲物介面少");
                    return;
                }

                slots[i].Refresh(storageData.slotDataList[i]);
            }
        }
    }
}