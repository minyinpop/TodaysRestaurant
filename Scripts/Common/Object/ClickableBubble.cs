using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Object
{
    internal sealed class ClickableBubble : MonoBehaviour
    {
        [field: Header("Component Settings")]
        [field: SerializeField] private Button button;
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
        
        public event Action onClick;

        private void OnEnable()
        {
            if (button is null) return;
            button.onClick += HandleButtonClick;
            _cleanUpActions.Add(() => button.onClick -= HandleButtonClick);
        }

        private void OnDisable()
        {
            foreach (var action in _cleanUpActions) action();
            _cleanUpActions.Clear();

            if (_countDownCor is null) return;
            StopCoroutine(_countDownCor);
            _countDownCor = null;
        }

        private void HandleButtonClick()
        {
            onClick?.Invoke();
        }

        public void SetInteractable(bool interactable)
        {
            if (!this.interactable)
            {
                button?.SetInteractable(false);
                return;
            }

            foreach (var image in colorChangeImages)
                image.color = new Color(image.color.r, image.color.g, image.color.b, interactable ? canInteractColor.a : cannotInteractColor.a);
            
            button?.SetInteractable(interactable);
        }
        
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