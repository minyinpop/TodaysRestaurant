using System.Card_Battle_System.System.Child;
using System.Card_Battle_System.System.Main.State_Machine;
using System.Card_Battle_System.System.Main.State_Machine.State;
using System.Initiative_System.System.Main;
using Data.Initiative_Coin;
using UnityEngine;

namespace System.Card_Battle_System.System.Main
{
    internal sealed class CardBattleSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private ShowCardSystem ShowCardSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;
        
        [field: Header("Other System")]
        [field: SerializeField] private InitiativeSystem InitiativeSystem;
        private GameObject InitiativeSystemObject;
        
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
                    InitiativeSystemObject = Instantiate(InitiativeSystem.gameObject);
                    InitiativeSystemObject.GetComponent<InitiativeSystem>().OnShowResultComplete += result =>
                    {
                        switch (result)
                        {
                            case TossResult.Heads:
                            {
                                OnPlayerTurn();
                                break;
                            }
                            case TossResult.Tails:
                            {
                                OnEnemyTurn();
                                break;
                            }
                            default:
                            {
                                throw new ArgumentOutOfRangeException(nameof(result), result, null);
                            }
                        }
                    };
                }
                
                private void OnInitiativeCoin_Exit()
                {
                    Destroy(InitiativeSystemObject);
                    InitiativeSystemObject = null;
                }
            #endregion

            #region OnPlayerTurn
                private void OnPlayerTurn()
                {
                    StateMachine.ChangeState(new OnPlayerTurn(OnPlayerTurn_Enter, OnPlayerTurn_Exit));
                }
                
                private void OnPlayerTurn_Enter()
                {
                    Debug.Log("Player's turn");
                }
                
                private void OnPlayerTurn_Exit()
                {
                }
            #endregion
            
            #region OnEnemyTurn
                private void OnEnemyTurn()
                {
                    StateMachine.ChangeState(new OnEnemyTurn(OnEnemyTurn_Enter, OnEnemyTurn_Exit));
                }
                
                private void OnEnemyTurn_Enter()
                {
                    Debug.Log("Enemy's turn");
                }
                
                private void OnEnemyTurn_Exit()
                {
                }
            #endregion
        #endregion
    }
}