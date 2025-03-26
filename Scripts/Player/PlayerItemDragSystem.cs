using System.Collections.Generic;
using DataBase.Item;
using Storage.Slot;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using InputSystem = Input.InputSystem;

namespace Player
{
    /// <summary>
    /// 用來控制玩家拖曳物品的類。
    /// </summary>
    public class PlayerItemDragSystem : MonoBehaviour
    {
        [Header("拖曳預覽相關"), Tooltip("用來顯示物品拖曳時的預覽，用於 UI。"), SerializeField]
        private GameObject itemDragPreviewPrefab;

        [Tooltip("用於檢測滑鼠要在哪一個 Canvas 上做偵測。"), SerializeField]
        private GraphicRaycaster raycaster;
        
        [Tooltip("儲物格拖曳預覽要生成在哪一個遊戲物件的底下。"), SerializeField]
        private Transform previewSpawnPoint;
        
        [Tooltip("儲物格的標籤，用於判斷滑鼠有沒有點擊到儲物格。"), SerializeField]
        private string storageSlotTag;
        
        // 用來判斷物品是否正在拖曳的狀態。
        private bool _isDragging;
        
        // 物品拖曳預覽的快取資料。
        private GameObject _previewSlot;
        private StorageSlotUI _previewSlotUI;
        private StorageSlotData _previewSlotData;
        
        // 被點選的儲物格的快取資料。
        private GameObject _selectedSlot;
        private StorageSlotUI _selectedSlotUI;
        private StorageSlotData _selectedSlotData;
        
        private void OnEnable()
        {
            InputSystem.input.Mouse.LeftClick.performed += OnLeftClickPerformed;
        }

        private void LateUpdate()
        {
            // 如果拖曳預覽不存在的話，就不執行。
            if (_isDragging)
                _previewSlot.transform.position = InputSystem.MousePos();
        }

        private void OnDisable()
        {
            InputSystem.input.Mouse.LeftClick.performed -= OnLeftClickPerformed;
        }

        /// <summary>
        /// 當 Left Click 被按下時所發生的事情。
        /// </summary>
        /// <param name="context"> 輸入系統的狀態。 </param>
        private void OnLeftClickPerformed(InputAction.CallbackContext context)
        {
            _selectedSlot = MouseDetectedSlot();

            if (_selectedSlot is null)
            {
                // TODO: 未來可以製作丟物品的判斷，像是點擊到了空白的區域，可以把物品給丟出來。
            }
            else
            {
                _selectedSlotUI = _selectedSlot.GetComponent<StorageSlotUI>();
                _selectedSlotData = _selectedSlotUI.SlotData();

                // 如果被點擊的儲物格是上鎖的，就直接離開判斷。
                if (_selectedSlotData.isLocked)
                {
                    ClearSelectedSlotData();
                    return;
                }
                    
                if (_isDragging)
                {
                    // 如果玩家在拖曳物品時，點擊了沒有存放物品的儲物格，就直接把物品給存放進去。
                    if (_selectedSlotData.itemData is null)
                    {
                        Destroy(_previewSlot);
                        
                        _selectedSlotData = UpdateSlotData(_previewSlotData.itemData, _previewSlotData.itemQuantity);
                        _selectedSlotUI.Refresh(_selectedSlotData);
                        
                        _isDragging = false;
                        
                        ClearSelectedSlotData();
                        ClearPreviewSlotData();
                    }
                    // 如果玩家在拖曳物品時，點擊了有存放物品的儲物格，就判斷物品可否堆疊。
                    else
                    {
                        // 如果被拖曳的物品與儲物格裡的物品不同，或是不可堆疊，就直接與該儲物格裡的物品做互換。
                        if (_previewSlotData.itemData != _selectedSlotData.itemData || !_selectedSlotData.itemData.Stackable)
                        {
                            (_previewSlotData, _selectedSlotData) = (_selectedSlotData, _previewSlotData);

                            _previewSlotUI.Refresh(_previewSlotData);
                            _selectedSlotUI.Refresh(_selectedSlotData);
                        }
                        // 如果被拖曳的物品，與儲物格裡的物品相同，並且可以一次堆疊完。
                        else if (_previewSlotData.itemQuantity + _selectedSlotData.itemQuantity <= _selectedSlotData.itemData.MaxStack)
                        {
                            Destroy(_previewSlot);
                            
                            _selectedSlotData = UpdateSlotData(_previewSlotData.itemData, _previewSlotData.itemQuantity + _selectedSlotData.itemQuantity);
                            _selectedSlotUI.Refresh(_selectedSlotData);
                            
                            _isDragging = false;
                            
                            ClearSelectedSlotData();
                            ClearPreviewSlotData();
                        }
                        // 如果被拖曳的物品，與儲物格裡的物品相同，但是一次堆疊不完。
                        else if (_previewSlotData.itemQuantity + _selectedSlotData.itemQuantity > _selectedSlotData.itemData.MaxStack)
                        {
                            var remainingSpace = _selectedSlotData.itemData.MaxStack - _selectedSlotData.itemQuantity;

                            _previewSlotData = UpdateSlotData(_previewSlotData.itemData, _previewSlotData.itemQuantity - remainingSpace);
                            _previewSlotUI.Refresh(_previewSlotData);

                            _selectedSlotData = UpdateSlotData(_selectedSlotData.itemData, _selectedSlotData.itemData.MaxStack);
                            _selectedSlotUI.Refresh(_selectedSlotData);
                            
                            ClearSelectedSlotData();
                        }
                    }
                }
                else
                {
                    // 如果玩家在沒有拖曳物品的時候，點擊了沒有儲存物品的儲物格，就不判斷任何事情。
                    if (_selectedSlotData.itemData is null)
                    {
                        ClearSelectedSlotData();
                    }
                    // 如果玩家在沒有拖曳物品的時候，點擊了有儲存物品的儲物格，就把物品給拿起來。
                    else
                    {
                        _previewSlot = Instantiate(itemDragPreviewPrefab, previewSpawnPoint);
                        _previewSlotUI = _previewSlot.GetComponent<StorageSlotUI>();
                        _previewSlotData = _selectedSlotUI.SlotData();
                        _previewSlotUI.Refresh(_previewSlotData);
                        
                        _isDragging = true;
                        
                        _selectedSlotUI.Clear();
                        ClearSelectedSlotData();
                    }
                }
            }
        }
        
        /// <summary>
        /// 用來判斷滑鼠有沒有點擊到。
        /// </summary>
        /// <returns> 返回被點擊到的儲物格，只會返回最前面的儲物格。 </returns>
        private GameObject MouseDetectedSlot()
        {
            var pointer = new PointerEventData(EventSystem.current)
            {
                position = InputSystem.MousePos()
            };
            var resultList = new List<RaycastResult>();

            raycaster.Raycast(pointer, resultList);

            // 判斷滑鼠點擊時，在所有可被檢測到的 UI 組件找尋儲物格。
            foreach (var result in resultList)
            {
                if (result.gameObject.layer != LayerMask.NameToLayer("UI"))
                    continue;

                if (!result.gameObject.CompareTag(storageSlotTag))
                    continue;

                return result.gameObject;
            }

            // 玩家沒有點擊到儲物格，回傳空的參數。
            return null;
        }
        
        /// <summary>
        /// 用來更新儲物格的資料。
        /// </summary>
        /// <param name="itemData"> 儲物格的物品的資料。 </param>
        /// <param name="itemQuantity"> 儲物格的物品的數量。 </param>
        /// <returns> 回傳新的儲物格資料。 </returns>
        private static StorageSlotData UpdateSlotData(ItemData itemData, int itemQuantity)
        {
            return new StorageSlotData
            {
                isLocked = false,
                itemData = itemData,
                itemQuantity = itemQuantity,
            };
        }
        
        /// <summary>
        /// 重置物品拖曳預覽的快取資料。
        /// </summary>
        private void ClearPreviewSlotData()
        {
            _previewSlot = null;
            _previewSlotUI = null;
            _previewSlotData = new StorageSlotData();
        }
        
        /// <summary>
        /// 重置被點擊的儲物格的快取資料。
        /// </summary>
        private void ClearSelectedSlotData()
        {
            _selectedSlot = null;
            _selectedSlotUI = null;
            _selectedSlotData = new StorageSlotData();
        }
    }
}
