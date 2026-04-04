using Animation_System.Spine;
using Common.Dialogue.Data;
using UnityEngine;

namespace Common.Dialogue.Child.Character
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Character/Play Character Animation", fileName = "New Data")]
    public sealed class PlayCharacterAnimation : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        [field: Header("Animation")]
        [field: SerializeField] private SpineAnimation[] spineAnimations;
                                public SpineAnimation[] SpineAnimations => spineAnimations;
                                
        [field: Header("Character Type")]
        [field: SerializeField] private DialogueCharacterType characterType;
                                public DialogueCharacterType CharacterType => characterType;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Play_Character_Animation;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}