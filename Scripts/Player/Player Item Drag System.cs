using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using InputSystem = Input.InputSystem;

namespace Player
{
    public class PlayerItemDragSystem : MonoBehaviour
    {
        // 裝置輸入端
        private InputManager _input;
        // 滑鼠位置
        private Vector2 MousePos => _input.Mouse.Position.ReadValue<Vector2>();
        
        [field: Header("基礎組件")]
        // 第一層畫布
        [field: SerializeField] private Canvas firstCanvas;
        // 第二層畫布
        [field: SerializeField] private Canvas secondCanvas;
        
        // 第一層畫布的圖片雷射偵測
        private GraphicRaycaster _raycaster;
        
        [field: Header("物品拖曳預覽")]
        // 物品拖曳預覽的預製件
        [field: SerializeField] private GameObject itemDragPreviewPrefab;
        // 物品拖曳預覽的偏移
        [field: SerializeField] private Vector2 itemDragPreviewOffset;

        // 滑鼠左鍵點擊時所在的格子
        private GridCore clickedGrid;
        // 滑鼠左鍵釋放時所在的格子
        private GridCore releasedGrid;
        // 物品拖曳的格子
        private GameObject _itemDragPreview;
        
        [field: Header("標籤設定")]
        // 物品格子的標籤
        [field: SerializeField] private string itemGridTag = "Item Grid"; 

        private void Awake()
        {
            _input = InputSystem.Input;
        }

        private void OnEnable()
        {
            _input.Mouse.LeftClick.started += OnMouseDown;
            _input.Mouse.LeftClick.canceled += OnMouseUp;
        }

        private void LateUpdate()
        {
            if (_itemDragPreview is not null)
                _itemDragPreview.transform.position = MousePos + itemDragPreviewOffset;
        }

        private void OnDisable()
        {
            _input.Mouse.LeftClick.started -= OnMouseDown;
            _input.Mouse.LeftClick.canceled -= OnMouseUp;
        }

        /// <summary>
        /// 當滑鼠左鍵按下時所發生的事情
        /// </summary>
        /// <param name="context"></param>
        private void OnMouseDown(InputAction.CallbackContext context)
        {
            clickedGrid = GridDetect();

            if (clickedGrid?.GridInfo.ItemData is not null)
            {
                _itemDragPreview = Instantiate(itemDragPreviewPrefab, secondCanvas.transform);
                _itemDragPreview.transform.GetChild(0).GetComponent<Image>().sprite =
                    clickedGrid.GridInfo.ItemData.Sprite;
            }
        }
        
        // TODO: 繼續製作有關 Item Drag Preview 的部分

        /// <summary>
        /// 當滑鼠左鍵放開時所發生的事情
        /// </summary>
        /// <param name="context"></param>
        private void OnMouseUp(InputAction.CallbackContext context)
        {
            releasedGrid = GridDetect();

            if (_itemDragPreview is not null)
            {
                Destroy(_itemDragPreview);
                _itemDragPreview = null;
            }
            
            clickedGrid = null;
            releasedGrid = null;
        }

        /// <summary>
        /// 偵測是否有格子在滑鼠當前的位置
        /// </summary>
        /// <returns></returns>
        private GridCore GridDetect()
        {
            var pointer = new PointerEventData(EventSystem.current)
            {
                position = MousePos
            };
            var results = new List<RaycastResult>();
            
            _raycaster.Raycast(pointer, results);

            foreach (var result in results)
            {
                if (result.gameObject.layer != LayerMask.NameToLayer("UI"))
                    continue;

                if (!result.gameObject.CompareTag(itemGridTag))
                    continue;
                
                return result.gameObject.GetComponent<GridCore>();
            }
            
            return null;
        }
    }
}