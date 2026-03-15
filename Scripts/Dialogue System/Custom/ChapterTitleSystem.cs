using System.Collections;
using Dialogue_System.Utage;
using Febucci.UI;
using Febucci.UI.Effects;
using TMPro;
using UnityEngine;
using Utage;

namespace Dialogue_System.Custom
{
    internal sealed class ChapterTitleSystem : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private AdvEngine advEngine;
        
        [field: Header("Appearance Settings")]
        [field: SerializeField] private AppearanceScriptableBase Appearance;
        [field: SerializeField] private AppearanceScriptableBase Disappearance;
        
        [field: Header("Title")]
        [field: SerializeField] private TextMeshProUGUI TitleText;
        [field: SerializeField] private TypewriterByWord TitleTypewriter;
        
        [field: Header("Subtitle")]
        [field: SerializeField] private TextMeshProUGUI SubtitleText;
        [field: SerializeField] private TypewriterByWord SubtitleTypewriter;
        
        private IEnumerator ShowCor;
        
        private void Awake()
        {
            if (advEngine is null)
            {
                Debug.Log($"{nameof(ChapterTitleSystem)} > {nameof(advEngine)} cannot be null.)");
                Destroy(gameObject);
                return;
            }

            UtageReceiveMessageSystem.ShowChapterTitle += Show;
        }

        private void OnDisable()
        {
            if (ShowCor is not null)
            {
                StopCoroutine(ShowCor); ShowCor = null;
            }
        }

        private void OnDestroy()
        {
            UtageReceiveMessageSystem.ShowChapterTitle -= Show;
        }

        private void Show(string title, string subtitle, float duration)
        {
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator ShowCoroutine()
            {
                advEngine.Config.NoSkip = true;
                
                TitleTypewriter.ShowText(title);
                SubtitleTypewriter.ShowText(subtitle);
                
                TitleTypewriter.StartShowingText();
                yield return new WaitForSeconds(Appearance.baseDuration);
                SubtitleTypewriter.StartShowingText();
                yield return new WaitForSeconds(Appearance.baseDuration + duration);

                SubtitleTypewriter.StartDisappearingText();
                yield return new WaitForSeconds(Disappearance.baseDuration);
                TitleTypewriter.StartDisappearingText();
                yield return new WaitForSeconds(Disappearance.baseDuration);
                
                advEngine.Config.NoSkip = false;
            }
        }
    }
}