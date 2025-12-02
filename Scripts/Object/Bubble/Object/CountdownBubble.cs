using System;
using System.Collections;
using System.Collections.Generic;
using Object.Bubble.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Object.Bubble.Object
{
    internal sealed class CountdownBubble : MonoBehaviour, IBubble
    {
        [field: Header("Button")]
        [field: SerializeField] private Button Button;
        
        [field: Header("Color")]
        [field: SerializeField] private List<Image> Images;
        [field: SerializeField] private Color CanInteractColor;
        [field: SerializeField] private Color CannotInteractColor;
        
        [field: Header("Progress Bar")]
        [field: SerializeField] private Image ProgressBar;
        [field: SerializeField] private Color FullColor;
        [field: SerializeField] private Color EmptyColor;
        
        private readonly List<Action> ActiveActions = new();
        
        public event Action OnClickBubble;

        private IEnumerator CountDownCor;

        private void OnEnable()
        {
            Button.OnClick += OnClick;
            ActiveActions.Add(() => Button.OnClick -= OnClick);
            return;

            void OnClick() { OnClickBubble?.Invoke(); }
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action();
            ActiveActions.Clear();
            
            if (CountDownCor is not null)
            {
                StopCoroutine(CountDownCor);
                CountDownCor = null;
            }
        }
        
        public void SetInteractable(bool interactable)
        {
            foreach (var image in Images) image.color = new Color(image.color.r, image.color.g, image.color.b, interactable ? CanInteractColor.a : CannotInteractColor.a);
            Button.SetInteractable(interactable);
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
    }
}