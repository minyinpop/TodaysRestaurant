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
    internal class DecisiveCoinSystem : MonoBehaviour
    {
        private StateMachine StateMachine = new();

        [field: SerializeField] private ScreenMask ScreenMask;
        [field: SerializeField] private DecisiveCoin DecisiveCoin;

        private void OnEnable()
        {
            // 暫時開發寫在這裡。
            StartSystem();
        }

        public void StartSystem()
        {
            ShowScreenMaskState();
        }

        public void FinishSystem()
        {
            // TODO 結束此系統時，所發生的事情。
        }

        #region State Machine
            private void ShowScreenMaskState()
            {
                StateMachine.ChangeState(this, new ShowScreenMask());
            }

            private void ReadyToTossCoinState()
            {
                StateMachine.ChangeState(this, new ReadyToTossCoin());
            }
        #endregion

        #region Screen Mask
            public void ShowScreenMask()
            {
                ScreenMask.Show(ReadyToTossCoinState);
            }
            
            public void HideScreenMask()
            {
                ScreenMask.Hide();
            }
        #endregion

        #region Decisive Coin
            public void MoveToTossPoint()
            {
                DecisiveCoin.MoveToTossPoint();
            }
        #endregion
    }
}