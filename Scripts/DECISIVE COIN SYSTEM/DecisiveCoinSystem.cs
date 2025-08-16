using DECISIVE_COIN_SYSTEM.Data;
using DECISIVE_COIN_SYSTEM.OBJECT;
using DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DECISIVE_COIN_SYSTEM.STATE_TYPE;
using DG.Tweening;
using UnityEngine;

namespace DECISIVE_COIN_SYSTEM
{
    public class DecisiveCoinSystem : MonoBehaviour
    {
        private readonly StateMachine StateMachine = new();

        [field: Header("Object")]
        [field: SerializeField] private ScreenMask ScreenMask;
        [field: SerializeField] private DecisiveCoin DecisiveCoin;
        [field: SerializeField] private TossResultText TossResultText;

        private TossResult TossResult;

        private void Start()
        {
            DecisiveCoin.OnClicked += TossingCoinState;
        }

        private void OnEnable()
        {
            // 暫時開發寫在這裡。
            StartSystem();
        }

        private void StartSystem()
        {
            ShowScreenMaskState();
        }

        public void FinishSystem()
        {
            // TODO 結束此系統時，所發生的事情。
        }

        #region State Machine
            private void ChangeState(IState newState) => StateMachine.ChangeState(this, newState);
            private void ShowScreenMaskState() => ChangeState(new ShowScreenMask(ReadyToTossCoinState));
            private void ReadyToTossCoinState() => ChangeState(new ReadyToTossCoin(SetCoinInteractToTrue));
            private void TossingCoinState() => ChangeState(new TossingCoin(ShowTossResultState));
            private void ShowTossResultState() => ChangeState(new ShowTossResult(HideTossResultState));
            private void HideTossResultState() => ChangeState(new HideTossResult(() => Debug.Log("Toss Finished.")));
        #endregion

        #region Screen Mask
            public Tween ShowScreenMask() => ScreenMask.Show();
            public Tween HideScreenMask() => ScreenMask.Hide();
        #endregion

        #region Decisive Coin
            private void SetCoinInteractToTrue() => DecisiveCoin.SetInteractable(true);
            public Tween MoveToTossPoint() => DecisiveCoin.MoveToTossPoint();
            public Tween TossingCoin() => DecisiveCoin.Tossing(out TossResult);
            public Tween MoveToShowPoint() => DecisiveCoin.MoveToShowPoint();
            public Tween HideCoin() => DecisiveCoin.HideCoin();
        #endregion

        #region TossResultText
            public Tween ShowTossResultText() => TossResultText.ShowText(TossResult);
            public Tween HideTossResultText() => TossResultText.HideText();
        #endregion
    }
}