using System.Collections.Generic;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM.PLAYER_DECK
{
    [CreateAssetMenu(menuName = "Minyinpop/Player Deck", fileName = "RENAME", order = 1)]
    internal class PlayerDeckSO : ScriptableObject
    {
        [field: Header("Card Prefab List")]
        [field: SerializeField] private List<GameObject> PlayerDeckList { get; set; }
        
        public GameObject GetRandomCard() => PlayerDeckList[Random.Range(0, PlayerDeckList.Count)];
    }
}