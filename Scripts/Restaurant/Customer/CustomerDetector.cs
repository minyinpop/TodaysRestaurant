using UnityEngine;

namespace Restaurant.Customer
{
    internal class CustomerDetector : MonoBehaviour
    {
        private CustomerManager CustomerManager { get; set; }

        public void Awake()
        {
            CustomerManager = GetComponent<CustomerManager>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                CustomerManager.SetPlayerEnter(true);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                CustomerManager.SetPlayerEnter(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                CustomerManager.SetPlayerEnter(false);
        }
    }
}