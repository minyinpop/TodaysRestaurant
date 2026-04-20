using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Dialogue.SO.Child.Text.Show_Full_Text
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Text/Show Full Text", fileName = "New Data")]
    public sealed class ShowText : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool blockProcess;
                                public override bool BlockProcess => blockProcess;
        [field: SerializeField, FormerlySerializedAs("autoPass")] private bool clickToPass;
                                                                          public override bool ClickToPass => clickToPass;
        
        [field: Header("Text Settings")]
        [field: SerializeField] private DoText textSettings;
                                public DoText TextSettings => textSettings;
                                
        [field: Header("Character Type")]
        [field: SerializeField] private DialogueCharacterType characterType;
                                public DialogueCharacterType CharacterType => characterType;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Show_Full_Text;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}