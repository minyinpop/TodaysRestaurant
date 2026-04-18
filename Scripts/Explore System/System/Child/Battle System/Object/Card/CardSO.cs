using Audio_System.Data;
using Common.Value.Type;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Card
{
    public abstract class CardSO : ScriptableObject
    {
        [field: Header("父資料 - 卡片類型")]
        [field: SerializeField] private CardType cardType;
                                public CardType CardType => cardType;
                                
        [field: Header("父資料 - 卡片預製件")]
        [field: SerializeField] private Card card;
                                public Card Card => card;

        [field: Header("父資料 - 卡片資料預製件")]
        [field: SerializeField] private GameObject cardInformationPrefab;
                                public GameObject CardInformationPrefab => cardInformationPrefab;
        
        [field: Header("父資料 - 卡片資料預製件")]
        [field: SerializeField] private PlaySFXData useSFXData;
                                public PlaySFXData UseSFXData => useSFXData;
                                
        [field: Header("父資料 - 卡片攻擊特效")]
        [field: SerializeField] private ParticleSystem attackVFX;
                                public ParticleSystem AttackVFX => attackVFX;
    }
}