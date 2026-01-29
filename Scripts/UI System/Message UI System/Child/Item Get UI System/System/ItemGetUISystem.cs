using System;
using System.Collections.Generic;
using Common.Item.Ingredient;
using Common.Value;
using UI_System.Message_UI_System.Child.Item_Get_UI_System.Object;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Item_Get_UI_System.System
{
    internal sealed class ItemGetUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private ItemGetUI itemGetUI;

        private void Awake()
        {
            if (itemGetUI == null)
            {
                Debug.Log($"{nameof(ItemGetUISystem)} > {nameof(itemGetUI)} cannot be null.");
            }
        }

        public void ShowUI(PopUpUIContent content, List<IngredientSO> items, Action onConfirm)
        {
            if (onConfirm == null)
            {
                Debug.Log($"{nameof(ItemGetUISystem)} > {nameof(ShowUI)} > {nameof(onConfirm)} callback cannot be null.");
                return;
            }
            
            itemGetUI.ShowUI(content);
            itemGetUI.gameObject.SetActive(true);
            
            itemGetUI.ShowItem(items, OnPopUpUIShowItemComplete);
            return;

            void OnPopUpUIShowItemComplete()
            {
                itemGetUI.OnClickConfirmButton += OnConfirmButtonClicked;
                itemGetUI.SetButtonInteractable(true);
                return;
                
                void OnConfirmButtonClicked()
                {
                    itemGetUI.OnClickConfirmButton -= OnConfirmButtonClicked;
                    itemGetUI.SetButtonInteractable(false);
                    
                    Destroy(itemGetUI.gameObject);
                    itemGetUI = null;
                    
                    onConfirm.Invoke();
                }
            }
        }
    }
}