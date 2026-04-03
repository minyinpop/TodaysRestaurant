using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.Child.Background.Hide
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Background/Hide Background", fileName = "New Data")]
    public sealed class HideBackground : DialogueData
    {
        [field: Header("Animation Settings")]
        [field: SerializeField] private DoColor_Image fadeOutSettings;
                                public DoColor_Image FadeOutSettings => fadeOutSettings;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Hide_Background;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}