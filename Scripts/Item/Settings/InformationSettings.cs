using UnityEngine;

namespace Item.Settings
{
    [System.Serializable]
    internal class InformationSettings
    {
        [field: SerializeField] internal string Name { get; private set; }
        [field: SerializeField] internal Sprite Sprite { get; private set; }
    }
}