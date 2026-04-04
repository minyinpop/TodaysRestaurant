using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.Child.Title
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Title/Hide Title", fileName = "New Data")]
    public sealed class HideTitle : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Hide_Title;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}