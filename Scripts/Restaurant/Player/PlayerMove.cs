using Database.Player.Attribute;
using Input;
using UnityEngine;

namespace Restaurant.Player
{
    // ==================================================
    // 用來控制玩家移動的程式碼。
    // 使用物理來移動。
    // ==================================================
    
    [RequireComponent(typeof(PlayerManager))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMove : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        [field: Header("玩家屬性資料"), SerializeField]
        public PlayerAttributeSO PlayerAttribute { get; private set; }
        
        
        
        // ========== { 組件相關 } ==========
        
        // 自身的 Rigidbody 組件。
        private Rigidbody Rig { get; set; }



        private void Awake()
        {
            Rig = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (PlayerAttribute.Walk.Can)
            {
                PlayerAttribute.Walk = new CanThenRunning
                {
                    Can = true,
                    IsRunning = InputSystem.IsWalkButtonPressed()
                };
            }
            else
            {
                PlayerAttribute.Walk = new CanThenRunning
                {
                    Can = false,
                    IsRunning = false
                };
            }
            
            if (PlayerAttribute.Run.Can)
            {
                PlayerAttribute.Run = new CanThenRunning
                {
                    Can = true,
                    IsRunning = InputSystem.IsRunButtonPressed()
                };
            }
            else
            {
                PlayerAttribute.Run = new CanThenRunning
                {
                    Can = false,
                    IsRunning = false
                };
            }
        }
        
        
        
        private void FixedUpdate()
        {
            var targetMoveSpeed = PlayerAttribute.Run.IsRunning
                ? PlayerAttribute.BasicMoveSpeed * PlayerAttribute.RunSpeedMultiplier
                : PlayerAttribute.BasicMoveSpeed;

            var x = PlayerAttribute.Walk.IsRunning ? InputSystem.MoveDirection().x * targetMoveSpeed * Time.fixedDeltaTime : 0;
            var z = PlayerAttribute.Walk.IsRunning ? InputSystem.MoveDirection().z * targetMoveSpeed * Time.fixedDeltaTime : 0;
            var newDir = new Vector3(x, Rig.linearVelocity.y, z);

            Rig.linearVelocity = newDir;
        }
    }
}