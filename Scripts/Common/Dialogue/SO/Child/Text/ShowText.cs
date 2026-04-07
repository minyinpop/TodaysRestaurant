using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.Child.Text
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Text/Show Text", fileName = "New Data")]
    public sealed class ShowText : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        [field: Header("Text Settings")]
        [field: SerializeField] private DoText textSettings;
                                public DoText TextSettings => textSettings;
                                
        [field: Header("Character Type")]
        [field: SerializeField] private DialogueCharacterType characterType;
                                public DialogueCharacterType CharacterType => characterType;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Show_Text;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}