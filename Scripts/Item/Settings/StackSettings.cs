using UnityEngine;

namespace Item.Settings
{
    [System.Serializable]
    internal class StackSettings
    {
        [field: SerializeField] internal bool CanStack { get; private set; }
        [field: SerializeField] internal int MaxStack { get; private set; }
    }
}