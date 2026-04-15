using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Dialogue.SO.Child.Background
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Background/Hide Background", fileName = "New Data")]
    public sealed class HideBackground : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool blockProcess;
                                public override bool BlockProcess => blockProcess;
        [field: SerializeField, FormerlySerializedAs("autoPass")] private bool clickToPass;
                                                                          public override bool ClickToPass => clickToPass;
        
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoColor_Image fadeOutSettings;
                                public DoColor_Image FadeOutSettings => fadeOutSettings;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Hide_Background;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}