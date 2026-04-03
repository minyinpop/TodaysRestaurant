using Common.Dialogue.Data;
using Common.Dialogue.Main;
using UnityEngine;

namespace Common.Dialogue.Child.Tool
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Tool/Wait", fileName = "New Data")]
    public sealed class Wait : DialogueData
    {
        private const DialogueDataType _dialogueDataType = DialogueDataType.Wait;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}