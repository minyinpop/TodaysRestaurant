using System.Collections.Generic;
using Storage.Slot.Data;
using UnityEngine;

namespace Storage.Base
{
    [CreateAssetMenu(menuName = "Today's Restaurant/Storage Data", fileName = "New Data", order = 1)]
    internal class StorageData : ScriptableObject
    {
        [field: SerializeField] internal List<StorageSlotData> SlotsData { get; set; }
    }
}