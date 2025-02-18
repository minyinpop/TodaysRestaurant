using System.Collections.Generic;
using Item.Interface;
using UnityEngine;

namespace Player
{
    public class PlayerStorageSystem : MonoBehaviour
    {
        [Tooltip("儲物介面的資料庫。")]
        [SerializeField]
        private List<Storage.Root.Backend.ScriptableObject.Storage> storages;

        public void AddItem(ITem item, int quantity)
        {
            foreach (var storage in storages)
            {
                // 判斷被選擇的儲物介面的資料庫，是否添加物品成功。
                if (storage.AddItem(item, quantity))
                {
                    Debug.Log("物品添加成功 !");
                    
                    // TODO: 物品添加完後，要同步更新顯示的儲物格。
                    
                    break;
                }
            }
        }
    }
}