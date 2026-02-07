using System;
using System.Collections;
using System.Collections.Generic;
using Common.Data.Item;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Object
{
    public sealed class ClickableBubble : MonoBehaviour
    {
        [field: Header("Component Settings")]
        [field: SerializeField] private Image bubbleImage;
        [field: SerializeField] private Button bubbleButton;
        [field: SerializeField] private List<Image> colorChangeImages;
        [field: SerializeField] private Image itemImage;
        
        [field: Header("Interaction Settings")]
        [field: SerializeField] private bool interactable;
        [field: SerializeField] private Color canInteractColor;
        [field: SerializeField] private Color cannotInteractColor;
        
        [field: Header("Progress Bar Settings")]
        [field: SerializeField] private Image progressBar;
        [field: SerializeField] private Color fullColor;
        [field: SerializeField] private Color emptyColor;
        
        private readonly List<Action> _cleanUpActions = new();
        
        private IEnumerator _countDownCor;
        
        public event Action OnClick;

        private void OnEnable()
        {
            if (bubbleButton is null) return;
            bubbleButton.OnClick += HandleButtonClicked;
            _cleanUpActions.Add(() => bubbleButton.OnClick -= HandleButtonClicked);
        }

        private void OnDisable()
        {
            foreach (var action in _cleanUpActions) action();
            _cleanUpActions.Clear();

            if (_countDownCor is null) return;
            StopCoroutine(_countDownCor);
            _countDownCor = null;
        }

        private void HandleButtonClicked()
        {
            OnClick?.Invoke();
        }

        #region Interaction
            public void SetRaycastTarget(bool toggle)
            {
                bubbleImage.raycastTarget = toggle;
            }
            
            public void SetInteractable(bool toggle)
            {
                if (!interactable)
                {
                    bubbleButton?.SetInteractable(false);
                    return;
                }

                foreach (var image in colorChangeImages)
                    image.color = new Color(image.color.r, image.color.g, image.color.b, toggle ? canInteractColor.a : cannotInteractColor.a);
                
                bubbleButton?.SetInteractable(toggle);
            }
        #endregion
        
        public void StartCountDown(float time, Action onComplete)
        {
            _countDownCor = CountDownCoroutine();
            StartCoroutine(_countDownCor);
            return;

            IEnumerator CountDownCoroutine()
            {
                if (progressBar is null) yield return new WaitForSeconds(time);
                else
                {
                    progressBar.gameObject.SetActive(true);
                    var value = progressBar.fillAmount;
                    while (true)
                    {
                        var newValue = Mathf.Clamp01(value -= Time.deltaTime / time);
                        progressBar.fillAmount = newValue;
                        progressBar.color = Color.Lerp(emptyColor, fullColor, newValue);
                        if (Mathf.Approximately(newValue, 0f)) break;
                        yield return null;
                    }
                }
                
                onComplete?.Invoke();
            }
        }

        #region ShowItem
            public void ShowItem(ItemSO item)
            {
                if (item is null) throw new ArgumentNullException(nameof(item));
                itemImage.sprite = item.ItemSprite;
                itemImage.gameObject.SetActive(true);
            }

            public void ShowItem(ItemSO item, float duration, Action onComplete)
            {
                _countDownCor = CountDownCoroutine();
                StartCoroutine(_countDownCor);
                return;

                IEnumerator CountDownCoroutine()
                {
                    if (item is null) throw new ArgumentNullException(nameof(item));
                    itemImage.sprite = item.ItemSprite;
                    itemImage.gameObject.SetActive(true);
                    yield return new WaitForSeconds(duration);
                    onComplete?.Invoke();
                }
            }
        #endregion
    }
}