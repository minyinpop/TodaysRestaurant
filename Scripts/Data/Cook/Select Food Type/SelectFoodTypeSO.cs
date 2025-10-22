using System.Collections.Generic;
using Data.Item.Base;
using UnityEngine;

namespace Data.Cook.Select_Food_Type
{
    [CreateAssetMenu(menuName = "Minyinpop/Cook/Select Food Type", fileName = "New Data")]
    internal sealed class SelectFoodTypeSO : ScriptableObject
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