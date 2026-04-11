using Audio_System.Data;
using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.SO.Child.Sound
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Sound/SFX", fileName = "New Data", order = 3)]
    public sealed class PlaySFX : DialogueData
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        [field: Header("資料")]
        [field: SerializeField] private PlaySFXData playSFXData;
                                public PlaySFXData PlaySfxData => playSFXData;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Play_SFX;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}