using BATTLE.OBJECT.INITIATIVE;
using BATTLE.SYSTEM.INITIATIVE.DATA;
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
        
        [field: Space(9)]
        [field: SerializeField] private CanvasScaler MainCanvasScaler;
        
        [field: Space(9)]
        [field: SerializeField] private RectTransform SpawnPoint;
        [field: SerializeField] private RectTransform ReadyPoint;
        [field: SerializeField] private RectTransform TossPoint;
        [field: SerializeField] private RectTransform ShowPoint;

        [field: Space(9)]
        [field: SerializeField] private GameObject UpperResultText;
        [field: SerializeField] private GameObject BottomResultText;
        private InitiativeText UpperResultTextScript;
        private InitiativeText BottomResultTextScript;

        [field: Space(9)]
        [field: SerializeField] private InitiativeResultContent HeadsResultContentText;
        [field: SerializeField] private InitiativeResultContent TailsResultContentText;
        
        private readonly InitiativeStateMachine StateMachine = new();
        private readonly InitiativeResult InitiativeResult = new();
        
        public void ChangeState(IInitiativeState newState)
        {
            StateMachine.ChangeState(this, newState);
        }

        public void SpawnCoin()
        {
            Coin = Instantiate(CoinPrefab, SpawnPoint.position, Quaternion.identity, SpawnPoint);
            CoinScript = Coin.GetComponent<InitiativeCoin>();
            CoinScript.Initialization(MainCanvasScaler, ReadyPoint, TossPoint, ShowPoint);
            CoinScript.OnTossComplete += OnCoinTossComplete;
            CoinScript.OnShowComplete += OnCoinShowComplete;
        }
        
        private void OnCoinTossComplete(int resultIndex)
        {
            InitiativeResult.SetResult(resultIndex);
            ChangeState(new ShowTossResult());
        }

        private void OnCoinShowComplete()
        {
            ChangeState(new ShowResultText());
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
        
        
        
        #region Initiative Text
            public void ShowResultText()
            {
                UpperResultText.SetActive(true);
                BottomResultText.SetActive(true);
                
                UpperResultTextScript = UpperResultText.GetComponent<InitiativeText>();
                BottomResultTextScript = BottomResultText.GetComponent<InitiativeText>();

                if (InitiativeResult.GetResult() == InitiativeResult.ResultType.Heads)
                {
                    UpperResultTextScript.SetText(HeadsResultContentText.TextColor, HeadsResultContentText.Upper);
                    BottomResultTextScript.SetText(HeadsResultContentText.TextColor, HeadsResultContentText.Bottom);
                }
                else
                {
                    UpperResultTextScript.SetText(TailsResultContentText.TextColor, TailsResultContentText.Upper);
                    BottomResultTextScript.SetText(TailsResultContentText.TextColor, TailsResultContentText.Bottom);
                }
            }
        #endregion
    }
}