using System.Collections.Generic;
using System.Linq;
using Common.Value.Type;
using Explore_System.System.Child.Battle_System.Object.Card;
using Explore_System.System.Child.Battle_System.Object.Card.Battle;
using UnityEngine;

namespace Common.Player.Child.Player_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Deck", fileName = "New Data")]
    internal sealed class PlayerDeckSO : ScriptableObject
    {
        [field: Header("Deck")]
        [field: SerializeField] private List<CardSO> cardsData;
                                public IReadOnlyList<CardSO> CardsData => cardsData;

        #region Deck
            public void Set(List<CardSO> cardPrefabs)
            {
                cardsData = cardPrefabs.ToList();
            }

            public void Remove(CardType targetType)
            {
                for (var i = 0; i < CardsData.Count; i++)
                {
                    if (CardsData[i].CardType != targetType)
                    {
                        continue;
                    }
                    
                    cardsData.Remove(CardsData[i]);
                }
            }
        #endregion

        #region Card
            public bool GetRandomCard(out GameObject prefab)
            {
                var totalDrawChance = 0f;
                
                foreach (var card in CardsData)
                {
                    if (card is not BattleCardSO battleCardData)
                    {
                        Debug.Log($"在 {nameof(PlayerDeckSO)} 裡搜尋到了不是 {nameof(BattleCard)} 的 {nameof(Card)}");
                        continue;
                    }
                    
                    totalDrawChance += battleCardData.DrawChance;
                }
                
                var randomDrawChance = Random.Range(0, totalDrawChance);
                
                foreach (var cardData in CardsData)
                {
                    if (cardData is not BattleCardSO battleCardData)
                    {
                        Debug.Log($"在 {nameof(PlayerDeckSO)} 裡搜尋到了不是 {nameof(BattleCard)} 的 {nameof(Card)}");
                        continue;
                    }
                    
                    randomDrawChance -= battleCardData.DrawChance;
                    
                    if (randomDrawChance > 0)
                    {
                        continue;
                    }
                    
                    prefab = cardData.Card.gameObject;
                    return true;
                }
                
                prefab = null;
                return false;
            }
        #endregion
    }
}