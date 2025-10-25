using System.Collections.Generic;
using Data.Item.Base;
using UnityEngine;

namespace Data.Restaurant.Cook_Menu.Select_Food_Page
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Select Food Page Data", fileName = "New Data")]
    internal sealed class SelectFoodPageSO : ScriptableObject
    {
        public List<ItemSO> ItemsData;

        public void Init(int index)
        {
            for (var i = 0; i < index; i++) ItemsData.Add(null);
        }

        public void Add(int slotIndex, ItemSO targetItemData)
        {
            ItemsData[slotIndex] = targetItemData;
        }
        
        public void Get(out List<ItemSO> targetItemsData)
        {
            targetItemsData = ItemsData;
        }

        public void Remove(ItemSO targetItemData)
        {
            for (var i = 0; i < ItemsData.Count; i++)
            {
                var itemData = ItemsData[i];
                if (itemData != targetItemData) continue;
                ItemsData[i] = null;
                return;
            }
        }
    }
}