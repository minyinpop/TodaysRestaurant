using DECISIVE_COIN_SYSTEM.Data;
using DECISIVE_COIN_SYSTEM.OBJECT;
using DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DECISIVE_COIN_SYSTEM.STATE_TYPE;
using DG.Tweening;
using UnityEngine;

namespace DECISIVE_COIN_SYSTEM
{
    /// <summary>
    /// 如果要使用這個系統，請直接 SetActive 就可以了。
    /// </summary>
    public class DecisiveCoinSystem : MonoBehaviour
    {
        private readonly StateMachine StateMachine = new();

        [field: Header("Object")]
        [field: SerializeField] private ScreenMask ScreenMask;
        [field: SerializeField] private DecisiveCoin DecisiveCoin;
        [field: SerializeField] private TossResultText TossResultText;

        private readonly TossResult TossResult = new();

        private void Start()
        {
            DecisiveCoin.OnTossComplete += ShowTossResultState;
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
            private void ShowScreenMaskState() => StateMachine.ChangeState(this, new ShowScreenMask());
            public void ReadyToTossCoinState() => StateMachine.ChangeState(this, new ReadyToTossCoin());
            private void ShowTossResultState() => StateMachine.ChangeState(this, new ShowTossResult());
            public void HideTossResultState() => StateMachine.ChangeState(this, new HideTossResult());
        #endregion

        #region Screen Mask
            public Tween ShowScreenMask() => ScreenMask.Show();
            public Tween HideScreenMask() => ScreenMask.Hide();
        #endregion

        #region Decisive Coin
            public void SetCoinInteractToTrue() => DecisiveCoin.SetInteractable(true);
            public Tween MoveToTossPoint() => DecisiveCoin.MoveToTossPoint();
            public Tween MoveToShowPoint() => DecisiveCoin.MoveToShowPoint();
            public Tween HideCoin() => DecisiveCoin.HideCoin();
        #endregion

        #region TossResultText
            public Tween ShowTossResultText() => TossResultText.ShowText();
            public Tween HideTossResultText() => TossResultText.HideText();
        #endregion
    }
}