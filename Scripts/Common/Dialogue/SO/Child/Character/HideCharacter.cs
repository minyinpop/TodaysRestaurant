using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.SO.Child.Character
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Character/Hide Character", fileName = "New Data")]
    public sealed class HideCharacter : DialogueData
    {
        [field: Header("Character Type")]
        [field: SerializeField] private DialogueCharacterType characterType;
                                public DialogueCharacterType CharacterType => characterType;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Hide_Character;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}