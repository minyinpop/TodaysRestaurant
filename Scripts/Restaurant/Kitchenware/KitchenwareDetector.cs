using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareDetector : MonoBehaviour
    {
        private KitchenwareManager Manager { get; set; }

        private void Awake() => Manager = GetComponent<KitchenwareManager>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                Manager.CurrentBubbleBase.PlayerEnter();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                Manager.CurrentBubbleBase.PlayerEnter();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
                Manager.CurrentBubbleBase.PlayerLeave();
        }
    }
}