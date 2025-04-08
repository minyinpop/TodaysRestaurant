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
            if (PlayerAttribute.Move.Can)
            {
                if (InputSystem.IsWalkButtonPressed())
                {
                    PlayerAttribute.Move = new CanThenRunning
                    {
                        Can = true,
                        IsRunning = true
                    };

                    if (!PlayerAttribute.Run.Can)
                        return;

                    if (InputSystem.IsRunButtonPressed())
                    {
                        PlayerAttribute.Run = new CanThenRunning
                        {
                            Can = true,
                            IsRunning = true
                        };
                    }
                    else
                    {
                        PlayerAttribute.Run = new CanThenRunning
                        {
                            Can = true,
                            IsRunning = false
                        };
                    }
                }
                else
                {
                    PlayerAttribute.Move = new CanThenRunning
                    {
                        Can = true,
                        IsRunning = false
                    };

                    if (PlayerAttribute.Run.IsRunning)
                    {
                        PlayerAttribute.Run = new CanThenRunning
                        {
                            Can = true,
                            IsRunning = false
                        };
                    }
                }
            }
            else
            {
                PlayerAttribute.Move = new CanThenRunning
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

            var x = PlayerAttribute.Move.IsRunning ? InputSystem.MoveDirection().x * targetMoveSpeed * Time.fixedDeltaTime : 0;
            var z = PlayerAttribute.Move.IsRunning ? InputSystem.MoveDirection().z * targetMoveSpeed * Time.fixedDeltaTime : 0;
            var newDir = new Vector3(x, Rig.linearVelocity.y, z);

            Rig.linearVelocity = newDir;
        }
    }
}