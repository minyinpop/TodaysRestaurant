using UnityEngine;

namespace System.Player.Child.Detect_System.Child
{
    internal sealed class DetectArea : MonoBehaviour
    {
        [field: Header("Settings")]
        [field: SerializeField] private LayerMask Layer;
        
        public event Action<GameObject> OnDetect;
        public event Action<GameObject> OnUnDetect;
        
        private void OnTriggerEnter(Collider other)
        {
            if (1 << other.gameObject.layer != Layer.value) return;
            OnDetect?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (1 << other.gameObject.layer != Layer.value) return;
            OnUnDetect?.Invoke(other.gameObject);
        }
    }
}