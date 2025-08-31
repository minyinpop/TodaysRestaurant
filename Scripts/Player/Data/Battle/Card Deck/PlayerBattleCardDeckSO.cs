using System.Collections.Generic;
using Battle.Object.Card.Base;
using UnityEngine;

namespace Player.Data.Battle.Card_Deck
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Battle Card Deck", fileName = "New Battle Card Deck")]
    internal sealed class PlayerBattleCardDeckSO : ScriptableObject
    {
        [field: Header("Prefabs")]
        [field: SerializeField] private List<GameObject> CardPrefabList;

        public void DrawCard(out GameObject CardPrefab)
        {
            float TotalDrawChance = 0;
            foreach (GameObject Prefab in CardPrefabList)
            {
                Prefab.GetComponent<ICard>().GetDrawChance(out float DrawChance);
                TotalDrawChance += DrawChance;
            }

            float RandomDrawChance = Random.Range(0, TotalDrawChance);
            foreach (GameObject Prefab in CardPrefabList)
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