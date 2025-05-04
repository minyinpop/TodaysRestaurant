using Interface;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    [RequireComponent(typeof(KitchenwareInteraction))]
    [RequireComponent(typeof(KitchenwareCook))]
    public class KitchenwareManager : IPlayerInteractable
    {
        private KitchenwareInteraction KitchenwareInteraction { get; set; }
        private KitchenwareCook KitchenwareCook { get; set; }
        
        private void Awake()
        {
            KitchenwareInteraction = GetComponent<KitchenwareInteraction>();
            KitchenwareCook = GetComponent<KitchenwareCook>();
        }
        
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
            KitchenwareInteraction.CloseMenu();
        }

        public override void Interact() => KitchenwareInteraction.Interact();
    }
}