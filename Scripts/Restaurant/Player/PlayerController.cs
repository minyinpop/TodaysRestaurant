using Database.Restaurant.Player.Attribute;
using Input;
using UnityEngine;

namespace Restaurant.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [field: Header("玩家的屬性資料")]
        [field: SerializeField] private PlayerAttributeSO PlayerAttribute { get; set; }

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
            var x = MoveDir.x * PlayerAttribute.MoveAttribute.MoveSpeed * Time.fixedDeltaTime;
            var y = Rig.linearVelocity.y;
            var z = MoveDir.z * PlayerAttribute.MoveAttribute.MoveSpeed * Time.fixedDeltaTime;
            
            Rig.linearVelocity = new Vector3(x, y, z);
        }
    }
}