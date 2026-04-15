using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.SO.Child.Title
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Title/Hide Title", fileName = "New Data")]
    public sealed class HideTitle : DialogueData
    {
        private const DialogueDataType _dialogueDataType = DialogueDataType.Hide_Title;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}