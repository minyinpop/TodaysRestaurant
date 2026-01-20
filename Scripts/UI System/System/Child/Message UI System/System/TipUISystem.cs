using System;
using Common.Value;
using Message_System.Object;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Message_UI_System
{
    public sealed class TipUISystem : MonoBehaviour, IUISystem
    {
        [field: Header("UI")]
        [field: SerializeField] private RectTransform popUpUIParent;
        [field: SerializeField] private GameObject popUpUIPrefab;
        
        private PopUpUI _popUpUI;
        
        public void SpawnUI(PopUpUIContent content, Action onConfirm = null)
        {
            _popUpUI = Instantiate(popUpUIPrefab, popUpUIParent).GetComponent<PopUpUI>();
            _popUpUI.Initialize(content);

            _popUpUI.OnClickConfirmButton += OnConfirmButtonClicked;
            _popUpUI.SetButtonInteractable(true);
            return;
            
            void OnConfirmButtonClicked()
            {
                _popUpUI.OnClickConfirmButton -= OnConfirmButtonClicked;
                _popUpUI.SetButtonInteractable(false);
                
                Destroy(_popUpUI.gameObject);
                _popUpUI = null;
                
                onConfirm.Invoke();
            }
        }
    }
}