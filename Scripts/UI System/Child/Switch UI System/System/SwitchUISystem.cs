using System;
using Common.Value;
using UI_System.Child.Switch_UI_System.Object;
using UnityEngine;

namespace UI_System.Child.Switch_UI_System.System
{
    public sealed class SwitchUISystem : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private RectTransform popUpUIParent;
        [field: SerializeField] private GameObject popUpUIPrefab;
        
        [field: Header("Mask")]
        [field: SerializeField] private GameObject mask;
        
        private PopUpUI_Switch _popUpUI;

        public void SpawnUI(PopUpUIContent content, Action onConfirm, Action onCancel = null)
        {
            if (onConfirm == null)
            {
                Debug.LogError("SwitchUISystem > SpawnUI > onConfirm callback cannot be null.");
                return;
            }
            
            mask.SetActive(true);

            _popUpUI = Instantiate(popUpUIPrefab, popUpUIParent).GetComponent<PopUpUI_Switch>();
            _popUpUI.Initialize(content, OnConfirmButtonClicked, OnCancelButtonClicked);
            return;
            
            void OnConfirmButtonClicked()
            {
                Cleanup();
                onConfirm.Invoke();
            }
            
            void OnCancelButtonClicked()
            {
                Cleanup();
                onCancel?.Invoke();
            }

            void Cleanup()
            {
                mask.SetActive(false);
                
                Destroy(_popUpUI.gameObject);
                _popUpUI = null;
            }
        }
    }
}