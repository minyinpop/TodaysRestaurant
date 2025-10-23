using Data.Mob.Character.Main;
using UnityEngine;

namespace Data.Mob.Character.Child.Customer.Base
{
    [CreateAssetMenu(menuName = "Minyinpop/Mob/Customer", fileName = "New Data")]
    internal sealed class CustomerSO : CharacterSO
    {
        #region Attribute
            [field: Header("Attribute")]
            [field: SerializeField] private float MoveSpeed;
            public override void GetMoveSpeed(out float moveSpeed) { moveSpeed = MoveSpeed; }
        #endregion
    }
}