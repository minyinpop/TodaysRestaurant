using System;
using Common.Value;
using UI_System.System.Child.Tip_UI_System.Object;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Tip_UI_System.System
{
    public sealed class TipUISystem : MonoBehaviour, IUISystem
    {
        [field: Header("UI")]
        [field: SerializeField] private RectTransform popUpUIParent;
        [field: SerializeField] private GameObject popUpUIPrefab;
        
        [field: Header("Mask")]
        [field: SerializeField] private GameObject mask;
        
        private PopUpUI_Tip _popUpUI;
        
        public void SpawnUI(PopUpUIContent content, Action onConfirm)
        {
            mask.SetActive(true);
            
            _popUpUI = Instantiate(popUpUIPrefab, popUpUIParent).GetComponent<PopUpUI_Tip>();
            _popUpUI.Initialize(content, OnConfirm);
            return;
            
            void OnConfirm()
            {
                mask.SetActive(false);
                
                Destroy(_popUpUI.gameObject);
                _popUpUI = null;
                
                onConfirm?.Invoke();
            }
        }
    }
}