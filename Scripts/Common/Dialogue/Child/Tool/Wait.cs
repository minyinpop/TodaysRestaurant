using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.Child.Tool
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Tool/Wait", fileName = "New Data")]
    public sealed class Wait : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
                                
        [field: Header("Time")]
        [field: SerializeField] private float waitTime;
                                public float WaitTime => waitTime;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Wait;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}