using Audio_System.Data;
using Common.Dialogue.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Dialogue.SO.Child.Sound
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Sound/Fade In BGM", fileName = "New Data", order = 1)]
    public sealed class FadeInBGM : DialogueData
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool blockProcess;
                                public override bool BlockProcess => blockProcess;
        [field: SerializeField, FormerlySerializedAs("autoPass")] private bool clickToPass;
                                                                          public override bool ClickToPass => clickToPass;
        
        [field: Header("資料")]
        [field: SerializeField] private FadeInBGMData fadeInBGMData;
                                public FadeInBGMData FadeInBGMData => fadeInBGMData;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Fade_In_BGM;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}