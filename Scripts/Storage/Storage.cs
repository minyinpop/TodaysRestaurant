using System.Collections.Generic;
using UnityEngine;

namespace Storage
{
    public class Storage : MonoBehaviour
    {
        [field: Tooltip("儲物介面裡的儲物格。"), SerializeField]
        private List<StorageSlot> slots;
    }
}
