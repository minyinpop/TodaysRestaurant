using System;
using UnityEngine;

namespace Player_System.Object
{
    public sealed class DetectArea : MonoBehaviour
    {
        [field: Header("Settings")]
        [field: SerializeField] private LayerMask Layer;
        
        public event Action<GameObject> OnEnterDetect;
        public event Action<GameObject> OnExitDetect;
        
        private void OnTriggerEnter(Collider other)
        {
            if (1 << other.gameObject.layer != Layer.value) return;
            OnEnterDetect?.Invoke(other.gameObject);
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (1 << other.gameObject.layer != Layer.value) return;
            OnEnterDetect?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (1 << other.gameObject.layer != Layer.value) return;
            OnExitDetect?.Invoke(other.gameObject);
        }
    }
}