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
        // 輸入系統
        private InputManager _input;
        // 移動方向
        private Vector3 MoveAxes => _input.Player.MoveAxes.ReadValue<Vector3>();
        
        // 圖片渲染
        private SpriteRenderer _sprite;
        // 動畫控制
        private Animator _anima;
        // 角色控制
        private CharacterController _cc;
        
        // 動畫哈希值
        private int IsWalkHash => Animator.StringToHash("IsWalk");

        [field: Header("設定"), Tooltip("狀態設定檔"), SerializeField]
        private StateConfigSO stateConfig;

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
            
            _anima.SetBool(IsWalkHash, MoveAxes.x != 0 || MoveAxes.z != 0);
        }
    }
}
