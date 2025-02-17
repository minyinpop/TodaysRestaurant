using System.Collections.Generic;
using Storage.Root.Backend.Struct;
using UnityEngine;

namespace Storage.Root.Backend.ScriptableObject
{
    [CreateAssetMenu(menuName = "Minyinpop/Storage", fileName = "New Storage", order = 1)]
    public class Storage : UnityEngine.ScriptableObject
    {
        [Tooltip("儲物格的資料，\n順序會影響到儲物格的顯示。\n有多少儲物格就要創建多少筆資料。")]
        public List<StorageSlotData> data;
    }
}