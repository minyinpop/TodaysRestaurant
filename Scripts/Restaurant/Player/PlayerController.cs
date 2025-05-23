using Database.Restaurant.Player.Attribute;
using Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Restaurant.Player
{
    [RequireComponent(typeof(Rigidbody))]
    internal class PlayerController : MonoBehaviour
    {
        [field: FormerlySerializedAs("<PlayerAttribute>k__BackingField")]
        [field: Header("玩家的屬性資料")]
        [field: SerializeField] private AttributeSO Attribute { get; set; }

        private InputManager Input { get; set; }
        private Vector3 MoveDir => Input.Player.Walk.ReadValue<Vector3>();
        
        private Rigidbody Rig { get; set; }

        private void Awake()
        {
            Input = InputSystem.Input;
            
            Rig = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!Attribute.MoveAttribute.CanMove)
            {
                Rig.linearVelocity = Vector3.zero;
                return;
            }
            
            var x = MoveDir.x * Attribute.MoveAttribute.MoveSpeed * Time.fixedDeltaTime;
            var y = Rig.linearVelocity.y;
            var z = MoveDir.z * Attribute.MoveAttribute.MoveSpeed * Time.fixedDeltaTime;
            
            Rig.linearVelocity = transform.TransformDirection(new Vector3(x, y, z));
        }
    }
}