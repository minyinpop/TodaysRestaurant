using System.Collections.Generic;
using Data.Item.Base;
using UnityEngine;

namespace Data.Cook.Select_Food_Type
{
    [CreateAssetMenu(menuName = "Minyinpop/Cook/Select Food Type", fileName = "New Data")]
    internal sealed class SelectFoodTypeSO : ScriptableObject
    {
        private readonly List<ItemSO> ItemsData = new();

        public void Set(List<ItemSO> itemsData)
        {
            ItemsData.Clear();
            ItemsData.AddRange(itemsData);
        }
    }
}