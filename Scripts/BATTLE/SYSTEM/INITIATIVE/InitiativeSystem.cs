using BATTLE.OBJECT.INITIATIVE_COIN;
using BATTLE.SYSTEM.INITIATIVE.STATE_MACHINE;
using BATTLE.SYSTEM.INITIATIVE.STATE_MACHINE.STATE;
using UnityEngine;
using UnityEngine.UI;

namespace BATTLE.SYSTEM.INITIATIVE
{
    internal class InitiativeSystem : MonoBehaviour
    {
        [field: SerializeField] private GameObject CoinPrefab;
        private GameObject Coin;
        private InitiativeCoin CoinScript;
        
        [field: SerializeField] private CanvasScaler MainCanvasScaler;
        [field: SerializeField] private RectTransform SpawnPoint;
        [field: SerializeField] private RectTransform ReadyPoint;
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
            CoinScript.Initialization(MainCanvasScaler, ReadyPoint, TossPoint, ShowPoint);
            CoinScript.OnTossComplete += () => { ChangeState(new ShowTossResult()); };
        }
        
        
        
        #region Initiative Coin
            public void MoveCoinToReadyPoint()
            {
                CoinScript.MoveCoinToReadyPoint();
            }
            
            public void MoveCoinToShowPoint()
            {
                CoinScript.MoveCoinToShowPoint();
            }
        
        #endregion
    }
}