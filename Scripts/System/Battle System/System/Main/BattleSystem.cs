using System.Battle_System.System.Child;
using System.Battle_System.System.Child.Initiative_System.System.Main;
using System.Battle_System.System.Child.Selected_Card_System.Main;
using System.Battle_System.System.Main.State_Machine;
using System.Battle_System.System.Main.State_Machine.State;
using System.Collections;
using Data.Initiative_Coin;
using Data.Player;
using UnityEngine;

namespace System.Battle_System.System.Main
{
    internal sealed class BattleSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private SelectedCardSystem SelectedCardSystem;
        [field: SerializeField] private CardPoolSystem CardPoolSystem;
        [field: SerializeField] private ShowCardSystem ShowCardSystem;
        [field: SerializeField] private HandCardSystem HandCardSystem;
        [field: SerializeField] private UseCardSystem UseCardSystem;
        [field: SerializeField] private PlayerTeamSystem PlayerTeamSystem;
        [field: SerializeField] private EnemyTeamSystem EnemyTeamSystem;
        
        [field: Header("Initiative System")]
        [field: SerializeField] private InitiativeSystem InitiativeSystem;
        private GameObject InitiativeSystemObject;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;
        
        private readonly StateMachine StateMachine = new();
        
        private TossResult TossResult = TossResult.Heads; // TODO Tails

        private IEnumerator CurrentCor;

        private bool IsEnd;

        private void Start()
        {
            OnBattleStart();
        }

        private void OnEnable()
        {
            PlayerTeamSystem.OnAllDeath += OnEnemyWin;
            EnemyTeamSystem.OnAllDeath += OnPlayerWin;
        }

        private void OnDisable()
        {
            PlayerTeamSystem.OnAllDeath -= OnEnemyWin;
            EnemyTeamSystem.OnAllDeath -= OnPlayerWin;
            if (CurrentCor is not null)
            {
                StopCoroutine(CurrentCor);
                CurrentCor = null;
            }
        }
        
        private void DrawAndShowCard(int drawNumber, Action onComplete = null)
        {
            CardPoolSystem.DrawCard(drawNumber, out var cards);
            ShowCardSystem.ShowCard(cards, () =>
            {
                CardPoolSystem.Refill();
                ShowCardSystem.GetShowCards(out var showCards);
                HandCardSystem.Add(showCards, onComplete);
            });
        }

        #region StateMachine
            #region OnBattleStart
                private void OnBattleStart()
                {
                    StateMachine.ChangeState(new OnBattleStart(
                        onEnter: () =>
                        {
                            CardPoolSystem.Refill(() =>
                            {
                                PlayerData.GetCharacterNumber(out var number);
                                number *= 2;
                                DrawAndShowCard(number, TurnManager); // OnInitiativeCoin
                            });
                        },
                        onExit: () =>
                        {
                            // Exit.
                        }));
                }
            #endregion
            
            #region OnInitiativeCoin
                private void OnInitiativeCoin()
                {
                    StateMachine.ChangeState(new OnInitiativeCoin(
                        onEnter: () =>
                        {
                            InitiativeSystemObject = Instantiate(InitiativeSystem.gameObject);
                            InitiativeSystemObject.GetComponent<InitiativeSystem>().OnShowResultComplete += result =>
                            {
                                TossResult = result;
                                TurnManager();
                            };
                        },
                        onExit: () =>
                        {
                            Destroy(InitiativeSystemObject);
                            InitiativeSystemObject = null;
                        }));
                }
            #endregion
            
            #region TurnManager
                private void TurnManager()
                {
                    CurrentCor = TurnManagerCoroutine();
                    StartCoroutine(CurrentCor);
                }

                private IEnumerator TurnManagerCoroutine()
                {
                    while (!IsEnd)
                    {
                        var playerTurnEnd = false;
                        var enemyTurnEnd = false;
                        var drawAndShowEnd = false;
                        
                        switch (TossResult)
                        {
                            case TossResult.Heads:
                            {
                                OnPlayerTurn(() =>
                                {
                                    playerTurnEnd = true;
                                    OnEnemyTurn(() =>
                                    {
                                        enemyTurnEnd = true;
                                    });
                                });
                                break;
                            }
                            case TossResult.Tails:
                            {
                                OnEnemyTurn(() =>
                                {
                                    enemyTurnEnd = true;
                                    OnPlayerTurn(() =>
                                    {
                                        playerTurnEnd = true;
                                    });
                                });
                                break;
                            }
                            default:
                            {
                                throw new ArgumentOutOfRangeException(nameof(TossResult), TossResult, null);
                            }
                        }

                        yield return new WaitUntil(() => playerTurnEnd && enemyTurnEnd);
                        PlayerData.GetCharacterNumber(out var number);
                        DrawAndShowCard(number, () => drawAndShowEnd = true);
                        yield return new WaitUntil(() => drawAndShowEnd);
                        yield return new WaitForEndOfFrame();
                    }
                }
            #endregion

            #region OnPlayerTurn
                private void OnPlayerTurn(Action onComplete = null)
                {
                    StateMachine.ChangeState(new OnPlayerTurn(
                        onEnter: () =>
                        {
                            SelectedCardSystem.OpenUI(
                            onUIOpen: () =>
                            {
                                HandCardSystem.SetCardsInteractable(true);
                            }, 
                            onUIClose: () =>
                            {
                                UseCardSystem.Use(() =>
                                {
                                    onComplete?.Invoke();
                                });
                            });
                        },
                        onExit: () =>
                        {
                            // Exit.
                        }));
                }
            #endregion
            
            #region OnEnemyTurn
                private void OnEnemyTurn(Action onComplete = null)
                {
                    StateMachine.ChangeState(new OnEnemyTurn(
                        onEnter: () =>
                        {
                            EnemyTeamSystem.Attack(() =>
                            {
                                onComplete?.Invoke();
                            });
                        },
                        onExit: () =>
                        {
                            // Exit.
                        }));
                }
            #endregion
            
            #region OnPlayerWin
                private void OnPlayerWin()
                {
                    if (CurrentCor is not null)
                    {
                        StopCoroutine(CurrentCor);
                        CurrentCor = null;
                    }

                    StateMachine.ChangeState(new OnPlayerWin(
                        onEnter: () =>
                        {
                            IsEnd = true;
                            Debug.Log("Player Win!");
                        },
                        onExit: () =>
                        {
                        }));
                }
            #endregion
            
            #region OnEnemyWin
                private void OnEnemyWin()
                {
                    if (CurrentCor is not null)
                    {
                        StopCoroutine(CurrentCor);
                        CurrentCor = null;
                    }
                    
                    StateMachine.ChangeState(new OnEnemyWin(
                        onEnter: () =>
                        {
                            IsEnd = true;
                            Debug.Log("Enemy Win!");
                        },
                        onExit: () =>
                        {
                        }));
                }
            #endregion
        #endregion
    }
}