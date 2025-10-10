using UnityEngine;

namespace System.Cook.Cook_Game.System.Child
{
    internal sealed class DetectSystem : MonoBehaviour
    {
        [field: Header("Tag")]
        [field: SerializeField] private string UtensilsTag;

        public event Action<bool> UtensilsDetected;

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
            UtensilsDetected?.Invoke(inDetectArea);
        }
    }
}