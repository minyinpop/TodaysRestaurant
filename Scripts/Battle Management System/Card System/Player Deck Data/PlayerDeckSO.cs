using System.Collections.Generic;
using Battle_Management_System.Card_System.Card_System;
using UnityEngine;

namespace Battle_Management_System.Card_System.Player_Deck_Data
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/New Player Deck", fileName = "New Name", order = 1)]
    internal class PlayerDeckSO : ScriptableObject
    {
        [field: SerializeField] private List<GameObject> CardList;

        public GameObject GetRandomCard()
        {
            var totalChance = 0;

            foreach (var card in CardList)
                totalChance += card.GetComponent<ICard>().GetDrawChance();
            
            var randomChance = Random.Range(1, totalChance);

            foreach (var card in CardList)
            {
                randomChance -= card.GetComponent<ICard>().GetDrawChance();

                if (randomChance <= 0)
                    return card;
            }

            return null;
        }
    }
}