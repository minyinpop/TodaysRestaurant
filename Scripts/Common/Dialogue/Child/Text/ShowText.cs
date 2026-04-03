using Common.Dialogue.Data;
using Common.Dialogue.Main;
using UnityEngine;

namespace Common.Dialogue.Child.Text
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Text/Show Text", fileName = "New Data")]
    public sealed class ShowText : DialogueData
    {
        [field: SerializeField] private string characterName;
                                public string CharacterName => characterName;
        [field: SerializeField, TextArea] private string text;
                                          public string Text => text;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Show_Text;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}