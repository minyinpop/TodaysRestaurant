using System.Collections.Generic;
using Storage.Slot.Abstract.Base;
using UnityEngine;

namespace Storage.Root.Frontend
{
    public class Storage : MonoBehaviour
    {
        [Tooltip("儲物介面的資料庫。")]
        [SerializeField]
        private Backend.ScriptableObject.Storage data;
        
        [Tooltip("儲物介面的所有儲物格，\n先後會影響儲物順序。")]
        [SerializeField]
        private List<StorageSlot> slots;
    }
}