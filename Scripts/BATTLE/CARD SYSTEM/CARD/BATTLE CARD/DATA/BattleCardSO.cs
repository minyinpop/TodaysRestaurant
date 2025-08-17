using System.Collections.Generic;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.CARD.BATTLE_CARD.DATA
{
    [CreateAssetMenu(menuName = "Minyinpop/Battle Card", fileName = "RENAME", order = 1)]
    internal class BattleCardSO : ScriptableObject
    {
        [field: Header("Card Sprite")]
        [field: SerializeField] private Sprite CardFrontSprite;
        [field: SerializeField] private List<Sprite> CardBackSpriteList;
        
        public Sprite GetCardFrontSprite() => CardFrontSprite;
        public Sprite GetCardBackSprite() => CardBackSpriteList[Random.Range(0, CardBackSpriteList.Count)];
    }
}