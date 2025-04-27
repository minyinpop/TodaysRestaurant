using System;
using UnityEngine;

namespace Restaurant.Customer
{
    [RequireComponent(typeof(CustomerController))]
    [RequireComponent(typeof(CustomerAnimator))]
    [RequireComponent(typeof(CustomerOrder))]
    [RequireComponent(typeof(CustomerCheckout))]
    public class Customer : MonoBehaviour
    {
        public event Action GoToSeat;

        private void Start()
        {
            GoToSeat?.Invoke();
        }
    }
}