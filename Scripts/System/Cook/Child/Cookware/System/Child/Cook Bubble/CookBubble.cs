using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Button = General.Object.Button;

namespace System.Cook.Cookware.System.Child.Cook_Bubble
{
    [RequireComponent(typeof(Button))]
    internal sealed class CookBubble : MonoBehaviour
    {
        [field: Header("Button")]
        [field: SerializeField] private Button Button;
        
        [field: Header("Progress Bar")]
        [field: SerializeField] private Image ProgressBar;
        [field: SerializeField] private Color FullColor;
        [field: SerializeField] private Color EmptyColor;
        
        private void OnEnable()
        {
            Button.OnClick += OnClicked;
        }

        private void OnDisable()
        {
            Button.OnClick -= OnClicked;
            if (CoutDownCor is not null)
            {
                StopCoroutine(CoutDownCor);
                CoutDownCor = null;
            }
        }

        public void SetInteractable(bool interactable)
        {
            Button.SetInteractable(interactable);
        }
        
        public event Action OnClick;
        private void OnClicked()
        {
            OnClick?.Invoke();
        }
        
        private IEnumerator CoutDownCor;
        public void CoutDown(float time, Action onComplete)
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