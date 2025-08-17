using System.Collections.Generic;
using UnityEngine;

namespace BATTLE.CARD_SYSTEM.MANAGER.CARD_POOL_SYSTEM.DATA
{
    [CreateAssetMenu(menuName = "Minyinpop/Player Deck", fileName = "RENAME", order = 1)]
    internal class PlayerDeckSO : ScriptableObject
    {
        public List<GameObject> PlayerDeckList;
        
        public GameObject GetRandomCard() => PlayerDeckList[Random.Range(0, PlayerDeckList.Count)];
    }
}