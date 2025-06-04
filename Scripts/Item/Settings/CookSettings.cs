using UnityEngine;

namespace Item.Settings
{
    [System.Serializable]
    internal class CookSettings
    {
        [field: SerializeField] internal bool CanCook { get; private set; }
        [field: SerializeField] internal float CookTime { get; private set; }
    }
}