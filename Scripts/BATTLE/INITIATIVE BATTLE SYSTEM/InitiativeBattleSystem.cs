using OBJECT.BATTLE.INITIATIVE_BATTLE_COIN;
using UnityEngine;

namespace BATTLE.INITIATIVE_BATTLE_SYSTEM
{
    internal class InitiativeBattleSystem : MonoBehaviour
    {
        [field: SerializeField] private GameObject CoinPrefab;
        private GameObject Coin;
        private InitiativeBattleCoin CoinScript;
        
        public void SpawnCoin()
        {
            Debug.Log("Spawn Initiative Battle Coin.");
        }
    }
}