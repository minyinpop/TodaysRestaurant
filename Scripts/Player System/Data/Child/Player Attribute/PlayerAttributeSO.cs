using UnityEngine;

namespace Player_System.Data.Child.Player_Attribute
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Attribute", fileName = "New Data")]
    internal sealed class PlayerAttributeSO : ScriptableObject
    {
        [field: SerializeField] private float MoveSpeed;
        public void GetMoveSpeed(out float moveSpeed) => moveSpeed = MoveSpeed;
    }
}