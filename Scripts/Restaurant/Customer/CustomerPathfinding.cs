using Database.Restaurant.Customer.Attribute;
using Database.Restaurant.Customer.Path;
using UnityEngine;

namespace Restaurant.Customer
{
    // ==================================================
    // 用於執行顧客尋路的程式碼。
    // 使用目標點的方式來移動。
    // ==================================================
    
    [RequireComponent(typeof(CustomerManager))]
    [RequireComponent(typeof(Rigidbody))]
    public class CustomerPathfinding : MonoBehaviour
    {
        // ========== { 尋路相關 } ==========
        
        [field: Header("各項資料"), Tooltip("屬性的資料。"), SerializeField]
        private CustomerAttributeSO CustomerAttribute { get; set; }
        
        [field: Tooltip("路徑的資料。"), SerializeField]
        private CustomerPathSO CustomerPath { get; set; }
        
        // 當前尋路的索引。
        private int CurrentPathIndex { get; set; }
        
        // 判斷顧客是否可以移動。
        private bool CanMove { get; set; } = true;
        
        
        
        // ========== { 自身組件 } ==========
        
        // 自身的 CustomerManager 組件。
        private CustomerManager CustomerManager { get; set; }
        
        // 自身的 Rigidbody 組件。
        private Rigidbody Rig { get; set; }



        private void Awake()
        {
            CustomerManager = GetComponent<CustomerManager>();
            Rig = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            
        }
        
        
        
        private void FixedUpdate()
        {
            if (!CanMove)
                return;
            
            var x = CustomerPath.PathList[CurrentPathIndex].x - transform.position.x;
            var z = CustomerPath.PathList[CurrentPathIndex].z - transform.position.z;
            var direction = new Vector3(x, 0, z).normalized;
            var moveSpeed = CustomerAttribute.BasicMoveSpeed * Time.fixedDeltaTime;
            
            Rig.linearVelocity = new Vector3(direction.x, Rig.linearVelocity.y, direction.z) * moveSpeed;
        }
        
        
        
        private void Update()
        {
            if (!CanMove)
                return;
            
            if (CustomerManager.CustomerState == CustomerManager.CustomerStateEnum.SearchingForTheSeat)
            {
                if (CurrentPathIndex < CustomerPath.PathList.Length)
                {
                    if (Vector3.Distance(transform.position, CustomerPath.PathList[CurrentPathIndex]) < .1f)
                        CurrentPathIndex++;
                }
                else if (CurrentPathIndex >= CustomerPath.PathList.Length)
                {
                    CanMove = false;

                    transform.position = CustomerPath.SeatPosition;

                    Rig.linearVelocity = Vector3.zero;
                    Rig.constraints = RigidbodyConstraints.FreezeAll;

                    CustomerManager.ChangeState(CustomerManager.CustomerStateEnum.OnSeat);
                }
            }
            else if (CustomerManager.CustomerState == CustomerManager.CustomerStateEnum.Leaving)
            {
                // TODO: 執行顧客離開餐廳的邏輯 ......
            }
        }
    }
}