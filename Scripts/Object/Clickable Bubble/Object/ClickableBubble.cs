using System;
using System.Collections;
using System.Collections.Generic;
using Data.Item.Base;
using Object.Clickable_Bubble.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Object.Clickable_Bubble.Object
{
    internal sealed class ClickableBubble : MonoBehaviour, IClickableBubble
    {
        [field: Header("Component Settings")]
        [field: SerializeField] private Button Button;
        [field: SerializeField] private List<Image> ColorChangeImages;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Interaction Settings")]
        [field: SerializeField] private bool Interactable;
        [field: SerializeField] private Color CanInteractColor;
        [field: SerializeField] private Color CannotInteractColor;
        
        [field: Header("Progress Bar Settings")]
        [field: SerializeField] private Image ProgressBar;
        [field: SerializeField] private Color FullColor;
        [field: SerializeField] private Color EmptyColor;
        
        private readonly List<Action> ActiveActions = new();
        
        private IEnumerator CountDownCor;
        
        public event Action OnClickBubble;

        private void OnEnable()
        {
            if (Button is null) return;
            Button.OnClick += OnClick;
            ActiveActions.Add(() => Button.OnClick -= OnClick);
            return;

            void OnClick()
            {
                OnClickBubble?.Invoke();
            }
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action();
            ActiveActions.Clear();

            if (CountDownCor is null) return;
            StopCoroutine(CountDownCor);
            CountDownCor = null;
        }
        
        public void SetInteractable(bool interactable)
        {
            if (!Interactable)
            {
                Button?.SetInteractable(false);
                return;
            }

            foreach (var image in ColorChangeImages)image.color = new Color(image.color.r, image.color.g, image.color.b, interactable ? CanInteractColor.a : CannotInteractColor.a);
            Button?.SetInteractable(interactable);
        }
        
        public void StartCountDown(float time, Action onComplete)
        {
            CountDownCor = CountDownCoroutine();
            StartCoroutine(CountDownCor);
            return;

            IEnumerator CountDownCoroutine()
            {
                if (ProgressBar is null) yield return new WaitForSeconds(time);
                else
                {
                    var value = ProgressBar.fillAmount;
                    while (true)
                    {
                        var newValue = Mathf.Clamp01(value -= Time.deltaTime / time);
                        ProgressBar.fillAmount = newValue;
                        ProgressBar.color = Color.Lerp(EmptyColor, FullColor, newValue);
                        if (Mathf.Approximately(newValue, 0f)) break;
                        yield return null;
                    }
                }
                
                onComplete?.Invoke();
            }
        }
        
        public void StartCountDown(ItemSO item, float time, Action onComplete)
        {
            CountDownCor = CountDownCoroutine();
            StartCoroutine(CountDownCor);
            return;

            IEnumerator CountDownCoroutine()
            {
                if (item is not null)
                {
                    item.GetItemSprite(out var itemSprite);
                    ItemImage.sprite = itemSprite;
                    ItemImage.gameObject.SetActive(true);
                }
                
                if (ProgressBar is null) yield return new WaitForSeconds(time);
                else
                {
                    var value = ProgressBar.fillAmount;
                    while (true)
                    {
                        var newValue = Mathf.Clamp01(value -= Time.deltaTime / time);
                        ProgressBar.fillAmount = newValue;
                        ProgressBar.color = Color.Lerp(EmptyColor, FullColor, newValue);
                        if (Mathf.Approximately(newValue, 0f)) break;
                        yield return null;
                    }
                }
                
                onComplete?.Invoke();
            }
        }
    }
}