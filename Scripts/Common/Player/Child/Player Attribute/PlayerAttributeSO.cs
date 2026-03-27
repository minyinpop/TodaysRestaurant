using UnityEngine;

namespace Common.Player.Child.Player_Attribute
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Attribute", fileName = "New Data")]
    internal sealed class PlayerAttributeSO : ScriptableObject
    {
        [field: Header("Attribute")]
        [field: SerializeField] private float moveSpeed;
                                public float MoveSpeed => moveSpeed;
    }
}