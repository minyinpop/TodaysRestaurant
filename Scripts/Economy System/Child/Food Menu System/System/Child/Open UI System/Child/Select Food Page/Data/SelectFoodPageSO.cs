using System.Linq;
using Item;
using UnityEngine;

namespace Economy_System.Child.Food_Menu_System.System.Child.Open_UI_System.Child.Select_Food_Page.Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Select Food Page Data", fileName = "New Data")]
    internal sealed class SelectFoodPageSO : ScriptableObject
    {
        private ITem[] ItemsData;

        public void Init(int index)
        {
            ItemsData = new ITem[index];
        }

        public void AddItemData(int slotIndex, ITem targetItemData)
        {
            ItemsData[slotIndex] = targetItemData;
        }
        
        #region Get Item Data
            public void GetAllItemData(out ITem[] itemsData)
            {
                itemsData = ItemsData;
            }

            public void GetRandomItemData(out ITem itemData)
            {
                var nonNullItemsData = ItemsData.Where(item => item is not null).ToArray();
                itemData = nonNullItemsData[Random.Range(0, nonNullItemsData.Length)];
            }
            
            public void GetRandomItemData(ITem[] excludeItemsData, out ITem itemData)
            {
                var nonNullItemsData = ItemsData.Where(item => item is not null).ToArray();
                var excludeItemsDataList = excludeItemsData.ToList();
                var filteredItemsData = nonNullItemsData.Where(item => !excludeItemsDataList.Contains(item)).ToArray();
                itemData = filteredItemsData.Length > 0 ? filteredItemsData[Random.Range(0, filteredItemsData.Length)] : null;
            }
        #endregion

        public void RemoveItemData(ITem itemData)
        {
            for (var i = 0; i < ItemsData.Length; i++)
            {
                var currentItemData = ItemsData[i];
                if (currentItemData != itemData) continue;
                ItemsData[i] = null;
                return;
            }
        }
    }
}