using OBJECT.BATTLE.INITIATIVE_BATTLE_COIN;
using UnityEngine;

namespace BATTLE.INITIATIVE_BATTLE_SYSTEM
{
    internal class InitiativeBattleSystem : MonoBehaviour
    {
        [field: SerializeField] private GameObject CoinPrefab;
        private GameObject Coin;
        private InitiativeBattleCoin CoinScript;

        [field: SerializeField] private RectTransform SpawnPoint;
        [field: SerializeField] private RectTransform TossPoint;
        [field: SerializeField] private RectTransform LandPoint;
        [field: SerializeField] private RectTransform ShowPoint;
        
        public void SpawnCoin()
        {
            Coin = Instantiate(CoinPrefab, SpawnPoint);
            CoinScript = Coin.GetComponent<InitiativeBattleCoin>();
            CoinScript.Initialize(TossPoint, LandPoint, ShowPoint);
        }

        public void MoveCoinToTossPoint() => CoinScript.MoveCoinToTossPoint();
    }
}