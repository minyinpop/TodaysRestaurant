using System;
using System.Collections;
using Database.Restaurant.Customer.Attribute;
using Database.Restaurant.Customer.Path;
using UnityEngine;

namespace Restaurant.Customer
{
    [RequireComponent(typeof(Rigidbody))]
    public class CustomerController : MonoBehaviour
    {
        [field: Header("顧客的資料")]
        [field: SerializeField] private CustomerAttributeSO Attribute { get; set; }
        [field: SerializeField] private CustomerPathSO Path { get; set; }
        
        private CustomerManager CustomerManager { get; set; }
        private Rigidbody Rig { get; set; }
        
        private IEnumerator WalkToSeatCoroutine { get; set; }

        public event Action GoToSeat;
        public event Action OnSeat;
        public event Action<bool> FlipX;

        private void Awake()
        {
            CustomerManager = GetComponent<CustomerManager>();
            Rig = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            CustomerManager.GoToSeat += StartGoToSeat;
        }

        private void OnDisable()
        {
            CustomerManager.GoToSeat -= StartGoToSeat;
            
            if (WalkToSeatCoroutine is not null)
            {
                StopCoroutine(WalkToSeatCoroutine);
                WalkToSeatCoroutine = null;
            }
        }

        private void StartGoToSeat()
        {
            WalkToSeatCoroutine = GoToSeatProcess();
            StartCoroutine(WalkToSeatCoroutine);
        }

        private IEnumerator GoToSeatProcess()
        {
            GoToSeat?.Invoke();
            
            foreach (var path in Path.GoToSeat)
            {
                while (Vector3.Distance(transform.position, path) > .2f)
                {
                    var dir = (path - transform.position).normalized;
                    
                    if (dir.x > 0)
                        FlipX?.Invoke(true);
                    else if (dir.x < 0)
                        FlipX?.Invoke(false);
                    
                    var x = dir.x * Attribute.MoveAttribute.MoveSpeed * Time.fixedDeltaTime;
                    var y = Rig.linearVelocity.y;
                    var z = dir.z * Attribute.MoveAttribute.MoveSpeed * Time.fixedDeltaTime;
                    
                    Rig.linearVelocity = new Vector3(x, y, z);
                    
                    yield return new WaitForFixedUpdate();
                }
            }
            
            Rig.linearVelocity = Vector3.zero;
            transform.position = Path.SeatPoint;
            
            OnSeat?.Invoke();
        }
    }
}