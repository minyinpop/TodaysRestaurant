using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.Child.Text
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Text/Hide Text", fileName = "New Data")]
    public sealed class HideText : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Hide_Text;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}