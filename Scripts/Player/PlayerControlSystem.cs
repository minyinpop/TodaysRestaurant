using Input;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 用來控制玩家小人物操作的類
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerControlSystem : MonoBehaviour
    {
        // 玩家的 Rigidbody 組件，用來控制玩家的物理移動。
        private Rigidbody _rig;
        
        // 玩家的 CapsuleCollider 組件，用來控制玩家的碰撞範圍。
        private CapsuleCollider _capsule;

        private void Awake()
        {
            _rig = GetComponent<Rigidbody>();
            _capsule = GetComponent<CapsuleCollider>();
        }

        private void FixedUpdate()
        {
            print(InputSystem.PlayerMoveDirection());
            
            // TODO: 玩家移動的程式碼。
        }
    }
}
