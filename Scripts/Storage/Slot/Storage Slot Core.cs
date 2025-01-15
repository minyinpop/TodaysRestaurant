using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Storage.Slot
{
    public abstract class StorageSlotCore : MonoBehaviour
    {
        // 儲存格資訊
        [field: HideInInspector]
        public StorageSlotInfo storageSlotInfo;
        
        [field: Header("資料"), Tooltip("這個儲存格是哪一個 Storage Data SO 的 ?"), SerializeField]
        public StorageDataSO StorageDataSO { get; protected set; }
        
        [field: Header("組件"), Tooltip("物品的圖片組件"), SerializeField]
        protected Image itemImage;

        [field: Tooltip("數量的文字組件"), SerializeField]
        protected TextMeshProUGUI itemAmountTMP;

        /// <summary>
        /// 解鎖儲存格
        /// </summary>
        /// <param name="otherInfo"></param>
        public virtual void Unlock(StorageSlotInfo otherInfo) {}

        /// <summary>
        /// 更新格子
        /// </summary>
        public void Refresh()
        {
            if (storageSlotInfo.state is StorageSlotInfo.StorageSlotState.Locked)
                return;

            if (storageSlotInfo.item is null)
            {
                if (itemImage is not null)
                    itemImage.gameObject.SetActive(false);
                
                if (itemAmountTMP is not null)
                    itemAmountTMP.gameObject.SetActive(false);
            }
            else
            {
                if (itemImage is not null)
                {
                    itemImage.gameObject.SetActive(true);
                    itemImage.sprite = storageSlotInfo.item.Sprite;
                }

                if (itemAmountTMP is not null)
                {
                    itemAmountTMP.gameObject.SetActive(true);
                    itemAmountTMP.text = $"{storageSlotInfo.itemAmount}";
                }
            }
        }

        /// <summary>
        /// 使用外部資訊更新格子
        /// </summary>
        /// <param name="otherInfo"></param>>
        public virtual void Refresh(StorageSlotInfo otherInfo)
        {
            if (otherInfo.state is StorageSlotInfo.StorageSlotState.Locked)
                return;
            
            storageSlotInfo = otherInfo;

            if (storageSlotInfo.item is null)
            {
                if (itemImage is not null)
                    itemImage.gameObject.SetActive(false);
                
                if (itemAmountTMP is not null)
                    itemAmountTMP.gameObject.SetActive(false);
            }
            else
            {
                if (itemImage is not null)
                {
                    itemImage.gameObject.SetActive(true);
                    itemImage.sprite = storageSlotInfo.item.Sprite;
                }

                if (itemAmountTMP is not null)
                {
                    itemAmountTMP.gameObject.SetActive(true);
                    itemAmountTMP.text = $"{otherInfo.itemAmount}";
                }
            }
        }
    }
}
