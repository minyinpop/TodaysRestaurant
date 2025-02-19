using Storage.Root.Backend.Struct;
using UnityEngine;

namespace Storage.Slot.Abstract.Base
{
    public abstract class StorageSlot : MonoBehaviour
    {
        public virtual void Refresh(StorageSlotData newData)
        {
        }
    }
}