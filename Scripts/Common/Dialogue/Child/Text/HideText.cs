using Common.Dialogue.Data;
using Common.Dialogue.Main;
using UnityEngine;

namespace Common.Dialogue.Child.Text
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Text/Hide Text", fileName = "New Data")]
    public sealed class HideText : DialogueData
    {
        private const DialogueDataType _dialogueDataType = DialogueDataType.Hide_Text;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}