using Input;
using UnityEngine;
using InputAction = UnityEngine.InputSystem.InputAction;

namespace Game.Stockpot
{
    [RequireComponent(typeof(Rigidbody2D))]
    // [RequireComponent(typeof(PolygonCollider2D))]
    public class StockpotSpoon : MonoBehaviour
    {
        [Header("組件"), Tooltip("深煮鍋小遊戲的管理器的類。"), SerializeField]
        private StockpotManager stockpotManager;
        
        [Tooltip("- 湯杓的重生位置。\n- 用於重新歸位的使用。"), SerializeField]
        private Transform spawnPoint;

        [Header("標籤"), Tooltip("- 用於判斷湯匙是否在液體中。\n- 在液體中的湯勺會因為阻力還變慢。"), SerializeField]
        private string liquidAreaTag;

        [Header("阻力設定"), Tooltip("- 在空氣中，湯勺移動的阻力。\n- 預設為 0。"), SerializeField]
        private float normalLinearDamping;
        
        [Tooltip("- 在空氣中，湯勺的旋轉阻力。\n- 預設為 0.05。"), SerializeField]
        private float normalAngularDamping = .05f;

        [Tooltip("- 在液體中，湯勺的移動阻力。\n- 預設為 5。"), SerializeField]
        private float liquidLinearDamping = 5f;
        
        [Tooltip("- 在液體中，湯勺的旋轉阻力。\n- 預設為 3。"), SerializeField]
        private float liquidAngularDamping = 3f;

        [Tooltip("- 物理效果切換速度。\n- 預設為 5。"), SerializeField]
        private float dampingChangeSpeed;

        [Header("攪拌設定"), Tooltip("- 攪拌距離的閥值，採用點到點偵測。\n- 預設為 8。"), SerializeField]
        private float stirringThreshold;

        // 自身的 Rigidbody2D 組件，用於模擬重力使用。
        private Rigidbody2D _rig2D;
        
        // 用來判斷湯勺目前是否在液體裡面，用於模擬液體的物理阻力。
        private bool _inLiquidArea;

        // 用來判斷湯勺目前有沒有被玩家給拿起來。
        private bool _isHandle;
        
        // 主要攝影機的組件，用於偵測滑鼠有沒有點擊到湯勺。
        private Camera _cam;
        
        // 上一個滑鼠的位置的暫存。
        private Vector2 _lastMousePos;
        
        // 用來判斷小遊戲是否結束的暫存。
        private bool _isFinish;
        
        private void Awake()
        {
            _rig2D = GetComponent<Rigidbody2D>();
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            InputSystem.input.Mouse.LeftClick.performed += OnHandleSpoon;
        }
        
        private void OnDisable()
        {
            InputSystem.input.Mouse.LeftClick.performed -= OnHandleSpoon;
        }

        private void FixedUpdate()
        {
            if (_isHandle)
            {
                _rig2D.linearDamping = normalLinearDamping;
                _rig2D.angularDamping = normalAngularDamping;
            }
            else
            {
                var targetLinearDamping = _inLiquidArea ? liquidLinearDamping : normalLinearDamping;
                var targetAngularDamping = _inLiquidArea ? liquidAngularDamping : normalAngularDamping;
                
                _rig2D.linearDamping = Mathf.MoveTowards(_rig2D.linearDamping, targetLinearDamping, dampingChangeSpeed * Time.fixedDeltaTime);
                _rig2D.angularDamping = Mathf.MoveTowards(_rig2D.angularDamping, targetAngularDamping, dampingChangeSpeed * Time.fixedDeltaTime);
            }
        }

        private void Update()
        {
            _rig2D.gravityScale = _isHandle ? 0 : 1;

            // 當小遊戲結束，就把湯勺放下。
            if (_isFinish)
                return;

            // 當玩家握住湯勺，就讓湯勺的中心點跟著玩家的滑鼠。
            if (!_isHandle)
                return;
            
            OnHandlingSpoon();

            // 當玩家握住湯勺並且湯勺在液體的範圍裏面，就執行攪拌的判斷。
            if (_inLiquidArea)
            {
                var currentMousePos = InputSystem.MousePos();
                var distance = Vector2.Distance(currentMousePos, _lastMousePos);

                if (distance > stirringThreshold)
                    OnStirringSoup();
                
                _lastMousePos = currentMousePos;
            }
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
        /// 用於判斷玩家在點擊滑鼠左鍵的一瞬間，是否握住湯勺。
        /// </summary>
        /// <param name="context"> New Input System 的資料。 </param>>
        private void OnHandleSpoon(InputAction.CallbackContext context)
        {
            var hitInfo = Physics2D.Raycast(HandleSpoonDistance(), Vector2.zero);

            if (hitInfo.collider is null)
                return;

            if (hitInfo.collider.gameObject != gameObject)
                return;

            _isHandle = true;
        }

        /// <summary>
        /// 用於判斷玩家是否持續握住湯勺。
        /// </summary>
        private void OnHandlingSpoon()
        {
            if (InputSystem.MouseLeftButtonIsInProgress())
                transform.position = HandleSpoonDistance();
            else
                _isHandle = false;
        }

        /// <summary>
        /// 當呼叫這個方法後，滑鼠就會從當前位置，往前發射一條射線，如果有打到 Collider，就會生成一個 Plane，並返還兩者的相交點。
        /// </summary>
        /// <returns> 返回 Ray 碰到 Plane 的相交點。 </returns>
        private Vector3 HandleSpoonDistance()
        {
            // 用於生成射線的位置。
            var ray = _cam.ScreenPointToRay(InputSystem.MousePos());
            
            // 用於計算射線與平面之間的相交點。
            var xyPlane = new Plane(Vector3.forward, new Vector3(0, 0, transform.position.z));

            // 發射射線，並與 xyPlane 確認相交點位置，過後把位置儲存進 distance 裡面。
            return xyPlane.Raycast(ray, out var distance) ? ray.GetPoint(distance) : new Vector3();
        }

        /// <summary>
        /// 用於執行攪拌程序的方法，目前只有增加進度條而已。
        /// </summary>
        private void OnStirringSoup()
        {
            stockpotManager.AddProgress();
        }

        /// <summary>
        /// 用於重設湯匙的狀態，以防止玩家把湯匙弄丟，導致小遊戲不能繼續。
        /// 此方法用於 UI 上的 Button 組件裡的 On Clicked 做使用。
        /// </summary>
        public void ResetPosition()
        {
            _rig2D.linearVelocity = new Vector2();
            _rig2D.angularVelocity = 0f;
            
            transform.position = spawnPoint.position;
            transform.rotation = new Quaternion();
            
            _rig2D.linearDamping = normalLinearDamping;
            _rig2D.angularDamping = normalAngularDamping;
        }

        /// <summary>
        /// 用於執行小遊戲結束的方法。
        /// </summary>
        public void FinishGame()
        {
            _isFinish = true;
            _isHandle = false;
        }
    }
}
