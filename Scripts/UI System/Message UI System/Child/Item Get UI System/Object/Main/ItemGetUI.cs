using System;
using System.Collections;
using System.Collections.Generic;
using Common.Button;
using Common.Item.Data;
using Common.Value;
using TMPro;
using UI_System.Message_UI_System.Child.Item_Get_UI_System.Object.Child;
using UnityEngine;

namespace UI_System.Message_UI_System.Child.Item_Get_UI_System.Object.Main
{
    internal sealed class ItemGetUI : MonoBehaviour
    {
        [field: Header("Title")]
        [field: SerializeField] private TextMeshProUGUI messageTMP;
        
        [field: Header("Item Get Slot")]
        [field: SerializeField] private Transform slotParent;
        [field: SerializeField] private ItemGetSlotContainer slotContainerPrefab;
                                private readonly Queue<ItemGetSlotContainer> _slotContainers = new();
        [field: SerializeField] private ItemGetSlot slotPrefab;
                                private readonly Queue<ItemGetSlot> _slots = new();
        
        [field: Header("Button")]
        [field: SerializeField] private Button confirmButton;

        private Action _onConfirmButtonCleanupAction;

        private IEnumerator _itemGetCoroutine;

        private void Awake()
        {
            if (messageTMP is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(messageTMP)} cannot be null.");
            }
            
            if (slotParent is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(slotParent)} cannot be null.");
            }
            
            if (slotContainerPrefab is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(slotContainerPrefab)} cannot be null.");
            }
            
            if (slotPrefab is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(slotPrefab)} cannot be null.");
            }
            
            if (confirmButton is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(confirmButton)} cannot be null.");
            }
        }

        private void OnEnable()
        {
            Debug.Log($"啟用 {name}");
            
            messageTMP.SetText(string.Empty);
            confirmButton.SetInteractable(false);
            confirmButton.gameObject.SetActive(false);
            
            foreach (var container in _slotContainers)
            {
                Destroy(container.gameObject);
            }

            _slotContainers.Clear();
        }

        private void OnDisable()
        {
            if (_itemGetCoroutine is not null)
            {
                StopCoroutine(_itemGetCoroutine);
                _itemGetCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            _onConfirmButtonCleanupAction?.Invoke();
        }

        public void SetMessage(PopUpUIContent content)
        {
            content.GetValues(
                message: out var message,
                confirmButtonTitle: out var confirmButtonTitle,
                cancelButtonTitle: out _,
                closeButtonTitle: out _);
            
            messageTMP.SetText(message);
            confirmButton.SetTitle(confirmButtonTitle);
        }

        public void ShowItemGet(IReadOnlyList<ItemSO> items, Action onConfirm)
        {
            _itemGetCoroutine = ShowItemGetCoroutine();
            StartCoroutine(_itemGetCoroutine);
            return;

            IEnumerator ShowItemGetCoroutine()
            {
                #region 顯示戰利品
                    var container = Instantiate(slotContainerPrefab.gameObject, slotParent).GetComponent<ItemGetSlotContainer>();
                    _slotContainers.Enqueue(container);
                    
                    foreach (var item in items)
                    {
                        if (container.HaveEmptySpace())
                        {
                            container.AddSlot(slotPrefab, item, out var newSlot);
                            _slots.Enqueue(newSlot);
                        }
                        else
                        {
                            container = Instantiate(slotContainerPrefab.gameObject, slotParent).GetComponent<ItemGetSlotContainer>();
                            _slotContainers.Enqueue(container);
                            
                            container.AddSlot(slotPrefab, item, out var newSlot);
                            _slots.Enqueue(newSlot);
                            
                        }
                        
                        yield return new WaitForSeconds(.25f);
                    }
                #endregion
                
                #region 啟用確認按鈕
                    confirmButton.SetInteractable(true);
                    confirmButton.gameObject.SetActive(true);
                    
                    confirmButton.OnClick += onConfirm;
                    _onConfirmButtonCleanupAction = () =>
                    {
                        confirmButton.SetInteractable(false);
                        confirmButton.OnClick -= onConfirm;
                    };
                #endregion
            }
        }

        public void ClearMessage()
        {
            messageTMP.SetText(string.Empty);
            confirmButton.SetInteractable(false);
            
            foreach (var container in _slotContainers)
            {
                Destroy(container.gameObject);
            }

            _slotContainers.Clear();
        }
    }
}