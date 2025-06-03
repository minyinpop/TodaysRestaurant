using UnityEngine;

namespace Item.Base
{
    [System.Serializable]
    internal class StackSettings
    {
        [field: SerializeField] internal bool CanStack { get; private set; }
        [field: Range(1, 99), SerializeField] internal int MaxStack { get; private set; }
    }
}