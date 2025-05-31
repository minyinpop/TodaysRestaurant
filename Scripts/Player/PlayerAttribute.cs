using UnityEngine;

namespace Player
{
    [CreateAssetMenu(menuName = "Minyinpop/Player Attribute", fileName = "New Data", order = 1)]
    public class PlayerAttribute : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; set; }
    }
}