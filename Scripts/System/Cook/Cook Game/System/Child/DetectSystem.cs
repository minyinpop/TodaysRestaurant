using System.Cook.Cook_Game.Object;
using UnityEngine;

namespace System.Cook.Cook_Game.System.Child
{
    internal sealed class DetectSystem : MonoBehaviour
    {
        [field: Header("Component")]
        [field: SerializeField] private BoxCollider2D BC2;
        
        [field: Header("Tag")]
        [field: SerializeField] private string UtensilsTag;
        
        [field: Header("Object")]
        [field: SerializeField] private Utensils Utensils;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Detect(other.gameObject, true);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            Detect(other.gameObject, false);
        }

        private void Detect(GameObject other, bool inDetectArea)
        {
            if (other is null) return;
            if (!other.CompareTag(UtensilsTag)) return;
            Utensils.IsInDetectArea(inDetectArea);
        }
    }
}