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
        // ========== { 路徑相關 } ==========
        
        [field: Header("各項資料"), Tooltip("屬性的資料。"), SerializeField]
        private CustomerAttributeSO CustomerAttribute { get; set; }
        
        [field: Tooltip("路徑的資料。"), SerializeField]
        private CustomerPathSO CustomerPath { get; set; }
        
        // 當前尋路的索引。
        private int CurrentPathIndex { get; set; }
        
        
        
        // ========== { 自身組件 } ==========
        
        private Rigidbody Rig { get; set; }



        private void Awake()
        {
            Rig = GetComponent<Rigidbody>();
        }
        
        
        
        private void FixedUpdate()
        {
            var x = CustomerPath.PathList[CurrentPathIndex].x - transform.position.x;
            var z = CustomerPath.PathList[CurrentPathIndex].z - transform.position.z;
            var direction = new Vector3(x, 0, z).normalized;
            var moveSpeed = CustomerAttribute.BasicMoveSpeed * Time.fixedDeltaTime;
            
            Rig.linearVelocity = new Vector3(direction.x, Rig.linearVelocity.y, direction.z) * moveSpeed;
        }
        
        
        
        private void Update()
        {
            if (CurrentPathIndex < CustomerPath.PathList.Length)
            {
                if (Vector3.Distance(transform.position, CustomerPath.PathList[CurrentPathIndex]) < .1f)
                    CurrentPathIndex++;
            }
        }
    }
}