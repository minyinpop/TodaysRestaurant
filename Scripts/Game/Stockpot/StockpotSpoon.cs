using UnityEngine;

namespace Game.Stockpot
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class StockpotSpoon : MonoBehaviour
    {
        [Header("組件"), Tooltip("深煮鍋小遊戲的管理器的類。"), SerializeField]
        private StockpotManager stockpotManager;
        
        [Tooltip("- 湯杓的重生位置。\n- 用於重新歸位的使用。"), SerializeField]
        private Transform spawnPoint;

        [Header("標籤"), Tooltip("- 用於判斷湯匙是否在液體中。\n- 在液體中的湯勺會因為阻力還變慢。"), SerializeField]
        private string liquidAreaTag;

        [Header("阻力設定"), Tooltip("- 在空氣中，湯勺移動的阻力。\n- 預設為 0。"), SerializeField]
        private float normalLinearDamping = 0f;
        
        [Tooltip("- 在空氣中，湯勺的旋轉阻力。\n- 預設為 0.05。"), SerializeField]
        private float normalAngularDamping = .05f;

        [Tooltip("- 在液體中，湯勺的移動阻力。\n- 預設為 5。"), SerializeField]
        private float liquidLinearDamping = 5f;
        
        [Tooltip("- 在液體中，湯勺的旋轉阻力。\n- 預設為 3。"), SerializeField]
        private float liquidAngularDamping = 3f;

        [Tooltip("- 物理效果切換速度。\n- 預設為 5。"), SerializeField]
        private float dampingChangeSpeed;

        // 自身的 Rigidbody2D 組件，用於模擬重力使用。
        private Rigidbody2D _rig2D;
        
        // 用來判斷湯勺目前是否在液體裡面，用於模擬液體的物理阻力。
        private bool _inLiquidArea;
        
        private void Awake()
        {
            _rig2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var targetLinearDamping = _inLiquidArea ? liquidLinearDamping : normalLinearDamping;
            var targetAngularDamping = _inLiquidArea ? liquidAngularDamping : normalAngularDamping;

            _rig2D.linearDamping = Mathf.MoveTowards(_rig2D.linearDamping, targetLinearDamping, dampingChangeSpeed * Time.fixedDeltaTime);
            _rig2D.angularDamping = Mathf.MoveTowards(_rig2D.angularDamping, targetAngularDamping, dampingChangeSpeed * Time.fixedDeltaTime);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(liquidAreaTag))
                _inLiquidArea = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(liquidAreaTag))
                _inLiquidArea = false;
        }

        /// <summary>
        /// 用於重設湯匙的狀態，以防止玩家把湯匙弄丟，導致小遊戲不能繼續。
        /// 此方法用於 UI 上的 Button 組件裡的 On Clicked 做使用。
        /// </summary>
        public void ResetPosition()
        {
            _rig2D.linearVelocity = new Vector2();
            
            _rig2D.linearDamping = normalLinearDamping;
            _rig2D.angularDamping = normalAngularDamping;
            
            transform.position = spawnPoint.position;
            transform.rotation = new Quaternion();
        }
    }
}
