using System.Collections.Generic;
using Battle_System.Object.Card.Base;
using UnityEngine;

namespace Player.Data.Battle_Card_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Battle Card Deck", fileName = "New Name")]
    internal sealed class BattleCardDeckSO : ScriptableObject
    {
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