using System;
using Common.Value;
using UI_System.Message_UI_System.Child.Tip_UI_System.Object;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Tip_UI_System.System
{
    public sealed class TipUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private GameObject mask;
        [field: SerializeField] private TipUI tipUI;

        private void Awake()
        {
            if (mask == null)
            {
                Debug.Log($"{nameof(TipUISystem)} > {nameof(mask)} cannot be null.");
                return;
            }

            if (tipUI == null)
            {
                Debug.Log($"{nameof(TipUISystem)} > {nameof(tipUI)} cannot be null.");
            }
        }

        public void ShowUI(PopUpUIContent content, Action onConfirm)
        {
            mask.SetActive(true);
            
            tipUI.ShowMessage(content, OnConfirm);
            tipUI.gameObject.SetActive(true);
            return;
            
            void OnConfirm()
            {
                mask.SetActive(false);
                
                tipUI.gameObject.SetActive(false);
                tipUI.ClearMessage();
                
                onConfirm?.Invoke();
            }
        }
    }
}