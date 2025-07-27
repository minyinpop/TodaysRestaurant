using BATTLE.OBJECT.INITIATIVE_COIN;
using BATTLE.SYSTEM.INITIATIVE.STATE_MACHINE;
using UnityEngine;

namespace BATTLE.SYSTEM.INITIATIVE
{
    internal class InitiativeSystem : MonoBehaviour
    {
        [field: SerializeField] private GameObject CoinPrefab;
        private GameObject Coin;
        private InitiativeCoin CoinScript;

        [field: SerializeField] private RectTransform SpawnPoint;
        [field: SerializeField] private RectTransform TossPoint;
        [field: SerializeField] private RectTransform ShowPoint;
        
        private readonly InitiativeStateMachine StateMachine = new();

        public void ChangeState(IInitiativeState newState)
        {
            StateMachine.ChangeState(this, newState);
        }

        public void SpawnCoin()
        {
            Coin = Instantiate(CoinPrefab, SpawnPoint.position, Quaternion.identity, SpawnPoint);
            CoinScript = Coin.GetComponent<InitiativeCoin>();
        }
        
        
        
        #region Initiative Coin
            public void MoveCoinToTossPoint()
            {
                CoinScript.MoveCoinToTossPoint(TossPoint);
            }
            
            public void MoveCoinToShowPoint()
            {
                CoinScript.MoveCoinToShowPoint(ShowPoint);
            }
        #endregion
    }
}