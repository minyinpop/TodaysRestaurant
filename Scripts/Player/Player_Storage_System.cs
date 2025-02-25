using System.Collections.Generic;
using Storage.Root.Backend.Struct;
using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Input.InputSystem;

namespace Player
{
    public class PlayerStorageSystem : MonoBehaviour
    {
        // 輸入端的程式碼。
        private Input_Manager _input;

        [Header("介面")]
        [Tooltip("物品欄的儲物介面的預製件。")]
        [SerializeField]
        private GameObject inventoryUI;
        // 物品欄的儲物介面的暫存。
        private GameObject _tempInventoryUI;
        
        [Tooltip("背包的儲物介面的預製件。")]
        [SerializeField]
        private GameObject bagUI;
        // 背包的儲物介面的暫存。
        private GameObject _tempBagUI;

        [Tooltip("遊戲介面的生成位置。")]
        [SerializeField]
        private RectTransform UISpawnPoint;
        
        // 儲物介面的遊戲物件。
        private List<Storage.Root.Frontend.Storage> _storageUIList = new();
        
        [Header("資料庫")]
        [Tooltip("儲物介面的資料庫。")]
        [SerializeField]
        private List<Storage.Root.Backend.ScriptableObject.Storage> storageDataList;

        private void Awake()
        {
            _input = InputSystem.input;
            
            _tempInventoryUI = Instantiate(inventoryUI);
        }

        private void OnEnable()
        {
            _input.Player.Bag.performed += OpenBag;
        }

        private void OnDisable()
        {
            _input.Player.Bag.performed -= OpenBag;
        }

        private void OpenBag(InputAction.CallbackContext context)
        {
        }

        /// <summary>
        /// 添加物品到玩家的背包。
        /// </summary>
        /// <param name="newData"> 被添加的物品的資料。 </param>
        public void AddItem(StorageSlotData newData)
        {
            var data = newData;
            
            for (var i = 0; i < storageDataList.Count; i++)
            {
                var remainingQuantity = storageDataList[i].AddItem(data);

                data = new StorageSlotData
                {
                    Locked = false,
                    Item = data.Item,
                    Quantity = remainingQuantity
                };
                
                // 表示物品添加完畢。
                if (remainingQuantity == 0)
                {
                    Debug.Log("物品添加完畢。");
                    return;
                }
                // 表示第 i 個資料庫的儲物格皆跑過一次，但無法添加完該物品。
                if (remainingQuantity > 0)
                    Debug.Log("物品添加不完，切換成下一個資料庫做添加。");
                // 表示某段程式碼發生錯誤。
                if (remainingQuantity < 0)
                    Debug.LogWarning("物品數量不能為負數，此情況可能發生錯誤 !");
            }
        }
    }
}