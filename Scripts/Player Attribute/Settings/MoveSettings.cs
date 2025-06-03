using UnityEngine;

namespace Player_Attribute.Settings
{
    [System.Serializable]
    internal class MoveSettings
    {
        [field: SerializeField] internal float MoveSpeed { get; set; }
    }
}