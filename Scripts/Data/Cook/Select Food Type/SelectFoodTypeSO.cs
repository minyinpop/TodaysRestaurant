using System.Collections.Generic;
using Data.Item.Base;
using UnityEngine;

namespace Data.Cook.Select_Food_Type
{
    [CreateAssetMenu(menuName = "Minyinpop/Cook/Select Food Type", fileName = "New Data")]
    internal sealed class SelectFoodTypeSO : ScriptableObject
    {
        public List<ItemSO> ItemsData = new();

        public void Init(int dataCount)
        {
            dataCount = Mathf.Abs(dataCount);
            for (var i = 0; i < dataCount; i++) ItemsData.Add(null);
        }

        public void Add(ItemSO itemData)
        {
            if (itemData is null) return;
            for (var i = 0; i < ItemsData.Count; i++)
            {
                var data = ItemsData[i];
                if (data is not null) continue;
                ItemsData[i] = itemData;
            }
        }

        public void Clear()
        {
            ItemsData.Clear();
        }
    }
}