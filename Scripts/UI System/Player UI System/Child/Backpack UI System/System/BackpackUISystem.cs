using Common.Button;
using UI_System.Player_UI_System.Child.Backpack_UI_System.Object;
using UnityEngine;

namespace UI_System.Player_UI_System.Child.Backpack_UI_System.System
{
    public sealed class BackpackUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private GameObject mask;
        [field: SerializeField] private BackpackUI backpackUI;
        [field: SerializeField] private Button fastButton;

        private bool _isBackpackEnabled = true;

        private void Awake()
        {
            if (mask is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(mask)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (backpackUI is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(backpackUI)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (fastButton is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(fastButton)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            fastButton.OnClick += RequireBackpackUI;
            
            SetBackpackUI(_isBackpackEnabled);
        }
        
        private void OnDestroy()
        {
            fastButton.OnClick -= RequireBackpackUI;
        }

        public void SetBackpackUI(bool isEnabled)
        {
            _isBackpackEnabled = isEnabled;
            
            if (_isBackpackEnabled)
            {
                fastButton.gameObject.SetActive(true);
            }
            else
            {
                mask.SetActive(false);
                backpackUI.gameObject.SetActive(false);
                
                fastButton.gameObject.SetActive(false);
            }
        }

        public void RequireBackpackUI()
        {
            if (_isBackpackEnabled)
            {
                mask.SetActive(!mask.gameObject.activeSelf);
                backpackUI.gameObject.SetActive(!backpackUI.gameObject.activeSelf);
            }
        }
    }
}