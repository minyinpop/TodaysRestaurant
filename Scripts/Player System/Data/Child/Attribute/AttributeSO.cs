using UnityEngine;

namespace Player_System.Data.Child.Attribute
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Attribute Data", fileName = "New Data")]
    internal sealed class AttributeSO : ScriptableObject
    {
        [field: SerializeField] private float MoveSpeed;
        public void GetMoveSpeed(out float moveSpeed) => moveSpeed = MoveSpeed;
    }
}