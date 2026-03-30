using System;
using System.Collections.Generic;
using Common.Button;
using Common.Item_Slot.New.Child;
using Common.Item.Data;
using UI_System.Player_UI_System.Child.Backpack_UI_System.Object;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Backpack_UI_System.System
{
    public sealed class BackpackUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private GameObject mask;
        [field: SerializeField] private BackpackUI backpackUI;
        [field: SerializeField] private Button fastButton;

        private bool _isBackpackEnabled = true;

        private void Awake()
        {
            if (mask is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(mask)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (backpackUI is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(backpackUI)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (fastButton is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(fastButton)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            fastButton.OnClick += RequireBackpackUI;
            
            SetBackpackUI(_isBackpackEnabled);
        }
        
        private void OnDestroy()
        {
            fastButton.OnClick -= RequireBackpackUI;
        }

        #region 裝置輸入
            public void SetBackpackUI(bool isEnabled)
            {
                _isBackpackEnabled = isEnabled;
                
                if (_isBackpackEnabled)
                {
                    fastButton.gameObject.SetActive(true);
                }
                else
                {
                    mask.SetActive(false);
                    backpackUI.gameObject.SetActive(false);
                    
                    fastButton.gameObject.SetActive(false);
                }
            }

            public void RequireBackpackUI()
            {
                if (_isBackpackEnabled)
                {
                    mask.SetActive(!mask.gameObject.activeSelf);
                    backpackUI.gameObject.SetActive(!backpackUI.gameObject.activeSelf);
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

                if (!_isBackpackEnabled)
                {
                    Debug.Log("背包尚未開啟，故無法添加物品。");
                    throw new InvalidOperationException(nameof(_isBackpackEnabled));
                }
            #endregion

            return backpackUI.AddItem(item);
        }

        public IReadOnlyList<BackpackSlot> GetBackpackSlots()
        {
            return backpackUI.BackpackSlots;
        }
    }
}