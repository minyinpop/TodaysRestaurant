using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.SO.Child.Tool
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Tool/Wait", fileName = "New Data")]
    public sealed class Wait : DialogueData
    {
        [field: Header("時間")]
        [field: SerializeField] private float waitTime;
                                public float WaitTime => waitTime;
        
        public override bool BlockProcess => true;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Wait;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}