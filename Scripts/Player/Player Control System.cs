using Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerControlSystem : MonoBehaviour
    {
        private InputManager _input;
        private Vector3 MoveAxes => _input.Player.MoveAxes.ReadValue<Vector3>();

        private SpriteRenderer _sprite;
        private Rigidbody _rig;

        [field: Header("基礎設定")]
        [field: SerializeField] private float moveSpeed = 120.0f; 

        private void Awake()
        {
            _input = InputSystem.Input;
            
            _sprite = GetComponent<SpriteRenderer>();
            _rig = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var x = MoveAxes.x * moveSpeed * Time.fixedDeltaTime;
            var y = _rig.linearVelocity.y;
            var z = MoveAxes.z * moveSpeed * Time.fixedDeltaTime;
            
            _rig.linearVelocity = new Vector3(x, y, z);
        }

        private void Update()
        {
            _sprite.flipX = MoveAxes.x switch
            {
                > 0 => true,
                < 0 => false,
                _ => _sprite.flipX
            };
        }
    }
}