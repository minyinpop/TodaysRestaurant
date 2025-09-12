using Card_Battle_System.System.Child;
using Card_Battle_System.System.Main.State_Machine;
using Card_Battle_System.System.Main.State_Machine.State;
using Data.Initiative_Coin;
using Initiative_System.System.Main;
using UnityEngine;

namespace Card_Battle_System.System.Main
{
    internal sealed class CardBattleSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private ShowCardSystem ShowCardSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;
        
        [field: Header("Other System")]
        [field: SerializeField] private InitiativeSystem InitiativeSystem;
        
        private readonly StateMachine StateMachine = new();

        private void Start()
        {
            OnBattleStart();
        }
        
        #region StateMachine
            #region OnBattleStart
                private void OnBattleStart()
                {
                    StateMachine.ChangeState(new OnBattleStart(OnBattleStart_Enter, OnBattleStart_Exit));
                }

                private void OnBattleStart_Enter()
                {
                    CardPoolSystem.Refill(() =>
                    {
                        CardPoolSystem.DrawCard(6, out var cards);
                        ShowCardSystem.ShowCard(cards, () =>
                        {
                            CardPoolSystem.Refill();
                            ShowCardSystem.GetShowCards(out var showCards);
                            HandCardSystem.Add(showCards, OnInitiativeCoin);
                        });
                    });
                }
                
                private void OnBattleStart_Exit()
                {
                }
            #endregion
            
            #region OnInitiativeCoin
                private void OnInitiativeCoin()
                {
                    StateMachine.ChangeState(new OnInitiativeCoin(OnInitiativeCoin_Enter, OnInitiativeCoin_Exit));
                }

                private void OnInitiativeCoin_Enter()
                {
                    var system = Instantiate(InitiativeSystem.gameObject);
                    var systemScript = system.GetComponent<InitiativeSystem>();
                    systemScript.OnShowResultComplete += OnInitiativeSystemComplete;
                }
                
                private void OnInitiativeCoin_Exit()
                {
                }
            #endregion
        #endregion

        private void OnInitiativeSystemComplete(TossResult result)
        {
            Debug.Log(result);
        }
    }
}