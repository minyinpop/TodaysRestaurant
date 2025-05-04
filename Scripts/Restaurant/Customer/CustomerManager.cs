using System;
using UnityEngine;

namespace Restaurant.Customer
{
    [RequireComponent(typeof(CustomerController))]
    [RequireComponent(typeof(CustomerSkin))]
    [RequireComponent(typeof(CustomerAnimator))]
    [RequireComponent(typeof(CustomerOrder))]
    [RequireComponent(typeof(CustomerCheckout))]
    public class CustomerManager : MonoBehaviour
    {
        private CustomerOrder CustomerOrder { get; set; }
        
        public event Action GoToSeat;
        public event Action Leave;

        private void Awake()
        {
            CustomerOrder = GetComponent<CustomerOrder>();
        }

        private void Start()
        {
            GoToSeat?.Invoke();
        }

        private void OnEnable()
        {
            CustomerOrder.HappyToLeave += HappyToLeave;
            CustomerOrder.HaveNoPatience += HaveNoPatience;
        }

        private void OnDisable()
        {
            CustomerOrder.HappyToLeave -= HappyToLeave;
            CustomerOrder.HaveNoPatience -= HaveNoPatience;
        }

        private void HappyToLeave()
        {
            Debug.Log("顧客開心地離開了");
            Leave?.Invoke();
        }
        
        private void HaveNoPatience()
        {
            Debug.Log($"{name} 沒有耐心了");
            Leave?.Invoke();
        }
    }
}