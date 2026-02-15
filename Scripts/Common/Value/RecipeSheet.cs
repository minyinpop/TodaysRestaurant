using System.Collections.Generic;
using Common.Item.Data;
using UnityEngine;

namespace Common.Value
{
    [System.Serializable]
    public sealed class RecipeSheet
    {
        [field: SerializeField] private List<ItemSO> items;
                                public List<ItemSO> Items => items;
    }
}