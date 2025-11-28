using System.Collections;
using System.Collections.Generic;
using Data.Item.Base;
using UnityEngine;
using UnityEngine.UI;
using Button = Object.Button;

namespace System.Economy.Child.Customer.Child.Bubble.Type
{
    [RequireComponent(typeof(Button))]
    internal sealed class ShowOrderItemBubble : Bubble
    {
        [field: Header("Button")]
        [field: SerializeField] private Image BubbleImage;
        [field: SerializeField] private Button Button;
        
        [field: Header("Image")]
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("Progress Bar")]
        [field: SerializeField] private Image ProgressBar;
        [field: SerializeField] private Color FullColor;
        [field: SerializeField] private Color EmptyColor;
        
        [field: Header("Color")]
        [field: SerializeField] private Image[] Images;
        [field: SerializeField] private Color CanInteractColor;
        [field: SerializeField] private Color CannotInteractColor;
        
        private readonly Queue<Action> ActiveActions = new();
        
        private IEnumerator MainCor;
        
        private void OnEnable()
        {
            Button.OnClick += OnClicked;
            ActiveActions.Enqueue(() => Button.OnClick -= OnClicked);
        }

        private void OnDisable()
        {
            while (ActiveActions.Count > 0) { var action = ActiveActions.Dequeue(); action?.Invoke(); }
            if (MainCor is not null) { StopCoroutine(MainCor); MainCor = null; }
        }

        public override void SetInteractable(bool interactable)
        {
            BubbleImage.raycastTarget = interactable;
            Button.SetInteractable(interactable);
            foreach (var image in Images) image.color = new Color(image.color.r, image.color.g, image.color.b, interactable ? CanInteractColor.a : CannotInteractColor.a);
        }
        
        public override void ShowItem(ItemSO item, float time, Action onComplete)
        {
            MainCor = CountDownCoroutine();
            StartCoroutine(MainCor);
            return;

            IEnumerator CountDownCoroutine()
            {
                item.GetItemSprite(out var itemSprite);
                ItemImage.sprite = itemSprite;
                ItemImage.gameObject.SetActive(true);
                
                yield return new WaitForSeconds(time);
                onComplete?.Invoke();
            }
        }
        
        public override void CountDown(float time, Action onComplete)
        {
            MainCor = CountDownCoroutine();
            StartCoroutine(MainCor);
            return;

            IEnumerator CountDownCoroutine()
            {
                if (ProgressBar is null) yield return new WaitForSeconds(time);
                else
                {
                    ProgressBar.gameObject.SetActive(true);
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