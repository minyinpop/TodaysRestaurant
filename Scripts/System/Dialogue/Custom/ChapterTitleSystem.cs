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
        [field: Header("Appearance Settings")]
        [field: SerializeField] private AppearanceScriptableBase Appearance;
        [field: SerializeField] private AppearanceScriptableBase Disappearance;
        
        [field: Header("Title")]
        [field: SerializeField] private TextMeshProUGUI TitleText;
        [field: SerializeField] private TypewriterByWord TitleTypewriter;
        
        [field: Header("Subtitle")]
        [field: SerializeField] private TextMeshProUGUI SubtitleText;
        [field: SerializeField] private TypewriterByWord SubtitleTypewriter;
        
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

        private void Show(string title, string subtitle)
        {
            ShowCor = ShowCoroutine();
            StartCoroutine(ShowCor);
            return;

            IEnumerator ShowCoroutine()
            {
                TitleTypewriter.ShowText(title);
                SubtitleTypewriter.ShowText(subtitle);
                
                TitleTypewriter.StartShowingText();
                yield return new WaitForSeconds(Appearance.baseDuration);
                SubtitleTypewriter.StartShowingText();
                yield return new WaitForSeconds(Appearance.baseDuration + 3);

                SubtitleTypewriter.StartDisappearingText();
                yield return new WaitForSeconds(Disappearance.baseDuration);
                TitleTypewriter.StartDisappearingText();
                yield return new WaitForSeconds(Disappearance.baseDuration);
            }
        }
    }
}