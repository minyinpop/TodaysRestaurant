using System.Collections.Generic;
using Battle_Management.Card.Base;
using UnityEngine;

namespace Player.Data.Equip_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Equip Deck", fileName = "New Equip Deck")]
    internal class EquipDeckSO : ScriptableObject
    {
        [field: Header("Equip Card Prefabs")]
        [field: SerializeField] private List<GameObject> EquipCardPrefabs;

        public void GetRandomCard(out GameObject prefab)
        {
            var totalChance = 0f;
            foreach (var equipCard in EquipCardPrefabs)
            {
                equipCard.GetComponent<ICard>().GetDrawChance(out var chance);
                totalChance += chance;
            }

            var randomChance = Random.Range(0f, totalChance);
            foreach (var equipCard in EquipCardPrefabs)
            {
                equipCard.GetComponent<ICard>().GetDrawChance(out var chance);
                randomChance -= chance;
                if (randomChance > 0f) continue;
                prefab = equipCard;
                return;
            }

            prefab = null;
        }
    }
}