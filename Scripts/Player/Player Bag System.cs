using System.Collections.Generic;
using Storage;
using Storage.Slot;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Player
{
    public class PlayerBagSystem : MonoBehaviour
    {
        // 輸入系統
        private InputManager _input;
        
        // 物品欄介面暫存
        private GameObject _inventoryUI;
        // 背包介面暫存
        private GameObject _bagUI;
        
        // 儲存格暫存
        private readonly List<StorageSlotCore> _storageSlots = new();
        
        [field: Header("資料"), Tooltip("玩家背包的資料庫組件"), SerializeField]
        public StorageDataSO StorageDataSO { get; private set; }
        
        [field: Header("介面"), Tooltip("物品欄介面的預製件"), SerializeField]
        private GameObject inventoryUIPrefab;
        
        [field: Tooltip("背包介面的預製件"), SerializeField]
        private GameObject bagUIPrefab;
        
        [field: Tooltip("畫布的位置組件"), SerializeField]
        private Canvas canvas;

        private void Awake()
        {
            _input = InputSystem.Input;
        }

        private void Start()
        {
            if (inventoryUIPrefab is not null)
            {
                _inventoryUI = Instantiate(inventoryUIPrefab, canvas.transform);
                
                AddInventoryStorageSlot();
                Refresh();
            }
        }

        private void OnEnable()
        {
            _input.Player.OpenBag.started += OnOpenBagButtonPressed;
        }

        private void OnDisable()
        {
            _input.Player.OpenBag.started -= OnOpenBagButtonPressed;
        }

        /// <summary>
        /// 背包互動鍵
        /// </summary>
        /// <param name="context"></param>
        private void OnOpenBagButtonPressed(InputAction.CallbackContext context)
        {
            if (_bagUI is null)
            {
                _bagUI = Instantiate(bagUIPrefab, canvas.transform);
                
                AddBagStorageSlot();
                Refresh();
            }
            else
            {
                RemoveBagStorageSlot();
                Destroy(_bagUI);
                
                Refresh();
                
                _bagUI = null;
            }
        }

        /// <summary>
        /// 更新背包
        /// </summary>
        private void Refresh()
        {
            for (var i = 0; i < _storageSlots.Count; i++)
                _storageSlots[i].Refresh(StorageDataSO.storageSlotInfos[i]);
        }
        
        /// <summary>
        /// 新增物品欄儲物格到暫存
        /// </summary>
        private void AddInventoryStorageSlot()
        {
            if (_inventoryUI is null)
                return;

            foreach (var storageSlot in _inventoryUI.GetComponentsInChildren<StorageSlotCore>())
            {
                if (_storageSlots.Contains(storageSlot))
                    continue;
                
                _storageSlots.Add(storageSlot);
            }
        }

        /// <summary>
        /// 從暫存中移除物品欄儲物格
        /// </summary>
        private void RemoveInventoryStorageSlot()
        {
            if (_inventoryUI is null)
                return;

            foreach (var storageSlot in _inventoryUI.GetComponentsInChildren<StorageSlotCore>())
            {
                if (_storageSlots.Contains(storageSlot))
                    _storageSlots.Remove(storageSlot);
            }
        }

        /// <summary>
        /// 新增背包儲物格到暫存
        /// </summary>
        private void AddBagStorageSlot()
        {
            if (_bagUI is null)
                return;

            foreach (var storageSlot in _bagUI.GetComponentsInChildren<StorageSlotCore>())
            {
                if (_storageSlots.Contains(storageSlot))
                    continue;
                
                _storageSlots.Add(storageSlot);
            }
        }

        /// <summary>
        /// 從暫存中移除背包儲物格
        /// </summary>
        private void RemoveBagStorageSlot()
        {
            if (_bagUI is null)
                return;
            
            foreach (var storageSlot in _bagUI.GetComponentsInChildren<StorageSlotCore>())
            {
                if (_storageSlots.Contains(storageSlot))
                    _storageSlots.Remove(storageSlot);
            }
        }
    }
}
