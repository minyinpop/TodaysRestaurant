using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Item.Data
{
    public static class ItemDatabase
    {
        private static readonly Dictionary<int, IItem> _itemDatabase = new();
        
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
            {
                Debug.Log("物品資料庫已初始化。");
                return;
            }
            
            _initialized = true;

            foreach (var item in Resources.LoadAll<ItemSO>("Item"))
            {
                _itemDatabase[item.ItemID] = item;
            }
            
            Debug.Log($"ItemDatabase 初始化完成，數量: {_itemDatabase.Count}");
        }

        public static IItem GetItem(int itemID)
        {
            if (!_initialized)
            {
                throw new InvalidOperationException(nameof(_initialized));
            }

            return _itemDatabase.GetValueOrDefault(itemID);
        }
    }
}