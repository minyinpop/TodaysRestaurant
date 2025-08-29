using System.Collections.Generic;
using Battle.Card.Base;
using UnityEngine;

namespace Player.Data.Equip_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Equip Deck", fileName = "New Name")]
    internal sealed class EquipDeckSO : ScriptableObject
    {
        [field: Header("Card Prefabs")]
        [field: SerializeField] private List<GameObject> EquipCards;

        public void GetRandomCard(out GameObject Card)
        {
            var totalDrawChance = 0f;

            foreach (GameObject card in EquipCards)
            {
                card.GetComponent<ICard>().GetDrawChance(out float chance);
                totalDrawChance += chance;
            }
            
            var randomDrawChance = Random.Range(0f, totalDrawChance);

            foreach (GameObject card in EquipCards)
            {
                card.GetComponent<ICard>().GetDrawChance(out float chance);
                randomDrawChance -= chance;
                
                if (randomDrawChance <= 0f)
                {
                    Card = card;
                    return;
                }
            }

            Card = null;
        }
    }
}