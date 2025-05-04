using Interface;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareInteraction : IPlayerInteractable
    {
        private KitchenwareManager KitchenwareManager { get; set; }
        
        private void Awake() => KitchenwareManager = GetComponent<KitchenwareManager>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            PlayerEnter(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            PlayerLeave(this);
            KitchenwareManager.CloseMenu();
        }
        
        public override void Interact() => KitchenwareManager.Interact();
    }
}