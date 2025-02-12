using System.Collections.Generic;
using Storage.Data.Core;
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

        private GameObject _itemDragPreview;
        private StorageSlotCore _previewStorageSlotCore;
        private StorageSlotData _previewStorageSlotData;

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
            _input.Mouse.RightButton.started += OnRightButtonPressed;
        }

        private void Update()
        {
            if (_itemDragPreview is not null)
                _itemDragPreview.transform.position = MousePos + previewOffset;
        }

        private void OnDisable()
        {
            _input.Mouse.LeftButton.started -= OnLeftButtonPressed;
            _input.Mouse.RightButton.started -= OnRightButtonPressed;
        }

        /// <summary>
        /// 當 " Left Button " 按鈕按下時
        /// </summary>
        /// <param name="context"></param>
        private void OnLeftButtonPressed(InputAction.CallbackContext context)
        {
            var tempClickedStorageSlot = StorageSlotDetect();
            
            if (tempClickedStorageSlot is null)
                return;
            
            var tempClickedStorageSlotData = tempClickedStorageSlot.StorageSlotData;

            if (_itemDragPreview is null)
            {
                if (tempClickedStorageSlotData.item is null)
                    return;

                _itemDragPreview = Instantiate(previewPrefab, previewSpawnPoint);
                _previewStorageSlotCore = _itemDragPreview.GetComponent<StorageSlotCore>();
                
                _previewStorageSlotCore.SetItem(tempClickedStorageSlotData);
                tempClickedStorageSlot.Reset();
            }
            else
            {
                if (tempClickedStorageSlot.StorageSlotData.item is null)
                {
                    tempClickedStorageSlot.SetItem(_previewStorageSlotData);
                    
                    Destroy(_itemDragPreview);
                    _itemDragPreview = null;
                    
                    return;
                }
                
                if (tempClickedStorageSlot.CheckSlotStackable(_previewStorageSlotData))
                {
                    if (tempClickedStorageSlotData.itemAmount +
                        _previewStorageSlotData.itemAmount >=
                        tempClickedStorageSlotData.item.MaxStack)
                    {
                        var remainingItemSpace = _previewStorageSlotData.itemAmount -
                                                 (tempClickedStorageSlotData.item.MaxStack -
                                                  tempClickedStorageSlotData.itemAmount);
                        
                        tempClickedStorageSlot.SetItem(new StorageSlotData
                        {
                            @lock = tempClickedStorageSlotData.@lock,
                            item = _previewStorageSlotData.item,
                            itemAmount = tempClickedStorageSlotData.item.MaxStack
                        });
                        
                        _previewStorageSlotCore.SetItem(new StorageSlotData
                        {
                            @lock = tempClickedStorageSlotData.@lock,
                            item = _previewStorageSlotData.item,
                            itemAmount = remainingItemSpace
                        });
                    }
                    else
                    {
                        // TODO: 如果物品可以直接堆疊並且不會剩餘
                        
                        var newClickedStorageSlotData = new StorageSlotData
                        {
                            @lock = tempClickedStorageSlotData.@lock,
                            item = tempClickedStorageSlotData.item,
                            itemAmount = tempClickedStorageSlotData.itemAmount + _previewStorageSlotData.itemAmount
                        };
                        
                        tempClickedStorageSlot.SetItem(newClickedStorageSlotData);
                        
                        Destroy(_itemDragPreview);
                        _itemDragPreview = null;
                    }
                }
            }
        }

        /// <summary>
        ///  當 " Right Button " 按鈕按下時
        /// </summary>
        /// <param name="context"></param>
        private void OnRightButtonPressed(InputAction.CallbackContext context)
        {
            var tempReleasedStorageSlot = StorageSlotDetect();
            var tempPreviewStorageSlot = _itemDragPreview.GetComponent<StorageSlotCore>();

            if (tempReleasedStorageSlot is null)
                return;

            if (_itemDragPreview is null)
                return;
            
            tempReleasedStorageSlot.AddOneItem(tempPreviewStorageSlot.StorageSlotData);
            tempPreviewStorageSlot.RemoveOneItem();

            if (tempPreviewStorageSlot.StorageSlotData.itemAmount > 0)
                return;
            
            Destroy(_itemDragPreview);
            _itemDragPreview = null;
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
