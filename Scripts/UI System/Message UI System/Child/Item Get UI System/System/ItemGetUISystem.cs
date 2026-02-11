using System;
using System.Collections.Generic;
using Common.Data.Item.Ingredient;
using Common.Value;
using UI_System.Message_UI_System.Child.Item_Get_UI_System.Object;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Item_Get_UI_System.System
{
    internal sealed class ItemGetUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private GameObject mask;
        [field: SerializeField] private ItemGetUI itemGetUI;

        private void Awake()
        {
            if (mask == null)
            {
                Debug.Log($"{nameof(ItemGetUISystem)} > {nameof(mask)} cannot be null.");
                return;
            }

            if (itemGetUI == null)
            {
                Debug.Log($"{nameof(ItemGetUISystem)} > {nameof(itemGetUI)} cannot be null.");
            }
        }

        public void ShowUI(PopUpUIContent content, List<IngredientSO> items, Action onConfirm)
        {
        }
    }
}