using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.SO.Child.Sound
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Sound/Fade Out BGM", fileName = "New Data", order = 2)]
    public sealed class FadeOutBGM : DialogueData
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        [field: Header("資料")]
        [field: SerializeField] private DoFade_AudioSource fadeOutSettings;
                                public DoFade_AudioSource FadeOutSettings => fadeOutSettings;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Fade_Out_BGM;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}