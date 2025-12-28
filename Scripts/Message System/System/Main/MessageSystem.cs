using System;
using System.Collections.Generic;
using Common.Value;
using Item.Ingredient;
using Message_System.System.Child;
using UnityEngine;

namespace Message_System.System.Main
{
    internal sealed class MessageSystem : MonoBehaviour
    {
        #region ItemGetSystem
            [field: SerializeField] private ItemGetSystem ItemGetSystem;
            [field: SerializeField] private List<IngredientSO> TempItems;

            public void ShowItemGetUI(PopUpUIContent content, Action onConfirm = null)
            {
                ItemGetSystem?.Show(content, TempItems, onConfirm);
            }
        #endregion
        
        #region DefeatSystem
            [field: SerializeField] private DefeatSystem DefeatSystem;

            public void ShowDefeatUI(PopUpUIContent content, Action onConfirm = null)
            {
                DefeatSystem?.Show(content, onConfirm);
            }
        #endregion
        
        #region OptionSystem
            [field: SerializeField] private OptionSystem OptionSystem;

            public void ShowOptionUI(PopUpUIContent content, Action onClose = null)
            {
                OptionSystem?.Show(content, onClose);
            }
        #endregion
    }
}