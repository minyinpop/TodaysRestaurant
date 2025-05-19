using System.Collections;
using System.Collections.Generic;
using Database.Restaurant.Customer.Attribute;
using Database.Restaurant.Customer.Path;
using Restaurant.Customer.CustomerState;
using Restaurant.Customer.CustomerState.State;
using UnityEngine;

namespace Restaurant.Customer
{
    [RequireComponent(typeof(Rigidbody))]
    internal class CustomerMove : MonoBehaviour
    {
        private CustomerManager CustomerManager { get; set; }
        private Rigidbody Rig { get; set; }
        
        private AttributeSO Attribute { get; set; }
        private PathSO Path { get; set; }
        
        private IEnumerator CurrentCoroutine { get; set; }

        private void Awake()
        {
            CustomerManager = GetComponent<CustomerManager>();
            Rig = GetComponent<Rigidbody>();
        }

        private void OnDestroy()
        {
            if (CurrentCoroutine is not null)
                StopCoroutine(CurrentCoroutine);
        }

        public void Init(AttributeSO attribute, PathSO path)
        {
            Attribute = attribute;
            Path = path;
        }
        
        
        
        public void WalkToSeat()
        {
            StartWalkProcess(Path.WalkToSeat, new OnSeat());
        }

        public void WalkToCheckout()
        {
            StartWalkProcess(Path.WalkToCheckout, new WaitForCheckout());
        }
        
        
        
        private void StartWalkProcess(List<Vector3> paths, IState nextState)
        {
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }

            CurrentCoroutine = WalkProcess(paths, nextState);
            StartCoroutine(CurrentCoroutine);
        }

        private IEnumerator WalkProcess(List<Vector3> paths, IState nextState)
        {
            foreach (var point in paths)
            {
                while (Vector3.Distance(point, transform.position) > .1f)
                {
                    if (point.x - transform.position.x > 0)
                        CustomerManager.SetFlipX(true);
                    else if (point.x - transform.position.x < 0)
                        CustomerManager.SetFlipX(false);
                    
                    var direction = (point - transform.position).normalized;
                    Rig.linearVelocity = direction * Attribute.MoveAttribute.MoveSpeed * Time.fixedDeltaTime;
                    yield return new WaitForFixedUpdate();
                }
            }

            CustomerManager.SetCustomerState(nextState);
        }
        
        
        
        public void OnSeat()
        {
            Rig.linearVelocity = Vector3.zero;
            transform.position = Path.SeatPoint;
        }
    }
}