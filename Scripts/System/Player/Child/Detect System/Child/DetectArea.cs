using System.Collections.Generic;
using UnityEngine;

namespace System.Player.Child.Detect_System.Child
{
    internal sealed class DetectArea : MonoBehaviour
    {
        [field: SerializeField] private string Tag;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(Tag)) return;
            DetectObjects.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag(Tag)) return;
            DetectObjects.Remove(other.gameObject);
        }

        private readonly List<GameObject> DetectObjects = new();
        public void OnDetect(out List<GameObject> detectObjects) => detectObjects = DetectObjects;
    }
}