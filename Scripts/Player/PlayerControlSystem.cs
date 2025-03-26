using DataBase.Character.Attribute;
using Input;
using Spine.Unity;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// 用來控制玩家小人物操作的類
    /// </summary>
    [RequireComponent(typeof(SkeletonAnimation))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class PlayerControlSystem : MonoBehaviour
    {
        // 玩家的 SkeletonAnimation 組件，用來控制玩家的動畫。
        private SkeletonAnimation _skeletonAnimation;
        
        // 玩家的 Rigidbody 組件，用來控制玩家的物理移動。
        private Rigidbody _rig;
        
        // 玩家的 CapsuleCollider 組件，用來控制玩家的碰撞範圍。
        private BoxCollider _bc;

        [Header("資料"), Tooltip("玩家的屬性資料，用來讀取移動速度等等。"), SerializeField]
        private CharacterAttributeData attributeData;

        private void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _rig = GetComponent<Rigidbody>();
            _bc = GetComponent<BoxCollider>();
        }

        private void FixedUpdate()
        {
            var x = InputSystem.PlayerMoveDirection().x * attributeData.MoveSpeed * Time.fixedDeltaTime;
            var z = InputSystem.PlayerMoveDirection().z * attributeData.MoveSpeed * Time.fixedDeltaTime;
            _rig.linearVelocity = new Vector3(x, _rig.linearVelocity.y, z);
        }

        private void Update()
        {
            _skeletonAnimation.Skeleton.ScaleX = InputSystem.PlayerMoveDirection().x switch
            {
                > 0 => -1,
                < 0 => 1,
                _ => _skeletonAnimation.Skeleton.ScaleX
            };
        }
    }
}
