using UnityEngine;

namespace Restaurant.Kitchenware
{
    internal class KitchenwareDetector : MonoBehaviour
    {
        private KitchenwareManager Manager { get; set; }

        private void Awake() => Manager = GetComponent<KitchenwareManager>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                Manager.SetPlayerEnter(true);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
                Manager.SetPlayerEnter(true);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                Manager.SetPlayerEnter(false);
        }
    }
}