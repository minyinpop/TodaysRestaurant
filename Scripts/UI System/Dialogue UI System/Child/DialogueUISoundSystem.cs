using System;
using Audio_System.Main;
using Common.Dialogue.SO.Child.Sound;
using UI_System.Message_UI_System.Main;
using UnityEngine;

namespace UI_System.Dialogue_UI_System.Child
{
    public sealed class DialogueUISoundSystem : MonoBehaviour
    {
        public void FadeInBGM(FadeInBGM dialogueData, Action onComplete)
        {
            MessageUISystem.ShowAudioUI(dialogueData.FadeInBGMData.Clip.name);
            
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

        
    }
}