using Input;
using State;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerControlSystem : MonoBehaviour
    {
        private InputManager _input;
        private Vector3 MoveAxes => _input.Player.MoveAxes.ReadValue<Vector3>();

        private SpriteRenderer _sprite;
        private Animator _anima;
        private CharacterController _cc;
        
        private readonly int _isWalkHash = Animator.StringToHash("IsWalk");

        [field: Tooltip("參數設定"), SerializeField]
        private StateConfig stateConfig;

        private void Awake()
        {
            _input = InputSystem.Input;
            
            _sprite = GetComponent<SpriteRenderer>();
            _anima = GetComponent<Animator>();
            _cc = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            var x = MoveAxes.x * stateConfig.MoveSpeed;
            var y = _cc.velocity.y;
            var z = MoveAxes.z * stateConfig.MoveSpeed;
            
            _cc.SimpleMove(new Vector3(x, y, z) * Time.fixedDeltaTime);
        }

        private void Update()
        {
            _sprite.flipX = MoveAxes.x switch
            {
                > 0 => true,
                < 0 => false,
                _ => _sprite.flipX
            };
            
            _anima.SetBool(_isWalkHash, MoveAxes.x != 0 || MoveAxes.z != 0);
        }
    }
}
