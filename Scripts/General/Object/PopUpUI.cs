using System;
using System.Collections;
using System.Collections.Generic;
using System.General.DOTween;
using System.Linq;
using Data.Animation.DOTween.Basic;
using Data.General;
using Data.Item.Base;
using TMPro;
using UnityEngine;

namespace General.Object
{
    [RequireComponent(typeof(DoAnimation))]
    internal sealed class PopUpUI : MonoBehaviour
    {
        private void OnEnable()
        {
            if (ConfirmButton is not null)
                ConfirmButton.OnClick += OnConfirmButtonClicked;
            if (CancelButton is not null)
                CancelButton.OnClick += OnCancelButtonClicked;
        }
        private void OnDisable()
        {
            if (ConfirmButton is not null)
                ConfirmButton.OnClick -= OnConfirmButtonClicked;
            if (CancelButton is not null)
                CancelButton.OnClick -= OnCancelButtonClicked;
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
                content.GetValues(out var message, out var confirm, out var cancel);
                MessageTMP?.SetText(message);
                ConfirmButton?.SetTitle(confirm);
                CancelButton?.SetTitle(cancel);
            }

            private void RemoveContent()
            {
                MessageTMP?.SetText(string.Empty);
                ConfirmButton?.SetTitle(string.Empty);
                CancelButton?.SetTitle(string.Empty);
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

            private List<ItemSlot> ItemSlots = new();
            
            private IEnumerator ShowItemCor;

            public void ShowItem(List<ItemSO> items, Action onComplete)
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
                        var slotScript = slot.GetComponent<ItemSlot>();
                        var item = items[index];
                        completes.Add(false);
                        ItemSlots.Add(slotScript);
                        slotScript.Add(item,
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
            
            public event Action OnConfirm;
            public event Action OnCancel;

            public void SetButtonInteractable(bool interactable)
            {
                ConfirmButton?.SetInteractable(interactable);
                CancelButton?.SetInteractable(interactable);
            }

            private void OnConfirmButtonClicked()
            {
                OnConfirm?.Invoke();
            }
            
            private void OnCancelButtonClicked()
            {
                OnCancel?.Invoke();
            }
        #endregion
    }
}