using System.Collections;
using System.Initiative_System.Object.Toss_Result_Text;
using Data.Initiative_Coin;
using UnityEngine;

namespace System.Initiative_System.System.Child
{
    internal sealed class TossResultTextSystem : MonoBehaviour
    {
        [field: Header("Object")]
        [field: SerializeField] private TossResultText TopText;
        [field: SerializeField] private TossResultText BottomText;
        
        [field: Header("Content")]
        [field: SerializeField] private ContentSettings HeadsContentSettings;
        [field: SerializeField] private ContentSettings TailsContentSettings;

        private IEnumerator ShowCor;

        private void OnDisable()
        {
            if (ShowCor is not null)
            {
                StopCoroutine(ShowCor);
                ShowCor = null;
            }
        }

        #region Show
            public void Show(TossResult result, float callbackDelay, Action onComplete = null)
            {
                switch (result)
                {
                    case TossResult.Heads:
                    {
                        ShowCor = ShowProcess(HeadsContentSettings, callbackDelay, onComplete);
                        StartCoroutine(ShowCor);
                        break;
                    }
                    case TossResult.Tails:
                    {
                        ShowCor = ShowProcess(TailsContentSettings, callbackDelay, onComplete);
                        StartCoroutine(ShowCor);
                        break;
                    }
                }
            }

            private IEnumerator ShowProcess(ContentSettings settings, float callbackDelay, Action onComplete = null)
            {
                const float showDelay = 1f;
                
                settings.GetTextColor(out var textColor);
                
                settings.GetTopContent(out var topContent);
                settings.GetBottomContent(out var bottomContent);
                
                TopText.Show(textColor, topContent);
                yield return new WaitForSeconds(showDelay);
                
                BottomText.Show(textColor, bottomContent);
                yield return new WaitForSeconds(callbackDelay);
                
                onComplete?.Invoke();
                ShowCor = null;
            }
        #endregion

        public void Hide(float hideDuration)
        {
            TopText.Hide(hideDuration);
            BottomText.Hide(hideDuration);
        }
    }

    [Serializable]
    internal sealed class ContentSettings
    {
        [field: Header("Color")]
        [field: SerializeField] private Color TextColor;
        
        [field: Header("Content")]
        [field: SerializeField] private string TopContent;
        [field: SerializeField] private string BottomContent;
        
        public void GetTextColor(out Color color)
        {
            color = TextColor;
        }

        public void GetTopContent(out string content)
        {
            content = TopContent;
        }
        
        public void GetBottomContent(out string content)
        {
            content = BottomContent;
        }
    }
}