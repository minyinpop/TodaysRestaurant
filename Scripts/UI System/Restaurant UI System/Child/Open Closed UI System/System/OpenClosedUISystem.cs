using System;
using UI_System.Restaurant_UI_System.Child.Open_Closed_UI_System.Object;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Open_Closed_UI_System.System
{
    public sealed class OpenClosedUISystem : MonoBehaviour
    {
        [field: Header("介面 UI")]
        [field: SerializeField] private OpenClosedUI openClosedUI;

        public event Action OnOpen;
        public event Action OnClosed;

        private void Awake()
        {
            openClosedUI.OnOpen += OnOpenInvoked;
            openClosedUI.OnClosed += OnClosedInvoked;
        }

        private void OnDestroy()
        {
            openClosedUI.OnOpen -= OnOpenInvoked;
            openClosedUI.OnClosed -= OnClosedInvoked;
        }

        private void OnOpenInvoked()
        {
            if (OnOpen is null)
            {
                Debug.Log($"{nameof(OnOpen)} 沒有被訂閱。");
                return;
            }

            OnOpen.Invoke();
        }

        private void OnClosedInvoked()
        {
            if (OnClosed is null)
            {
                Debug.Log($"{nameof(OnClosed)} 沒有被訂閱。");
                return;
            }

            OnClosed.Invoke();
        }
    }
}