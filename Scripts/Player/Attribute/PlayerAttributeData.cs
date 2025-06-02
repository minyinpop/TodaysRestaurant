using UnityEngine;

namespace Player.Attribute
{
    [CreateAssetMenu(menuName = "Today's Restaurant/Player/Player Attribute Data", fileName = "New Data", order = 1)]
    internal class PlayerAttributeData : ScriptableObject
    {
        [field: SerializeField] internal float MoveSpeed { get; set; }
    }
}