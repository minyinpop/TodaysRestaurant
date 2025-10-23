using System.Collections;
using System.Collections.Generic;
using System.Cook.Child.Cookware.System.Child.Cook_Bubble.Main;
using UnityEngine;
using UnityEngine.UI;

namespace System.Cook.Child.Cookware.System.Child.Cook_Bubble.Child
{
    internal sealed class GameTimeBubble : Bubble
    {
        [field: Header("Button")]
        [field: SerializeField] private Button Button;
        
        [field: Header("Progress Bar")]
        [field: SerializeField] private Image ProgressBar;
        [field: SerializeField] private Color FullColor;
        [field: SerializeField] private Color EmptyColor;
        
        [field: Header("Color")]
        [field: SerializeField] private List<Image> Images;
        [field: SerializeField] private Color CanInteractColor;
        [field: SerializeField] private Color CannotInteractColor;
        
        private readonly List<Action> ActiveActions = new();
        
        private void OnEnable()
        {
            Button.OnClick += OnClicked;
            ActiveActions.Add(() => Button.OnClick -= OnClicked);
        }

        private void OnDisable()
        {
            foreach (var action in ActiveActions) action?.Invoke();
            ActiveActions.Clear();
            
            if (CoutDownCor is not null)
            {
                StopCoroutine(CoutDownCor);
                CoutDownCor = null;
            }
        }

        public override void SetInteractable(bool interactable)
        {
            foreach (var image in Images) image.color = new Color(image.color.r, image.color.g, image.color.b, interactable ? CanInteractColor.a : CannotInteractColor.a);
            Button.SetInteractable(interactable);
        }
        
        private IEnumerator CoutDownCor;
        public override void CoutDown(float time, Action onComplete)
        {
            CoutDownCor = CoutDownCoroutine();
            StartCoroutine(CoutDownCor);
            return;

            IEnumerator CoutDownCoroutine()
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