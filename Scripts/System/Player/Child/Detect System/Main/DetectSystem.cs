using System.Collections;
using System.Player.Child.Detect_System.Child;
using UnityEngine;

namespace System.Player.Child.Detect_System.Main
{
    internal sealed class DetectSystem : MonoBehaviour
    {
        [field: Header("Detect Area")]
        [field: SerializeField] private DetectArea CookwareDetectArea;

        private IEnumerator DetectCor;
        
        private void Start()
        {
            StartDetect();
        }
        
        private void OnDisable()
        {
            StopDetect();
        }

        private void StartDetect()
        {
            return;

            IEnumerator DetectCoroutine()
            {
                yield break;
            }
        }

        private void StopDetect()
        {
        }
    }
}