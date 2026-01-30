using System;
using Common.Object;
using UI_System.Player_UI_System.Main;
using UnityEngine;

namespace UI_System.Player_UI_System.Child
{
    public sealed class FastButtonUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Button backpackFastUIButton;
                                private Action _backpackFastUIButtonCleanupAction;

        private void Awake()
        {
            if (backpackFastUIButton == null)
            {
                Debug.Log($"{nameof(FastButtonUISystem)} > {nameof(backpackFastUIButton)} cannot be null.");
            }
            else
            {
                backpackFastUIButton.OnClicked += PlayerUISystem.RequireBackpackUI;
                _backpackFastUIButtonCleanupAction = () =>
                {
                    backpackFastUIButton.OnClicked -= PlayerUISystem.RequireBackpackUI;
                    _backpackFastUIButtonCleanupAction = null;
                };
            }
        }
        
        private void OnDestroy()
        {
            _backpackFastUIButtonCleanupAction?.Invoke();
        }
    }
}