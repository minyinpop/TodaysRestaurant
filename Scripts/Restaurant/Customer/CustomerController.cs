using System;
using System.Collections;
using System.Collections.Generic;
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
        
        private IEnumerator CurrentCoroutine { get; set; }
        private IEnumerator WalkCoroutine { get; set; }

        public event Action OnWalk;
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
            CustomerManager.Leave += StartLeave;
        }

        private void OnDisable()
        {
            CustomerManager.GoToSeat -= StartGoToSeat;
            CustomerManager.Leave -= StartLeave;

            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
            
            if (WalkCoroutine is not null)
            {
                StopCoroutine(WalkCoroutine);
                WalkCoroutine = null;
            }
        }

        private void StartGoToSeat()
        {
            CurrentCoroutine = GoToSeatProcess();
            StartCoroutine(CurrentCoroutine);
        }

        private void StartLeave()
        {
            CurrentCoroutine = LeaveProcess();
            StartCoroutine(CurrentCoroutine);
        }
        
        private IEnumerator GoToSeatProcess()
        {
            OnWalk?.Invoke();

            WalkCoroutine = WalkProcess(Path.GoToSeat);
            yield return WalkCoroutine;
            
            Rig.linearVelocity = Vector3.zero;
            transform.position = Path.SeatPoint;

            OnSeat?.Invoke();
        }

        private IEnumerator LeaveProcess()
        {
            OnWalk?.Invoke();

            WalkCoroutine = WalkProcess(Path.Leave);
            yield return WalkProcess(Path.Leave);
            
            Destroy(gameObject);
        }

        private IEnumerator WalkProcess(List<Vector3> paths)
        {
            foreach (var path in paths)
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
        }
    }
}