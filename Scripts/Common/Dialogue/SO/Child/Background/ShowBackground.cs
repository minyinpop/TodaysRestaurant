using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Dialogue.SO.Child.Background
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Background/Show Background", fileName = "New Data")]
    public sealed class ShowBackground : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool blockProcess;
                                public override bool BlockProcess => blockProcess;
        [field: SerializeField, FormerlySerializedAs("autoPass")] private bool clickToPass;
                                                                          public override bool ClickToPass => clickToPass;
        
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