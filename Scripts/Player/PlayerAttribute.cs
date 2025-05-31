using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Player Attribute", fileName = "New Data", order = 1)]
    internal class PlayerAttribute : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; set; }
    }
}