using UnityEngine;

namespace BATTLE.OTHER
{
    internal class InitiativeCoin : MonoBehaviour
    {
        // Components
        private RectTransform RectTransform { get; set; }
        
        // State
        private bool CanClick { get; set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public void SlideInScreen()
        {
            
        }
    }
}