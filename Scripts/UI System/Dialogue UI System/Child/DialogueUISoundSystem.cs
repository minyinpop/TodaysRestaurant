using System;
using System.Collections;
using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Audio_System.Main;
using Common.Dialogue.SO.Child.Sound;
using TMPro;
using UnityEngine;

namespace UI_System.Dialogue_UI_System.Child
{
    [RequireComponent(typeof(DoAnimation))]
    public sealed class DialogueUISoundSystem : MonoBehaviour
    {
        [field: Header("自身組件")]
        [field: SerializeField] private new DoAnimation animation;
        
        [field: Header("BGM 名稱面板")]
        [field: SerializeField] private TextMeshProUGUI BGMNameText;
        [field: SerializeField] private CanvasGroup BGMNamePanelCanvasGroup;
        [field: SerializeField] private DoFade_CanvasGroup BGMNamePanelFadeInSettings;
        [field: SerializeField] private DoFade_CanvasGroup BGMNamePanelFadeOutSettings;

        private IEnumerator _showNameCoroutine;

        private void Awake()
        {
            if (animation is null)
            {
                throw new InvalidOperationException($"{nameof(animation)} 沒有被掛載。");
            }

            if (BGMNameText is null)
            {
                throw new InvalidOperationException($"{nameof(BGMNameText)} 沒有被掛載。");
            }

            if (BGMNamePanelCanvasGroup is null)
            {
                throw new InvalidOperationException($"{nameof(BGMNamePanelCanvasGroup)} 沒有被掛載。");
            }
        }

        private void OnDisable()
        {
            if (_showNameCoroutine is not null)
            {
                StopCoroutine(_showNameCoroutine);
                _showNameCoroutine = null;
            }
        }

        public void FadeInBGM(FadeInBGM dialogueData, Action onComplete)
        {
            StartShowName(dialogueData.FadeInBGMData.Clip.name);
            
            AudioSystem.Instance.BGMSystem.FadeInBGM(
                data: dialogueData.FadeInBGMData,
                onComplete: onComplete);
        }

        public void FadeOutBGM(FadeOutBGM dialogueData, Action onComplete)
        {
            AudioSystem.Instance.BGMSystem.FadeOutBGM(
                data: dialogueData.FadeOutBGMData,
                onComplete: onComplete);
        }

        public void PlaySFX(PlaySFX dialogueData, Action onComplete)
        {
            AudioSystem.Instance.SFXSystem.PlayOneShot(
                data: dialogueData.PlaySfxData);
            
            onComplete.Invoke();
        }

        private void StartShowName(string BGMName)
        {
            _showNameCoroutine = ShowNameCoroutine();
            StartCoroutine(_showNameCoroutine);
            return;
            
            IEnumerator ShowNameCoroutine()
            {
                var complete = false;

                BGMNameText.text = $"BGM:{BGMName}";
                
                BGMNamePanelCanvasGroup.gameObject.SetActive(true);
                
                animation.DoFade_CanvasGroup(
                    canvasGroup: BGMNamePanelCanvasGroup,
                    settings: BGMNamePanelFadeInSettings,
                    onComplete: () => complete = true);
                
                yield return new WaitUntil(() => complete);
                yield return new WaitForSeconds(5);
                
                animation.DoFade_CanvasGroup(
                    canvasGroup: BGMNamePanelCanvasGroup,
                    settings: BGMNamePanelFadeOutSettings,
                    onComplete: () =>
                    {
                        BGMNamePanelCanvasGroup.gameObject.SetActive(false);
                        
                        _showNameCoroutine = null;
                    });
            }
        }
    }
}