using System.Collections.Generic;
using Item;
using Storage.Slot;
using Storage.Slot.Category;
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
        
        // 物品預覽儲物格的遊戲物件。
        private GameObject _itemPreview;
        
        // 物品預覽儲物格的資料庫。
        private StorageSlotData _itemPreviewSlotData;
        
        // 儲物格的遊戲物件，用於獲取第一次點擊到的儲物格的所有訊息。
        private GameObject _selectedSlot;
        private GameObject _tempSelectedSlot;

        // _selectedSlot 的介面資訊，用於快速讀取資料用。
        private StorageSlotUI _selectedSlotUI;
        private StorageSlotUI _tempSelectedSlotUI;
        
        // _selectedSlot 的儲物格的資料，用於快速讀取資料用。
        private StorageSlotData _selectedSlotData;
        private StorageSlotData _tempSelectedSlotData;
        
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
            if (_itemPreview is not null)
                _itemPreview.transform.position = MousePos;
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
        /// 重置所有的儲物格的暫存的資料。
        /// </summary>
        private void Reset()
        {
            _selectedSlot = null;
            _tempSelectedSlot = null;
            
            _selectedSlotUI = null;
            _tempSelectedSlotUI = null;
            
            _selectedSlotData = new StorageSlotData();
            _tempSelectedSlotData = new StorageSlotData();
        }
    }
}
