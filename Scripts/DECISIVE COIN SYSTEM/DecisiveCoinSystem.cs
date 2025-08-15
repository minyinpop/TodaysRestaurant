using DECISIVE_COIN_SYSTEM.OBJECT;
using DECISIVE_COIN_SYSTEM.STATE_MACHINE;
using DECISIVE_COIN_SYSTEM.STATE_TYPE;
using UnityEngine;

namespace DECISIVE_COIN_SYSTEM
{
    /// <summary>
    /// 如果要使用這個系統，請直接 SetActive 就可以了。
    /// </summary>
    public class DecisiveCoinSystem : MonoBehaviour
    {
        private readonly StateMachine StateMachine = new();

        [field: SerializeField] private ScreenMask ScreenMask;
        [field: SerializeField] private DecisiveCoin DecisiveCoin;

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
            private void ShowScreenMaskState()
            {
                StateMachine.ChangeState(this, new ShowScreenMask());
            }

            public void ReadyToTossCoinState()
            {
                StateMachine.ChangeState(this, new ReadyToTossCoin());
            }

            private void ShowTossResultState()
            {
                StateMachine.ChangeState(this, new ShowTossResult());
            }
        #endregion

        #region Screen Mask
            public void ShowScreenMask(System.Action onComplete)
            {
                ScreenMask.Show(onComplete);
            }
            
            public void HideScreenMask(System.Action onComplete)
            {
                ScreenMask.Hide(onComplete);
            }
        #endregion

        #region Decisive Coin
            public void MoveToTossPoint(System.Action onComplete = null)
            {
                DecisiveCoin.MoveToTossPoint(() =>
                {
                    DecisiveCoin.SetInteractable(true);
                    onComplete?.Invoke();
                });
            }

            public void MoveToShowPoint(System.Action onComplete = null)
            {
                DecisiveCoin.MoveToShowPoint(() => onComplete?.Invoke());
            }
            
        #endregion
    }
}