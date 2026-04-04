using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.Child.Background
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Background/Show Background", fileName = "New Data")]
    public sealed class ShowBackground : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        [field: Header("Image")]
        [field: SerializeField] private Sprite backgroundImage;
                                public Sprite BackgroundImage => backgroundImage;

        [field: Header("Animation Settings")]
        [field: SerializeField] private DoColor_Image fadeInSettings;
                                public DoColor_Image FadeInSettings => fadeInSettings;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Show_Background;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}