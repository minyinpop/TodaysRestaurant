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
        // 裝置輸入端
        private InputManager _input;
        // 移動方向軸
        private Vector3 MoveAxes => _input.Player.MoveAxes.ReadValue<Vector3>();
        
        // 自身的圖片渲染器
        private SpriteRenderer _sprite;
        // 自身的動畫控制器
        private Animator _anima;
        // 自身的剛體
        private Rigidbody _rig;

        [field: Header("基礎設定")]
        // 移動速度
        [field: SerializeField] private float moveSpeed = 120.0f;
        
        // 走路的動畫哈希值
        private readonly int isWalk = Animator.StringToHash("IsWalk");

        private void Awake()
        {
            _input = InputSystem.Input;
            
            _sprite = GetComponent<SpriteRenderer>();
            _anima = GetComponent<Animator>();
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
            
            _anima.SetBool(isWalk, MoveAxes.x != 0 || MoveAxes.z != 0);
        }
    }
}