using System.Collections.Generic;
using Item;
using Storage.Slot;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using InputSystem = Input.InputSystem;

namespace Player
{
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
        private TempStorageSlotData _previewStorageSlotData;
        
        // 被點選的物品的快取資料。
        private TempStorageSlotData _selectedStorageSlotData;
        
        // 輸入端。
        private InputMap _input;
        
        // 滑鼠位置。
        // TODO: 把 MousePos 改道 InputSystem 裡面，這裡做抓取資料即可。
        private Vector2 MousePos => _input.Mouse.Position.ReadValue<Vector2>();

        private void Awake()
        {
            _input = InputSystem.input;
        }

        private void OnEnable()
        {
            _input.Mouse.LeftClick.performed += OnLeftClickPerformed;
        }

        private void LateUpdate()
        {
            // 如果拖曳預覽不存在的話，就不執行。
            if (_previewStorageSlotData.slot is not null)
                _previewStorageSlotData.slot.transform.position = MousePos;
        }

        private void OnDisable()
        {
            _input.Mouse.LeftClick.performed -= OnLeftClickPerformed;
        }

        /// <summary>
        /// 當 Left Click 被按下時所發生的事情。
        /// </summary>
        /// <param name="context"> 輸入系統的狀態。 </param>
        private void OnLeftClickPerformed(InputAction.CallbackContext context)
        {
            // TODO: 玩家滑鼠的各種互動。
            _selectedStorageSlotData = UpdateTempSlotData(false, MouseDetectedSlot());
            
            // 如果玩家沒有點擊到儲物格。
            if (_selectedStorageSlotData.slot is null)
            {
            }
            // 如果玩家點擊到儲物格。
            else
            {
                _selectedStorageSlotData = UpdateTempSlotData(true, _selectedStorageSlotData.slot);
                
                // 玩家在拖曳物品時，點擊了儲物格。
                if (_isDragging)
                {
                    // 玩家在拖曳物品的時候，點擊了空的儲物格。
                    if (_selectedStorageSlotData.slotData.itemData is null)
                    {
                        print("物品被放入了空的儲物格");
                    }
                    // 玩家在拖曳物品的時候，點擊了有物品的儲物格。
                    else
                    {
                        print("物品與該儲物格內的物品做邏輯比較");
                    }
                }
                // 玩家在空手時，點擊了儲物格，就判斷是否要生成物品預覽。
                else
                {
                    // 玩家在空手時，點擊了沒有儲存物品的儲物格，就直接清空暫存數據，並直接退出判斷。
                    if (_selectedStorageSlotData.slotData.itemData is null)
                        return;

                    // 玩家在空手時，點擊了有儲存物品的儲物格，就生成物品拖曳預覽，並且更新其介面。
                    _isDragging = true;
                    
                    _previewStorageSlotData = UpdateTempSlotData(true, Instantiate(itemDragPreviewPrefab, previewSpawnPoint));
                    _previewStorageSlotData.slotUI.Refresh(_selectedStorageSlotData.slotData);
                }
            }
        }

        /// <summary>
        /// 用來判斷滑鼠有沒有點擊到
        /// </summary>
        /// <returns></returns>
        private GameObject MouseDetectedSlot()
        {
            var pointer = new PointerEventData(EventSystem.current)
            {
                position = MousePos
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
        /// 用來更新儲物格的快取資料。
        /// </summary>
        /// <param name="fullData"> 是否只儲存儲物格的遊戲物件。 </param>>
        /// <param name="slot"> 儲物格的遊戲物件。 </param>
        /// <returns> 回傳薪的儲物格快取資料。 </returns>
        private static TempStorageSlotData UpdateTempSlotData(bool fullData, GameObject slot)
        {
            if (fullData)
            {
                return new TempStorageSlotData
                {
                    slot = slot,
                    slotUI = slot.GetComponent<StorageSlotUI>(),
                    slotData = slot.GetComponent<StorageSlotUI>().SlotData()
                };
            }

            return new TempStorageSlotData
            {
                slot = slot,
                slotUI = null,
                slotData = new StorageSlotData()
            };
        }

        /// <summary>
        /// 重置所有的儲物格的暫存的資料。
        /// </summary>
        private void ClearTempSlotData()
        {
            _previewStorageSlotData = new TempStorageSlotData();
            _selectedStorageSlotData = new TempStorageSlotData();
        }
    }
    
    /// <summary>
    /// 用來快速讀取儲物格的各項資料，只限於暫存專用。
    /// </summary>
    [System.Serializable]
    public struct TempStorageSlotData
    {
        // 儲物格的遊戲物件。
        public GameObject slot;
        
        // 儲物格的介面資料。
        public StorageSlotUI slotUI;
        
        // 儲物格的儲物資料。
        public StorageSlotData slotData;
    }
}
