using UnityEngine;

namespace Item.Base
{
    [System.Serializable]
    internal class InfoSettings
    {
        [field: SerializeField] internal string Name { get; private set; }
        [field: SerializeField] internal Sprite Sprite { get; private set; }
    }
}