using System.Collections.Generic;
using Battle_Management_System.Card_System.Base;
using UnityEngine;

namespace Player.Data.Equip_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Battle Deck", fileName = "New Data", order = 1)]
    internal class EquipDeckData : ScriptableObject
    {
        [field: Header("Deck")]
        [field: SerializeField] private List<GameObject> EquipCards;

        public void GetRandomCard(out GameObject outCard)
        {
            var totalChance = 0f;
            
            foreach (var card in EquipCards)
            {
                card.GetComponent<ICard>().GetDrawChance(out var chance);
                totalChance += chance;
            }
            
            var randomChance = Random.Range(0, totalChance);

            foreach (var card in EquipCards)
            {
                card.GetComponent<ICard>().GetDrawChance(out var chance);
                randomChance -= chance;

                if (randomChance > 0) continue;

                outCard = card;
                return;
            }
#if UNITY_EDITOR
            Debug.Log("Equip Deck is Empty!");
#endif
            outCard = null;
        }
    }
}