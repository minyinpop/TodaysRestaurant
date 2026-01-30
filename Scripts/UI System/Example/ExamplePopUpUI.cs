using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Item.Ingredient;
using Common.Object;
using Common.Object.Storage_Slot;
using Common.Value;
using TMPro;
using UnityEngine;

namespace UI_System
{
    internal sealed class ExamplePopUpUI : MonoBehaviour
    {
        private void OnEnable()
        {
            if (ConfirmButton is not null)
                ConfirmButton.OnClicked += OnConfirmButtonClicked;
            if (CancelButton is not null)
                CancelButton.OnClicked += OnCancelButtonClicked;
            if (CloseButton is not null)
                CloseButton.OnClicked += OnCloseButtonClicked;
        }
        private void OnDisable()
        {
            if (ConfirmButton is not null)
                ConfirmButton.OnClicked -= OnConfirmButtonClicked;
            if (CancelButton is not null)
                CancelButton.OnClicked -= OnCancelButtonClicked;
            if (CloseButton is not null)
                CloseButton.OnClicked -= OnCloseButtonClicked;
            if (ShowItemCor is not null)
            {
                StopCoroutine(ShowItemCor);
                ShowItemCor = null;
            }
        }
        
        #region General
            public void Initialize(PopUpUIContent content)
            {
                SetContent(content);
            }

            private void SetContent(PopUpUIContent content)
            {
                content.GetValues(out var message, out var confirm, out var cancel, out var close);
                MessageTMP?.SetText(message);
                ConfirmButton?.SetTitle(confirm);
                CancelButton?.SetTitle(cancel);
                CloseButton?.SetTitle(close);
            }
        #endregion
        
        #region Message
            [field: Header("Message")]
            [field: SerializeField] private TextMeshProUGUI MessageTMP;
            #endregion
            
            #region Item Slot
            [field: Header("Item Slot")]
            [field: SerializeField] private Transform SpawnParent;
            [field: SerializeField] private GameObject SlotPrefab;

            private List<StorageSlot> ItemSlots = new();
            
            private IEnumerator ShowItemCor;

            public void ShowItem(List<IngredientSO> items, Action onComplete)
            {
                if (onComplete == null)
                {
                    Debug.LogError("PopUpUI > ShowItem > onComplete cannot be null.");
                    return;
                }
                
                ShowItemCor = ShowItemCoroutine();
                StartCoroutine(ShowItemCor);
                return;

                IEnumerator ShowItemCoroutine()
                {
                    var completes = new List<bool>();
                    for (var i = 0; i < items.Count; i++)
                    {
                        var index = i;
                        var slot = Instantiate(SlotPrefab, SpawnParent);
                        var slotScript = slot.GetComponent<StorageSlot>();
                        var item = items[index];
                        completes.Add(false);
                        ItemSlots.Add(slotScript);
                        slotScript.TryAddItem(item,
                            onComplete: () =>
                            {
                                completes[index] = true;
                            });
                        yield return new WaitForSeconds(.1f);
                    }
                    
                    yield return new WaitUntil(() => completes.All(c => c));
                    onComplete.Invoke();
                    ShowItemCor = null;
                }
            }
        #endregion
        
        #region Button
            [field: Header("Button")]
            [field: SerializeField] private Button ConfirmButton;
            [field: SerializeField] private Button CancelButton;
            [field: SerializeField] private Button CloseButton;
            
            public event Action OnClickConfirmButton;
            public event Action OnClickCancelButton;
            public event Action OnClickCloseButton;

            public void SetButtonInteractable(bool interactable)
            {
                ConfirmButton?.SetInteractable(interactable);
                CancelButton?.SetInteractable(interactable);
                CloseButton?.SetInteractable(interactable);
            }

            private void OnConfirmButtonClicked()
            {
                OnClickConfirmButton?.Invoke();
            }
            
            private void OnCancelButtonClicked()
            {
                OnClickCancelButton?.Invoke();
            }
            
            private void OnCloseButtonClicked()
            {
                OnClickCloseButton?.Invoke();
            }
        #endregion
    }
}