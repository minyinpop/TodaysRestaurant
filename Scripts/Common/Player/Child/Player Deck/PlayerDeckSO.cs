using System.Collections.Generic;
using System.Linq;
using Battle_System.Object.Card;
using Common.Value.Type;
using UnityEngine;

namespace Common.Player.Child.Player_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Deck", fileName = "New Data")]
    internal sealed class PlayerDeckSO : ScriptableObject
    {
        [field: SerializeField] private List<GameObject> CardPrefabs;

        #region Deck
            public void Set(List<GameObject> cardPrefabs)
            {
                CardPrefabs = cardPrefabs.ToList();
            }

            public void Get(out List<GameObject> cardPrefabs)
            {
                cardPrefabs = CardPrefabs;
            }
            
            public void Remove(CardType targetType)
            {
                for (var i = 0; i < CardPrefabs.Count; i++)
                {
                    var cardPrefab = CardPrefabs[i];
                    cardPrefab.GetComponent<ICard>().GetCardType(out var type);
                    if (type != targetType) continue;
                    CardPrefabs.Remove(cardPrefab);
                }
            }
        #endregion

        #region Card
            public bool GetRandomCard(out GameObject prefab)
            {
                var totalDrawChance = 0f;
                foreach (var cardPrefab in CardPrefabs)
                {
                    cardPrefab.GetComponent<ICard>().GetDrawChance(out var drawChance);
                    totalDrawChance += drawChance;
                }
                
                var randomDrawChance = UnityEngine.Random.Range(0, totalDrawChance);
                foreach (var cardPrefab in CardPrefabs)
                {
                    cardPrefab.GetComponent<ICard>().GetDrawChance(out var drawChance);
                    randomDrawChance -= drawChance;
                    if (randomDrawChance > 0) continue;
                    prefab = cardPrefab;
                    return true;
                }
                
                prefab = null;
                return false;
            }
        #endregion
    }
}