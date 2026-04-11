using Audio_System.Data;
using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.SO.Child.Sound
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Sound/Fade In BGM", fileName = "New Data", order = 1)]
    public sealed class FadeInBGM : DialogueData
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        [field: Header("資料")]
        [field: SerializeField] private PlayBGMData playBGMData;
                                public PlayBGMData PlayBGMData => playBGMData;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Fade_In_BGM;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}