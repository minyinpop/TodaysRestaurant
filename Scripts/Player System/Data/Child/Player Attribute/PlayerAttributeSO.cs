using UnityEngine;

namespace Player_System.Data.Child.Player_Attribute
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Attribute", fileName = "New Data")]
    internal sealed class PlayerAttributeSO : ScriptableObject
    {
        [field: Header("Attribute")]
        [field: SerializeField] private float moveSpeed;
                                public float MoveSpeed => moveSpeed;
    }
}