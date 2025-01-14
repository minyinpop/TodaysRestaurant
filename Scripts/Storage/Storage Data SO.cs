using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Storage
{
    [CreateAssetMenu(menuName = "Minyinpop/Storage Data", fileName = "New Storage Data", order = 2)]
    public class StorageDataSO : ScriptableObject
    {
        [field: Header(""), Tooltip("")]
        public List<ItemCore> Items { get; }
    }
}
