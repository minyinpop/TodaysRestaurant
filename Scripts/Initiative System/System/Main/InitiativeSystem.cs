using Data.Initiative_Coin;
using DG.Tweening;
using Initiative_System.Object.Initiative_Coin;
using Initiative_System.System.Child;
using Initiative_System.System.Main.State_Machine;
using Initiative_System.System.Main.State_Machine.State;
using UnityEngine;

namespace Initiative_System.System.Main
{
    internal sealed class InitiativeSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private ScreenMaskSystem ScreenMaskSystem;
        [field: SerializeField] private TossResultTextSystem TossResultTextSystem;
        
        [field: Header("Object")]
        [field: SerializeField] private InitiativeCoin InitiativeCoin;

        private readonly StateMachine StateMachine = new();

        private TossResult TossResult = TossResult.Null;

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
                                .OnComplete(() => Debug.Log("Initiative System End"));
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