using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Common.Object;
using Common.Object.Storage_Slot;
using Common.Value;
using Item.Ingredient;
using TMPro;
using UnityEngine;

namespace Message_System.Object
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class PopUpUI : MonoBehaviour
    {
        private void OnEnable()
        {
            if (ConfirmButton is not null)
                ConfirmButton.onClick += OnConfirmButtonClicked;
            if (CancelButton is not null)
                CancelButton.onClick += OnCancelButtonClicked;
            if (CloseButton is not null)
                CloseButton.onClick += OnCloseButtonClicked;
        }
        private void OnDisable()
        {
            if (ConfirmButton is not null)
                ConfirmButton.onClick -= OnConfirmButtonClicked;
            if (CancelButton is not null)
                CancelButton.onClick -= OnCancelButtonClicked;
            if (CloseButton is not null)
                CloseButton.onClick -= OnCloseButtonClicked;
            if (ShowItemCor is not null)
            {
                StopCoroutine(ShowItemCor);
                ShowItemCor = null;
            }
        }
        
        #region General
        [field: Header("General")]
        [field: SerializeField] private DoAnimation DoAnimation;

        private IEnumerator ShowCor;

        public void Show(PopUpUIContent content, DoFade_CanvasGroup settings, Action onComplete)
        {
            SetContent(content);
            gameObject.SetActive(true);
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator ShowCoroutine()
            {
                var maskComplete = false;
                var uiComplete = false;
                DoAnimation.DoFade_CanvasGroup(
                    canvasGroup: Mask,
                    settings: settings,
                    onComplete: () =>
                    {
                        maskComplete = true;
                        DoAnimation.DoFade_CanvasGroup(
                            canvasGroup: UI,
                            settings: settings,
                            onComplete: () =>
                            {
                                uiComplete = true;
                            });
                    });
                yield return new WaitUntil(() => maskComplete && uiComplete);
                onComplete?.Invoke();
                ShowCor = null;
            }
        }
        
        public void Hide(DoFade_CanvasGroup settings, Action onComplete)
        {
            ShowCor = HideCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator HideCoroutine()
            {
                var maskComplete = false;
                var uiComplete = false;
                DoAnimation.DoFade_CanvasGroup(
                    canvasGroup: UI,
                    settings: settings,
                    onComplete: () =>
                    {
                        uiComplete = true;
                        DoAnimation.DoFade_CanvasGroup(
                            canvasGroup: Mask,
                            settings: settings,
                            onComplete: () =>
                            {
                                maskComplete = true;
                            });
                    });
                yield return new WaitUntil(() => maskComplete && uiComplete);
                onComplete?.Invoke();
                gameObject.SetActive(false);
                RemoveContent();
                ShowCor = null;
            }
        }

        private void SetContent(PopUpUIContent content)
        {
            content.GetValues(out var message, out var confirm, out var cancel, out var close);
            MessageTMP?.SetText(message);
            ConfirmButton?.SetTitle(confirm);
            CancelButton?.SetTitle(cancel);
            CloseButton?.SetTitle(close);
        }

        private void RemoveContent()
        {
            MessageTMP?.SetText(string.Empty);
            ConfirmButton?.SetTitle(string.Empty);
            CancelButton?.SetTitle(string.Empty);
            CloseButton?.SetTitle(string.Empty);
        }
        #endregion
        
        #region Mask
        [field: Header("Mask")]
        [field: SerializeField] private CanvasGroup Mask;

        public void ShowMask(DoFade_CanvasGroup settings, Action onComplete)
        {
            gameObject.SetActive(true);
            DoAnimation.DoFade_CanvasGroup(
                canvasGroup: Mask,
                settings: settings,
                onComplete: onComplete);
        }
        
        public void HideMask(DoFade_CanvasGroup settings, Action onComplete)
        {
            DoAnimation.DoFade_CanvasGroup(
                canvasGroup: Mask,
                settings: settings,
                onComplete: () =>
                {
                    onComplete?.Invoke();
                    gameObject.SetActive(false);   
                });
        }
        #endregion
        
        #region UI
        [field: SerializeField] private CanvasGroup UI;

        public void ShowUI(PopUpUIContent content, DoFade_CanvasGroup settings, Action onComplete)
        {
            SetContent(content);
            gameObject.SetActive(true);
            DoAnimation.DoFade_CanvasGroup(
                canvasGroup: UI,
                settings: settings,
                onComplete: onComplete);
        }

        public void HideUI(DoFade_CanvasGroup settings, Action onComplete)
        {
            DoAnimation.DoFade_CanvasGroup(
                canvasGroup: UI,
                settings: settings,
                onComplete: () =>
                {
                    onComplete?.Invoke();
                    gameObject.SetActive(false);
                    RemoveContent();
                });
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
                onComplete?.Invoke();
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