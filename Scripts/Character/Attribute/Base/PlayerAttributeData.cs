using Character.Attribute.Settings;
using UnityEngine;

namespace Character.Attribute.Base
{
    [CreateAssetMenu(menuName = "Today's Restaurant/Player/Player Attribute Data", fileName = "New Data",  order = 1)]
    internal class PlayerAttributeData : ScriptableObject
    {
        [field: Header("移動設定")]
        [field: SerializeField] internal MoveSettings MoveSettings { get; private set; }
    }
}