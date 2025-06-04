using UnityEngine;

namespace Character.Attribute.Settings
{
    [System.Serializable]
    internal class MoveSettings
    {
        [field: SerializeField] internal bool CanMove { get; set; }
        [field: SerializeField] internal float MoveSpeed { get; private set; }
    }
}