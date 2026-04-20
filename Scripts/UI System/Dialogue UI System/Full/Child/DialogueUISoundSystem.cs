using System;
using Audio_System.Data;
using Audio_System.Main;
using Common.Dialogue.SO.Child.Sound;
using UnityEngine;

namespace UI_System.Dialogue_UI_System.Full.Child
{
    public sealed class DialogueUISoundSystem : MonoBehaviour
    {
        public void FadeInBGM(FadeInBGM dialogueData, Action onComplete)
        {
            switch (dialogueData.FadeInBGMData.AudioSourceType)
            {
                case AudioSourceType.Common_BGM:
                {
                    AudioSystem.Instance.CommonBGM.FadeInBGM(
                        data: dialogueData.FadeInBGMData,
                        onComplete: onComplete);
                    break;
                }
                default:
                {
                    Debug.Log($"{nameof(FadeInBGM)} 接收到沒被登記的 {nameof(AudioSourceType)}。");
                    break;
                }
            }
        }

        public void FadeOutBGM(FadeOutBGM dialogueData, Action onComplete)
        {
            switch (dialogueData.FadeOutBGMData.AudioSourceType)
            {
                case AudioSourceType.Common_BGM:
                {
                    AudioSystem.Instance.CommonBGM.FadeOutBGM(
                        data: dialogueData.FadeOutBGMData,
                        onComplete: onComplete);
                    break;
                }
                default:
                {
                    Debug.Log($"{nameof(FadeOutBGM)} 接收到沒被登記的 {nameof(AudioSourceType)}。");
                    break;
                }
            }

        }

        public void PlaySFX(PlaySFX dialogueData, Action onComplete)
        {
            switch (dialogueData.PlaySfxData.AudioSourceType)
            {
                case AudioSourceType.UI_SFX:
                {
                    AudioSystem.Instance.UISFX.PlayOneShot(
                        data: dialogueData.PlaySfxData);
                    break;
                }
                case AudioSourceType.Footstep_SFX:
                {
                    AudioSystem.Instance.FootstepSFX.PlayOneShot(
                        data: dialogueData.PlaySfxData);
                    break;
                }
                case AudioSourceType.Interact_SFX:
                {
                    AudioSystem.Instance.InteractSFX.PlayOneShot(
                        data: dialogueData.PlaySfxData);
                    break;
                }
                case AudioSourceType.Other_SFX:
                {
                    AudioSystem.Instance.OtherSFX.PlayOneShot(
                        data: dialogueData.PlaySfxData);
                    break;
                }
                default:
                {
                    Debug.Log($"{nameof(PlaySFX)} 接收到沒被登記的 {nameof(AudioSourceType)}。");
                    break;
                }
            }
            
            onComplete.Invoke();
        }
    }
}