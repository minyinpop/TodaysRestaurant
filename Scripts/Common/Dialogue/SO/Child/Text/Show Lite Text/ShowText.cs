using Animation_System.DOTween.Basic;
using Common.Dialogue.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Dialogue.SO.Child.Text.Show_Lite_Text
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Text/Show Lite Text", fileName = "New Data")]
    public sealed class ShowText : DialogueData
    {
        [field: Header("播放狀態")]
        [field: SerializeField] private bool blockProcess;
                                public override bool BlockProcess => blockProcess;
        [field: SerializeField, FormerlySerializedAs("autoPass")] private bool clickToPass;
                                                                          public override bool ClickToPass => clickToPass;
        
        [field: Header("角色的大頭貼")]
        [field: SerializeField] private Sprite avatarSprite;
                                public Sprite AvatarSprite => avatarSprite;
        
        [field: Header("文字設定")]
        [field: SerializeField] private DoText textSettings;
                                public DoText TextSettings => textSettings;
                                
        [field: Header("角色類型")]
        [field: SerializeField] private DialogueCharacterType characterType;
                                public DialogueCharacterType CharacterType => characterType;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.Show_Lite_Text;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}