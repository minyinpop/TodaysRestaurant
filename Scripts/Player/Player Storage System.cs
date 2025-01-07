using System.Collections.Generic;
using Grid;
using Item;
using UnityEngine;

namespace Player
{
    public class PlayerStorageSystem : MonoBehaviour
    {
        // 總物品格
        [field: SerializeField] private List<GridCore> totalGrids;

        public void IncreaseItem(ITem itemData)
        {
            foreach (var grid in totalGrids)
            {
                if (grid.GridInfo.ItemData is null)
                {
                    // TODO: 呼叫格子裡的添加物品方法
                }
            }
        }
    }
}
