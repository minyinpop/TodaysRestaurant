using Common.Dialogue.Data;
using Spine.Unity;
using UnityEngine;

namespace Common.Dialogue.SO.Child.Character
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Character/Show Character", fileName = "New Data")]
    public sealed class ShowCharacter : DialogueData
    {
        [field: Header("Prefab")]
        [field: SerializeField] private SkeletonGraphic characterGraphic;
                                public SkeletonGraphic CharacterGraphic => characterGraphic;
        
        [field: Header("Character Type")]
        [field: SerializeField] private DialogueCharacterType characterType;
                                public DialogueCharacterType CharacterType => characterType;
                                
        [field: Header("Position Type")]
        [field: SerializeField] private DialogueCharacterPositionType positionType;
                                public DialogueCharacterPositionType PositionType => positionType;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Show_Character;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}