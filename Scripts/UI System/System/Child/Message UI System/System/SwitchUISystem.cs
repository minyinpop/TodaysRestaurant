using System;
using Common.Value;
using Message_System.Object;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Message_UI_System
{
    public sealed class SwitchUISystem : MonoBehaviour, IUISystem
    {
        [field: Header("UI")]
        [field: SerializeField] private RectTransform popUpUIParent;
        [field: SerializeField] private GameObject popUpUIPrefab;
        
        private PopUpUI _popUpUI;

        public void SpawnUI(PopUpUIContent content, Action onConfirm = null, Action onCancel = null)
        {
            _popUpUI = Instantiate(popUpUIPrefab, popUpUIParent).GetComponent<PopUpUI>();
            _popUpUI.Initialize(content);
            
            _popUpUI.OnClickConfirmButton += OnConfirmButtonClicked;
            _popUpUI.OnClickCancelButton += OnCancelButtonClicked;
            _popUpUI.SetButtonInteractable(true);
            return;
            
            void OnConfirmButtonClicked()
            {
                Cleanup();
                onConfirm.Invoke();
            }
            
            void OnCancelButtonClicked()
            {
                Cleanup();
                onCancel.Invoke();
            }

            void Cleanup()
            {
                _popUpUI.SetButtonInteractable(false);
                _popUpUI.OnClickConfirmButton -= OnConfirmButtonClicked;
                _popUpUI.OnClickCancelButton -= OnCancelButtonClicked;
                
                Destroy(_popUpUI.gameObject);
                _popUpUI = null;
            }
        }
    }
}