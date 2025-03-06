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
        [Header("拖曳預覽相關"), Tooltip("用來顯示物品拖曳時的預覽，用於 UI。"), SerializeField]
        private GameObject itemDragPreviewPrefab;

        [Tooltip("用於檢測滑鼠要在哪一個 Canvas 上做偵測。"), SerializeField]
        private GraphicRaycaster raycaster;
        
        [Tooltip("儲物格拖曳預覽要生成在哪一個遊戲物件的底下。"), SerializeField]
        private Transform previewSpawnPoint;
        
        [Tooltip("儲物格的標籤，用於判斷滑鼠有沒有點擊到儲物格。"), SerializeField]
        private string storageSlotTag;
        
        // 物品預覽儲物格的遊戲物件。
        private GameObject _tempItemPreview;
        // 物品預覽儲物格的資料庫。
        private StorageSlotData _tempItemPreviewSlotData;
        
        // 輸入端。
        private InputMap _input;
        // 滑鼠位置。
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
            if (_tempItemPreview is not null)
                _tempItemPreview.transform.position = MousePos;
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
            // 判斷玩家有沒有物品正在被拖曳。
            if (_tempItemPreview is null)
            {
                _tempItemPreview = Instantiate(itemDragPreviewPrefab, previewSpawnPoint);
                _tempItemPreviewSlotData = _tempItemPreview.GetComponent<StorageSlotData>();
                
                // TODO: 做後續的判斷 ......
            }
            else
            {
                var clickedSlot = MouseDetectedSlot();

                // 判斷玩家有沒有點擊到儲物格，並做後續的判斷。
                if (clickedSlot is null)
                {
                    // TODO: 可以做後續的判斷，像是在非 UI 的區域點擊，可以把物品給丟出去。
                    return;
                }

                // TODO: 判斷 clickedSlot 裡有沒有物品，而做後續的判斷。
                _tempItemPreview = null;
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
    }
}
