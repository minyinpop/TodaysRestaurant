using System;
using System.Collections.Generic;
using Common.Item.Ingredient;
using Common.Value;
using UI_System.Child.Message_UI_System.Object;
using UnityEngine;

namespace UI_System.Child.Message_UI_System.System
{
    internal sealed class ItemGetUISystem : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private RectTransform popUpUIParent;
        [field: SerializeField] private GameObject popUpUIPrefab;
        
        private PopUpUI _popUpUI;

        public void SpawnUI(PopUpUIContent content, List<IngredientSO> items, Action onConfirm)
        {
            if (onConfirm == null)
            {
                Debug.LogError("ItemGetUISystem > SpawnUI > onConfirm cannot be null.");
                return;
            }
            
            _popUpUI = Instantiate(popUpUIPrefab, popUpUIParent).GetComponent<PopUpUI>();
            _popUpUI.Initialize(content);
            
            _popUpUI.ShowItem(items, OnPopUpUIShowItemComplete);
            return;

            void OnPopUpUIShowItemComplete()
            {
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
}