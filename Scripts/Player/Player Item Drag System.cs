using System.Collections.Generic;
using Storage.Slot.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using InputSystem = Input.InputSystem;

namespace Player
{
    public class PlayerItemDragSystem : MonoBehaviour
    {
        private InputManager _input;
        private Vector2 MousePos => _input.Mouse.MousePos.ReadValue<Vector2>();
        
        private StorageSlotCore _clickedStorageSlot;
        private StorageSlotCore _releasedStorageSlot;
        
        private GameObject _itemDragPreview;

        [field: Tooltip("物品拖曳預覽的預製件"), SerializeField]
        private GameObject previewPrefab;

        [field: Tooltip("物品拖曳預覽的偏移量"), SerializeField]
        private Vector2 previewOffset;
        
        [Tooltip("物品拖曳預覽的生成點"), SerializeField]
        private Transform previewSpawnPoint;

        [field: Tooltip("哪一個畫布的圖片偵測器會被使用 ?"), SerializeField]
        private GraphicRaycaster raycaster;

        [field: Tooltip("儲存格的標籤"), SerializeField]
        private string storageSlotTag = "Storage Slot";

        private void Awake()
        {
            _input = InputSystem.Input;
        }

        private void OnEnable()
        {
            _input.Mouse.LeftButton.started += OnLeftButtonPressed;
            _input.Mouse.LeftButton.canceled += OnLeftButtonReleased;
        }

        private void Update()
        {
            if (_itemDragPreview is not null)
                _itemDragPreview.transform.position = MousePos + previewOffset;
        }

        private void OnDisable()
        {
            _input.Mouse.LeftButton.started -= OnLeftButtonPressed;
            _input.Mouse.LeftButton.canceled -= OnLeftButtonReleased;
        }

        /// <summary>
        /// 當 " Left Button " 按鈕按下時
        /// </summary>
        /// <param name="context"></param>
        private void OnLeftButtonPressed(InputAction.CallbackContext context)
        {
            _clickedStorageSlot = StorageSlotDetect();

            if (_clickedStorageSlot?.StorageSlotData.item is null)
                return;

            _itemDragPreview = Instantiate(previewPrefab, previewSpawnPoint);
            _itemDragPreview.GetComponent<Image>().sprite = _clickedStorageSlot.StorageSlotData.item.Sprite;
        }

        /// <summary>
        /// 當 " Left Button " 按鈕放開時
        /// </summary>
        /// <param name="context"></param>
        private void OnLeftButtonReleased(InputAction.CallbackContext context)
        {
            _releasedStorageSlot = StorageSlotDetect();

            Destroy(_itemDragPreview);
            _itemDragPreview = null;
            
            _clickedStorageSlot = null;
            _releasedStorageSlot = null;
        }

        /// <summary>
        /// 偵測當前滑鼠位置底下是否有儲存格
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
    }
}
