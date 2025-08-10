using System.Collections;
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
        [field: SerializeField] private InitiativeText UpperResultTextScript;
        [field: SerializeField] private InitiativeText BottomResultTextScript;

        [field: Space(9)]
        [field: SerializeField] private InitiativeResultContent HeadsResultContentText;
        [field: SerializeField] private InitiativeResultContent TailsResultContentText;
        
        private readonly InitiativeStateMachine StateMachine = new();
        private readonly InitiativeResult InitiativeResult = new();

        private IEnumerator ShowResultTextCoroutine;

        public event System.Action OnTossResultShowFinish;

        private void OnDisable() => ClearShowResultTextCoroutine();

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
        
        private void OnCoinShowComplete() => ChangeState(new ShowResultText());
        
        private void ClearShowResultTextCoroutine()
        {
            if (ShowResultTextCoroutine is null) return;
            StopCoroutine(ShowResultTextCoroutine);
            ShowResultTextCoroutine = null;
        }
        
        #region Initiative Coin
            public void MoveCoinToReadyPoint() => CoinScript.MoveCoinToReadyPoint();
            public void MoveCoinToShowPoint() => CoinScript.MoveCoinToShowPoint();
            private void ShrinkCoinToZero() => CoinScript.ShrinkToZero();
        #endregion
        
        #region Initiative Text
            public void StartShowResultText()
            {
                ClearShowResultTextCoroutine();
                ShowResultTextCoroutine = ResultTextProcess();
                StartCoroutine(ShowResultTextCoroutine);
            }

            private IEnumerator ResultTextProcess()
            {
                ShowUpperResultText(InitiativeResult.GetResult() == InitiativeResult.ResultType.Heads ? HeadsResultContentText : TailsResultContentText);
                yield return new WaitForSeconds(1);
                ShowBottomResultText(InitiativeResult.GetResult() == InitiativeResult.ResultType.Heads ? HeadsResultContentText : TailsResultContentText);
                yield return new WaitForSeconds(2);
                HideUpperResultText();
                HideBottomResultText();
                ShrinkCoinToZero();
                yield return new WaitForSeconds(1);
                OnTossResultShowFinish?.Invoke();
            }

            private void ShowUpperResultText(InitiativeResultContent content) => UpperResultTextScript.ShowText(content.TextColor, content.Upper);
            private void ShowBottomResultText(InitiativeResultContent content) => BottomResultTextScript.ShowText(content.TextColor, content.Bottom);
            private void HideUpperResultText() => UpperResultTextScript.HideText();
            private void HideBottomResultText() => BottomResultTextScript.HideText();

            #endregion
    }
}