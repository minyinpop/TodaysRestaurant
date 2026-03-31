using System;
using System.Collections.Generic;
using Common.Item_Slot.New.Child;
using Common.Item.Data;
using UI_System.Player_UI_System.Child.Hotbar_UI_System.Object;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Hotbar_UI_System.System
{
    public sealed class HotbarUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private HotbarUI hotbarUI;

        private bool _isHotbarEnabled = true;
        
        private void Awake()
        {
            if (hotbarUI is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(hotbarUI)} cannot be null.");
                Destroy(gameObject);
            }
            
            SetHotbarUI(_isHotbarEnabled);
        }

        public void SetHotbarUI(bool isEnabled)
        {
            _isHotbarEnabled = isEnabled;
            hotbarUI.gameObject.SetActive(_isHotbarEnabled);
        }

        #region 裝置輸入
            public void UseSelectedHotbarSlotItem()
            {
                if (_isHotbarEnabled)
                {
                    hotbarUI.UseSelectedHotbarSlotItem();
                }
            }
            
            public void PerformHotbar(int hotbarIndex)
            {
                if (_isHotbarEnabled)
                {
                    hotbarUI.PerformHotbar(hotbarIndex);
                }
            }
        #endregion
        
        public bool AddItem(IItem item)
        {
            #region 條件檢查
                if (item is null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (!_isHotbarEnabled)
                {
                    Debug.Log("快捷欄尚未開啟，故無法添加物品。");
                    throw new InvalidOperationException(nameof(_isHotbarEnabled));
                }
            #endregion
            
            return hotbarUI.AddItem(item);
        }

        public bool RemoveItem(ItemSO item)
        {
            #region 條件檢查
                if (item is null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (!_isHotbarEnabled)
                {
                    Debug.Log("快捷欄尚未開啟，故無法移除物品。");
                    throw new InvalidOperationException(nameof(_isHotbarEnabled));
                }
            #endregion

            return hotbarUI.RemoveItem(item);
        }

        public IReadOnlyList<HotbarSlot> GetHotbarSlots()
        {
            return hotbarUI.HotbarSlots;
        }
    }
}