using System;
using System.Collections;
using Common.Button;
using Common.Item.Data.Ingredient;
using Common.Value;
using TMPro;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Item_Get_UI_System.Object
{
    internal sealed class ItemGetUI : MonoBehaviour
    {
        [field: Header("Title")]
        [field: SerializeField] private TextMeshProUGUI messageTMP;
        
        [field: Header("")]
        [field: SerializeField] private Transform itemSlotParent;
        // [field: SerializeField] private TODO 物品格子的 Prefab
        
        [field: Header("Button")]
        [field: SerializeField] private Button confirmButton;

        private Action _onConfirmButtonCleanupAction;

        private IEnumerator _showMessageCoroutine;

        private void OnDisable()
        {
            if (_showMessageCoroutine is not null)
            {
                StopCoroutine(_showMessageCoroutine);
                _showMessageCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            _onConfirmButtonCleanupAction?.Invoke();
        }
        
        public void ShowMessage(PopUpUIContent content, IngredientSO[] ingredients, Action onConfirm)
        {
            _showMessageCoroutine = ShowMessageCoroutine();
            StartCoroutine(_showMessageCoroutine);
            return;

            IEnumerator ShowMessageCoroutine()
            {
                content.GetValues(
                    message: out var message,
                    confirmButtonTitle: out var confirmButtonTitle,
                    cancelButtonTitle: out _,
                    closeButtonTitle: out _);
                
                #region 設定 UI 文字
                    messageTMP.SetText(message);
                    confirmButton.SetTitle(confirmButtonTitle);
                #endregion
                
                #region 顯示戰利品
                #endregion

                yield return null;
            }
        }

        public void ClearMessage()
        {
            messageTMP.SetText(string.Empty);
            confirmButton.SetTitle(string.Empty);
            
            _onConfirmButtonCleanupAction?.Invoke();
        }
    }
}