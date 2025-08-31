using System.Collections.Generic;
using Battle.Object.Card.Base;
using UnityEngine;

namespace Player.Data.Battle.Card_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Battle Card Deck", fileName = "New Battle Card Deck")]
    internal sealed class PlayerBattleCardDeckSO : ScriptableObject
    {
        [field: SerializeField] private List<GameObject> CardPrefabs;
        
        public void GetRandomCardPrefab(out GameObject CardPrefab)
        {
            var TotalDrawChance = 0f;
            foreach (var CurrentCard in CardPrefabs)
            {
                CurrentCard.GetComponent<ICard>().GetDrawChance(out var DrawChance);
                TotalDrawChance += DrawChance;
            }

            var RandomDrawChance = Random.Range(0, TotalDrawChance);
            foreach (var CurrentCard in CardPrefabs)
            {
                CurrentCard.GetComponent<ICard>().GetDrawChance(out var DrawChance);
                RandomDrawChance -= DrawChance;
                if (RandomDrawChance > 0) continue;
                CardPrefab = CurrentCard;
                return;
            }
            
            CardPrefab = null;
        }
    }
}