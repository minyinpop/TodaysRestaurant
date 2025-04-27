using System.Collections;
using System.Collections.Generic;
using Database.Restaurant.Customer.Attribute;
using Database.Restaurant.Customer.Path;
using UnityEngine;

namespace Restaurant.Customer
{
    [RequireComponent(typeof(Rigidbody))]
    public class CustomerNavigation : MonoBehaviour
    {
        [field: Header("屬性資料")]
        [field: SerializeField] private CustomerAttributeSO CustomerAttribute { get; set; }
        
        [field: Header("路徑資料")]
        [field: SerializeField] private CustomerPathSO CustomerPath { get; set; }
        
        private Customer Customer { get; set; }
        private Rigidbody Rig { get; set; }
        
        private IEnumerator MoveCoroutine { get; set; }
        
        private Vector3 TargetPoint { get; set; }

        private void Awake()
        {
            Customer = GetComponent<Customer>();
            Rig = GetComponent<Rigidbody>();
        }
        
        private void Start()
        {
            GoInside();
        }

        private void OnDisable()
        {
            if (MoveCoroutine is not null)
            {
                StopCoroutine(MoveCoroutine);
                MoveCoroutine = null;
            }
        }

        private void FixedUpdate()
        {
            var dir = (TargetPoint - transform.position).normalized;
            
            var x = dir.x * CustomerAttribute.MoveAttribute.MoveSpeed * Time.fixedDeltaTime;
            var z = dir.z * CustomerAttribute.MoveAttribute.MoveSpeed * Time.fixedDeltaTime;
            
            Rig.linearVelocity = new Vector3(x, Rig.linearVelocity.y, z);
        }

        private void GoInside()
        {
            MoveCoroutine = MoveProcess(CustomerPath.InSidePath);
            StartCoroutine(MoveCoroutine);
        }
        
        private void GoOutside()
        {
        }

        private IEnumerator MoveProcess(List<Vector3> pathData)
        {
            foreach (var point in pathData)
            {
                TargetPoint = point;
                
                while (Vector3.Distance(transform.position, TargetPoint) > .1f)
                    yield return null;
            }
            
            // TODO 把顧客給瞬移到位置上
            Customer.OnTargetPoint();
        }
    }
}