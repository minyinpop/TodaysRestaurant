using BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.DATA;
using UnityEngine;
using UnityEngine.UI;

namespace BATTLE.CARD_SYSTEM.CARD.CARD_SKIN_SYSTEM
{
    internal class CardSkinSystem : MonoBehaviour
    {
        [field: Header("Component")]
        // [field: SerializeField] private 
        [field: SerializeField] private Image FrontImage;
        [field: SerializeField] private Image BackImage;
        
        [field: Header("Card Data")]
        [field: SerializeField] private BattleCardSO BattleCardSO;
        
        public void SetFrontImage() => FrontImage.sprite = BattleCardSO.GetCardFrontSprite();
        public void SetBackImage() => BackImage.sprite = BattleCardSO.GetCardBackSprite();
    }
}