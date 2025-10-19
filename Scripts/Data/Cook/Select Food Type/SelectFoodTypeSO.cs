using System.Collections.Generic;
using Data.Item.Base;
using UnityEngine;

namespace Data.Cook.Select_Food_Type
{
    [CreateAssetMenu(menuName = "Minyinpop/Cook/Select Food Type", fileName = "New Data")]
    internal sealed class SelectFoodTypeSO : ScriptableObject
    {
        private readonly List<ItemSO> ItemsData = new();

        public void Init(int index)
        {
            for (var i = 0; i < index; i++) ItemsData.Add(null);
        }

        public void Add(int slotIndex, ItemSO itemData)
        {
            ItemsData[slotIndex] = itemData;
        }
        
        public void Get(out List<ItemSO> itemsData)
        {
            itemsData = ItemsData;
        }
    }
}