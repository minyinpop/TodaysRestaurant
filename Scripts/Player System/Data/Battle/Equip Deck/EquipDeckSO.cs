using System.Collections.Generic;
using Battle_System.Card_System.Base;
using UnityEngine;

namespace Player_System.Data.Battle.Equip_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player System/Equip Deck Data", fileName = "New Equip Deck Data")]
    internal sealed class EquipDeckSO : ScriptableObject
    {
        [field: Header("Equip Card Prefabs")]
        [field: SerializeField] private List<GameObject> CardPrefabs;

        public void GetRandomCard(out GameObject CardPrefab)
        {
            float TotalDrawChance = 0;
            foreach (GameObject Prefab in CardPrefabs)
            {
                Prefab.GetComponent<ICard>().GetDrawChance(out float DrawChance);
                TotalDrawChance += DrawChance;
            }
            
            float RandomDrawChance = Random.Range(0, TotalDrawChance);
            foreach (GameObject Prefab in CardPrefabs)
            {
                Prefab.GetComponent<ICard>().GetDrawChance(out float DrawChance);
                RandomDrawChance -= DrawChance;

                if (RandomDrawChance > 0) continue;
                
                CardPrefab = Prefab;
                return;
            }
            
            CardPrefab = null;
        }
    }
}