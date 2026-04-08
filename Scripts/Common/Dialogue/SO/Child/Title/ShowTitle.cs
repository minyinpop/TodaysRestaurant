using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.SO.Child.Title
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Title/Show Title", fileName = "New Data")]
    public sealed class ShowTitle : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        [field: Header("Title Settings")]
        [field: SerializeField] private DoText titleSettings;
                                public DoText TitleSettings => titleSettings;
        
        [field: Header("Subtitle Settings")]
        [field: SerializeField] private DoText subtitleSettings;
                                public DoText SubtitleSettings => subtitleSettings;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Show_Title;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}