using Common.Value.Type;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.Object.Card
{
    public abstract class CardSO : ScriptableObject
    {
        [field: Header("主資料 - 卡片類型")]
        [field: SerializeField] private CardType cardType;
                                public CardType CardType => cardType;
                                
        [field: Header("主資料 - 卡片預製件")]
        [field: SerializeField] private Card card;
                                public Card Card => card;

        [field: Header("主資料 - 卡片資料預製件")]
        [field: SerializeField] private GameObject cardInformationPrefab;
                                public GameObject CardInformationPrefab => cardInformationPrefab;
    }
}