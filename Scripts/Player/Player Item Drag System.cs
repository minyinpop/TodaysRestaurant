using System.Collections.Generic;
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
        // 輸入系統
        private InputManager _input;
        // 滑鼠位置
        private Vector2 MousePos => _input.Mouse.MousePos.ReadValue<Vector2>();
        
        // 點擊的儲存格暫存
        private StorageSlotCore _clickedSlot;
        // 放開的儲存格暫存
        private StorageSlotCore _releasedSlot;
        
        // 物品拖曳預覽暫存
        private GameObject _itemDragPreview;

        [field: Header("介面"), Tooltip("物品拖曳預覽的生成畫布組件"), SerializeField]
        private Canvas canvas;
        
        [field: Tooltip("哪一個畫布是用於偵測物品拖曳的 ?"), SerializeField]
        private GraphicRaycaster raycaster;
        
        [field: Header("物品拖曳"), Tooltip("物品拖曳預覽的預製件"), SerializeField]
        private GameObject itemDragPreviewPrefab;

        [field: Tooltip("物品拖曳預覽的偏移量"), SerializeField]
        private Vector2 itemDragPreviewOffset;

        [field: Header("標籤"), Tooltip("儲存格的標籤"), SerializeField]
        private string storageSlotTag = "Storage Slot";

        private void Awake()
        {
            _input = InputSystem.Input;
        }

        private void OnEnable()
        {
            _input.Mouse.LeftButton.started += OnLeftButtonClick;
            _input.Mouse.LeftButton.canceled += OnLeftButtonRelease;
        }

        private void Update()
        {
            if (_itemDragPreview is not null)
                _itemDragPreview.transform.position = MousePos + itemDragPreviewOffset;
        }

        private void OnDisable()
        {
            _input.Mouse.LeftButton.started -= OnLeftButtonClick;
            _input.Mouse.LeftButton.canceled -= OnLeftButtonRelease;
        }

        /// <summary>
        /// 當按下滑鼠左鍵
        /// </summary>
        /// <param name="context"></param>
        private void OnLeftButtonClick(InputAction.CallbackContext context)
        {
            _clickedSlot = StorageSlotDetect();

            if (_clickedSlot?.storageSlotInfo.item is null)
                return;
            
            _itemDragPreview = Instantiate(itemDragPreviewPrefab, canvas.transform);
            _itemDragPreview.GetComponent<Image>().sprite = _clickedSlot.storageSlotInfo.item.Sprite;
        }

        /// <summary>
        /// 當放開滑鼠左鍵
        /// </summary>
        /// <param name="context"></param>
        private void OnLeftButtonRelease(InputAction.CallbackContext context)
        {
            _releasedSlot = StorageSlotDetect();
            
            // " 點擊的儲存格 " 或 " 放開的儲存格 " 是空的
            if (_clickedSlot is null || _releasedSlot is null)
            {
                ResetData();
                return;
            }
            
            // " 點擊的儲存格 " 裡的物品是空的
            if (_clickedSlot.storageSlotInfo.item is null)
            {
                ResetData();
                return;
            }

            // " 放開的儲存格 " 是被鎖上的
            if (_releasedSlot.storageSlotInfo.state is StorageSlotInfo.StorageSlotState.Locked)
            {
                ResetData();
                return;
            }

            // " 點擊的儲存格 " 與 " 放開的儲存格 " 的屬性是一樣的
            if (_clickedSlot.storageSlotInfo.GetHashCode() == _releasedSlot.storageSlotInfo.GetHashCode())
            {
                ResetData();
                return;
            }

            // " 放開的儲存格 " 裡是沒有物品的
            if (_releasedSlot.storageSlotInfo.item is null)
            {
                _releasedSlot.storageSlotInfo = _clickedSlot.storageSlotInfo;
                _clickedSlot.storageSlotInfo = StorageSlotInfo.Reset(_clickedSlot.storageSlotInfo.state);
                
                _clickedSlot.Refresh();
                _releasedSlot.Refresh();
                
                // TODO: 更新與儲存格相符的 Storage Data SO ...
                
                ResetData();
                return;
            }
        }

        /// <summary>
        /// 偵測儲存格
        /// </summary>
        /// <returns></returns>
        private StorageSlotCore StorageSlotDetect()
        {
            var pointer = new PointerEventData(EventSystem.current)
            {
                position = MousePos
            };
            var results = new List<RaycastResult>();
            
            raycaster.Raycast(pointer, results);

            foreach (var result in results)
            {
                if (result.gameObject.layer != LayerMask.NameToLayer("UI"))
                    continue;

                if (!result.gameObject.CompareTag(storageSlotTag))
                    continue;

                return result.gameObject.GetComponent<StorageSlotCore>();
            }

            return null;
        }

        /// <summary>
        /// 重置資訊
        /// </summary>
        private void ResetData()
        {
            Destroy(_itemDragPreview);
            _itemDragPreview = null;

            _clickedSlot = null;
            _releasedSlot = null;
        }
    }
}