using System;
using System.Collections.Generic;
using Common.Value;
using Item.Ingredient;
using Message_System.Object;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Message_UI_System
{
    internal sealed class ItemGetUISystem : MonoBehaviour, IUISystem
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