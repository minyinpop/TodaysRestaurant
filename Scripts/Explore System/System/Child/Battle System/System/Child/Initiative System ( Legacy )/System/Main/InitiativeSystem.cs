using System;
using Common.Value;
using DG.Tweening;
using Explore_System.System.Child.Battle_System.System.Child.Initiative_System___Legacy__.Object.Initiative_Coin;
using Explore_System.System.Child.Battle_System.System.Child.Initiative_System___Legacy__.System.Child;
using Explore_System.System.Child.Battle_System.System.Child.Initiative_System___Legacy__.System.Main.State_Machine;
using Explore_System.System.Child.Battle_System.System.Child.Initiative_System___Legacy__.System.Main.State_Machine.State;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Child.Initiative_System___Legacy__.System.Main
{
    internal sealed class InitiativeSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private ScreenMaskSystem ScreenMaskSystem;
        [field: SerializeField] private TossResultTextSystem TossResultTextSystem;
        
        [field: Header("Object")]
        [field: SerializeField] private InitiativeCoin InitiativeCoin;

        private readonly StateMachine StateMachine = new();

        private TossResult TossResult = TossResult.Tails;

        public event Action<TossResult> OnShowResultComplete;

        private void Start()
        {
            OnTossStart();
        }
        
        private void OnEnable()
        {
            InitiativeCoin.OnShowTossResult += ShowTossResult;
        }
        
        private void OnDisable()
        {
            InitiativeCoin.OnShowTossResult -= ShowTossResult;
        }
        
        private void ShowTossResult(TossResult tossResult)
        {
            TossResult = tossResult;
            OnTossEnd();
        }

        #region State Machine
            #region OnTossStart
                private void OnTossStart()
                {
                    StateMachine.ChangeState(new OnTossStart(OnTossStart_Enter, OnTossStart_Exit));
                }

                private void OnTossStart_Enter()
                {
                    ScreenMaskSystem.FadeIn()
                        .OnComplete(() => InitiativeCoin.MoveToReadyParent());
                }
                
                private void OnTossStart_Exit()
                {
                }
            #endregion

            #region OnTossEnd
                private void OnTossEnd()
                {
                    StateMachine.ChangeState(new OnTossEnd(OnTossEnd_Enter, OnTossEnd_Exit));
                }
            
                private void OnTossEnd_Enter()
                {
                    TossResultTextSystem.Show(TossResult, 3, () =>
                    {
                        const float textHideDuration = 1f;
                        const float coinHideDuration = 1.5f;
                        
                        TossResultTextSystem.Hide(textHideDuration);
                        InitiativeCoin.Hide(coinHideDuration, () =>
                        {
                            ScreenMaskSystem.FadeOut()
                                .OnComplete(() => OnShowResultComplete?.Invoke(TossResult));
                        });
                    });
                }
                
                private void OnTossEnd_Exit()
                {
                }
            #endregion
        #endregion
    }
}