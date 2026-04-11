using Audio_System.Data;
using Common.Dialogue.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Dialogue.SO.Child.Sound
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Sound/Fade Out BGM", fileName = "New Data", order = 2)]
    public sealed class FadeOutBGM : DialogueData
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool blockProcess;
                                public override bool BlockProcess => blockProcess;
        [field: SerializeField, FormerlySerializedAs("autoPass")] private bool clickToPass;
                                                                          public override bool ClickToPass => clickToPass;
        
        [field: Header("資料")]
        [field: SerializeField] private FadeOutBGMData fadeOutBGMData;
                                public FadeOutBGMData FadeOutBGMData => fadeOutBGMData;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Fade_Out_BGM;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}