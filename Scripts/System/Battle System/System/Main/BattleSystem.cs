using System.Battle_System.Object.Mob.Type.Character.Base;
using System.Battle_System.System.Child;
using System.Battle_System.System.Child.Initiative_System.System.Main;
using System.Battle_System.System.Child.Selected_Card_System.Main;
using System.Battle_System.System.Main.State_Machine;
using System.Battle_System.System.Main.State_Machine.State;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data.General;
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
        [field: SerializeField] private CharacterTeamSystem characterTeamSystem;
        [field: SerializeField] private EnemyTeamSystem EnemyTeamSystem;
        
        [field: Header("Initiative System")]
        [field: SerializeField] private InitiativeSystem InitiativeSystem;
        private GameObject InitiativeSystemObject;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;
        
        private readonly StateMachine StateMachine = new();
        
        private TossResult TossResult = TossResult.Heads; // TODO Tails

        private IEnumerator TurnCor;
        private IEnumerator AttackCor;
        private IEnumerator CharacterDeathCor;

        private bool IsEnd;

        private void Start()
        {
            OnBattleStart();
        }

        private void OnEnable()
        {
            CharacterBase.RecycleCard += OnCharacterDead;
        }

        private void OnDisable()
        {
            CharacterBase.RecycleCard -= OnCharacterDead;
            if (TurnCor is not null)
            {
                StopCoroutine(TurnCor);
                TurnCor = null;
            }

            if (AttackCor is not null)
            {
                StopCoroutine(AttackCor);
                AttackCor = null;
            }
            
            if (CharacterDeathCor is not null)
            {
                StopCoroutine(CharacterDeathCor);
                CharacterDeathCor = null;
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

        private void OnCharacterDead(List<CardType> cardTypes, Action onComplete)
        {
            CharacterDeathCor = RecycleCardCoroutine();
            StartCoroutine(CharacterDeathCor);
            return;

            IEnumerator RecycleCardCoroutine()
            {
                var completes = new List<bool>();
                for (var i = 0; i < cardTypes.Count; i++)
                {
                    var index = i;
                    var type = cardTypes[index];
                    completes.Add(false);
                    
                    var CardPoolRecycleComplete = false;
                    var HandCardRecycleComplete = false;
                    var UseCardRecycleComplete = false;
                    
                    CardPoolSystem.RecycleCard(type, 
                        onComplete:() =>
                        {
                            CardPoolRecycleComplete = true;
                        });
                    
                    HandCardSystem.RecycleCard(type,
                        onComplete: () =>
                        {
                            HandCardRecycleComplete = true;
                        });
                    
                    UseCardSystem.RecycleCard(type,
                        onComplete: () =>
                        {
                            UseCardRecycleComplete = true;
                        });
                    
                    yield return new WaitUntil(() => CardPoolRecycleComplete && HandCardRecycleComplete && UseCardRecycleComplete);
                    completes[index] = true;
                }
                
                yield return new WaitUntil(() => completes.All(c => c));
                var CardPoolRefillComplete = false;
                
                CardPoolSystem.Refill(
                    onComplete: () =>
                    {
                        CardPoolRefillComplete = true;
                    });
                
                yield return new WaitUntil(() => CardPoolRefillComplete);
                onComplete?.Invoke();
            }
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
                                number *= 1;
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
                    TurnCor = TurnManagerCoroutine();
                    StartCoroutine(TurnCor);
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
                                OnPlayerTurn(
                                    haveEnemyAlive: () =>
                                    {
                                        playerTurnEnd = true;
                                        OnEnemyTurn(
                                            haveCharacterAlive: () =>
                                            {
                                                enemyTurnEnd = true;
                                            },
                                            characterAllDead: () =>
                                            {
                                                StopCoroutine(TurnCor);
                                                TurnCor = null;
                                                OnEnemyWin();
                                            });
                                    },
                                    enemyAllDeath: () =>
                                    {
                                        StopCoroutine(TurnCor);
                                        TurnCor = null;
                                        OnPlayerWin();
                                    });
                                break;
                            }
                            case TossResult.Tails:
                            {
                                OnEnemyTurn(
                                    haveCharacterAlive: () =>
                                    {
                                        enemyTurnEnd = true;
                                        OnPlayerTurn(
                                            haveEnemyAlive: () =>
                                            {
                                                playerTurnEnd = true;
                                            },
                                            enemyAllDeath: () =>
                                            {
                                                StopCoroutine(TurnCor);
                                                TurnCor = null;
                                                OnPlayerWin();
                                            });
                                    },
                                    characterAllDead: () =>
                                    {
                                        StopCoroutine(TurnCor);
                                        TurnCor = null;
                                        OnEnemyWin();
                                    });
                                break;
                            }
                        }
                        
                        yield return new WaitUntil(() => playerTurnEnd && enemyTurnEnd);
                        PlayerData.GetCharacterNumber(out var number);
                        DrawAndShowCard(number, () => drawAndShowEnd = true);
                        yield return new WaitUntil(() => drawAndShowEnd);
                    }
                }
            #endregion

            #region OnPlayerTurn
                private void OnPlayerTurn(Action haveEnemyAlive, Action enemyAllDeath)
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
                                    AttackCor = UseCardCoroutine();
                                    StartCoroutine(AttackCor);
                                    return;

                                    IEnumerator UseCardCoroutine()
                                    {
                                        var canContinue = true;
                                        var haveAnyEnemyAlive = false;
                                        while (canContinue)
                                        {
                                            var onUseComplete = false;
                                            UseCardSystem.Use(
                                                haveEnemyAlive: hasCards =>
                                                {
                                                    canContinue = hasCards;
                                                    onUseComplete = true;
                                                    haveAnyEnemyAlive = true;
                                                },
                                                enemyAllDeath: () =>
                                                {
                                                    canContinue = false;
                                                    onUseComplete = true;
                                                    enemyAllDeath?.Invoke();
                                                    StopCoroutine(AttackCor);
                                                    AttackCor = null;
                                                });
                                            yield return new WaitUntil(() => onUseComplete);
                                        }

                                        if (haveAnyEnemyAlive)
                                            haveEnemyAlive?.Invoke();
                                        AttackCor = null;
                                    }
                                });
                        },
                        onExit: () =>
                        {
                            // Exit.
                        }));
                }
            #endregion
            
            #region OnEnemyTurn
                private void OnEnemyTurn(Action haveCharacterAlive, Action characterAllDead)
                {
                    StateMachine.ChangeState(new OnEnemyTurn(
                        onEnter: () =>
                        {
                            EnemyTeamSystem.Attack(
                                haveCharacterAlive: () =>
                                {
                                    haveCharacterAlive?.Invoke();
                                },
                                characterAllDead: () =>
                                {
                                    characterAllDead?.Invoke();
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
                    if (TurnCor is not null)
                    {
                        StopCoroutine(TurnCor);
                        TurnCor = null;
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
                    if (TurnCor is not null)
                    {
                        StopCoroutine(TurnCor);
                        TurnCor = null;
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