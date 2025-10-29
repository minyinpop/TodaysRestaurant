using System.Collections;
using System.Collections.Generic;
using System.Dialogue.Utage;
using Febucci.UI;
using Febucci.UI.Effects;
using TMPro;
using UnityEngine;

namespace System.Dialogue.Custom
{
    internal sealed class ChapterTitleSystem : MonoBehaviour
    {
        [field: Header("Title")]
        [field: SerializeField] private TextMeshProUGUI TitleText;
        [field: SerializeField] private TypewriterByWord TitleTypewriter;
        [field: SerializeField] private AppearanceScriptableBase TitleAppearance;
        [field: SerializeField] private AppearanceScriptableBase TitleDisappearance;
        
        [field: Header("Subtitle")]
        [field: SerializeField] private TextMeshProUGUI SubtitleText;
        [field: SerializeField] private TypewriterByWord SubtitleTypewriter;
        [field: SerializeField] private AppearanceScriptableBase SubtitleAppearance;
        [field: SerializeField] private AppearanceScriptableBase SubtitleDisappearance;

        private readonly Queue<Action> ActiveActions = new();

        private IEnumerator ShowCor;
        
        private void OnEnable()
        {
            UtageReceiveMessageSystem.ShowChapterTitle += Show;
            ActiveActions.Enqueue(() => UtageReceiveMessageSystem.ShowChapterTitle -= Show);
        }

        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
            if (ShowCor is not null) { StopCoroutine(ShowCor); ShowCor = null; }
        }

        private void Show(Action onComplete)
        {
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator ShowCoroutine()
            {
                TitleTypewriter.StartShowingText();
                yield return new WaitForSeconds(TitleAppearance.baseDuration);

                SubtitleTypewriter.StartShowingText();
                yield return new WaitForSeconds(SubtitleAppearance.baseDuration + 3);

                SubtitleTypewriter.StartDisappearingText();
                yield return new WaitForSeconds(SubtitleAppearance.baseDuration);

                TitleTypewriter.StartDisappearingText();
                yield return new WaitForSeconds(TitleAppearance.baseDuration + 1);

                onComplete?.Invoke();
            }
        }
    }
}