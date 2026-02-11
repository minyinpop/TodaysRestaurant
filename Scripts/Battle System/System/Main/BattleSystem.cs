using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle_System.System.Child;
using Battle_System.System.Child.Initiative_System.System.Main;
using Battle_System.System.Child.Selected_Card_System.Main;
using Battle_System.System.Main.State_Machine;
using Battle_System.System.Main.State_Machine.State;
using Common.Data.Player.Child.Player_Team;
using Common.Value;
using Common.Value.Type;
using UnityEngine;

namespace Battle_System.System.Main
{
    internal sealed class BattleSystem : MonoBehaviour
    {
        [field: Header("Systems")]
        [field: SerializeField] private SelectedCardSystem selectedCardSystem;
        [field: SerializeField] private CardPoolSystem cardPoolSystem;
        [field: SerializeField] private ShowCardSystem showCardSystem;
        [field: SerializeField] private HandCardSystem handCardSystem;
        [field: SerializeField] private InitiativeSystem initiativeSystem;
        [field: SerializeField] private UseCardSystem useCardSystem;
        [field: SerializeField] private PlayerTeamSystem playerTeamSystem;
        [field: SerializeField] private EnemyTeamSystem enemyTeamSystem;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerTeamSO playerTeamData;
        
        private readonly StateMachine StateMachine = new();
        
        private TossResult TossResult = TossResult.Tails;
        
        private GameObject InitiativeSystemObject;

        private IEnumerator TurnCor;
        private IEnumerator AttackCor;
        private IEnumerator CharacterDeathCor;
        private IEnumerator DrawCardAndShowCardCor;

        private bool IsEnd;
        
        // public static event Action ReloadScene;
        // public static event Action<string, Action> ChangeScene;
        // public static event Action<string, int> StartScenario;

        private void Start()
        {
            OnBattleStart();
        }

        private void OnEnable()
        {
            PlayerTeamSystem.RecycleCard += OnRecycleCard;
        }

        private void OnDisable()
        {
            PlayerTeamSystem.RecycleCard -= OnRecycleCard;
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
        
        private void DrawAndShowCard(int drawNumber, Action onComplete)
        {
            DrawCardAndShowCardCor = DrawAndShowCardCoroutine();
            StartCoroutine(DrawCardAndShowCardCor);
            return;
            
            IEnumerator DrawAndShowCardCoroutine()
            {
                var cardPoolRefillComplete = false;
                var AddCardToHandComplete = false;
                cardPoolSystem.DrawCard(drawNumber, out var cards);
                showCardSystem.ShowCard(cards, () =>
                {
                    cardPoolSystem.Refill(
                        onComplete: () =>
                        {
                            cardPoolRefillComplete = true;
                        });
                    showCardSystem.GetShowCards(out var showCards);
                    handCardSystem.Add(showCards,
                        onComplete: () =>
                        {
                            AddCardToHandComplete = true;
                        });
                });
                yield return new WaitUntil(() => cardPoolRefillComplete && AddCardToHandComplete);
                onComplete?.Invoke();
                DrawCardAndShowCardCor = null;
            }
        }

        private void OnRecycleCard(CardType[] cardTypes, Action onComplete)
        {
            CharacterDeathCor = RecycleCardCoroutine();
            StartCoroutine(CharacterDeathCor);
            return;

            IEnumerator RecycleCardCoroutine()
            {
                var completes = new List<bool>();
                for (var i = 0; i < cardTypes.Length; i++)
                {
                    var index = i;
                    var type = cardTypes[index];
                    completes.Add(false);
                    
                    var CardPoolRecycleComplete = false;
                    var HandCardRecycleComplete = false;
                    var UseCardRecycleComplete = false;
                    
                    cardPoolSystem.RecycleCard(type, 
                        onComplete:() =>
                        {
                            CardPoolRecycleComplete = true;
                        });
                    
                    handCardSystem.RecycleCard(type,
                        onComplete: () =>
                        {
                            HandCardRecycleComplete = true;
                        });
                    
                    useCardSystem.RecycleCard(type,
                        onComplete: () =>
                        {
                            UseCardRecycleComplete = true;
                        });
                    
                    yield return new WaitUntil(() => CardPoolRecycleComplete && HandCardRecycleComplete && UseCardRecycleComplete);
                    completes[index] = true;
                }
                
                yield return new WaitUntil(() => completes.All(c => c));
                var CardPoolRefillComplete = false;
                
                cardPoolSystem.Refill(
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
                            cardPoolSystem.Refill(
                                onComplete:() =>
                                {
                                    playerTeamData.GetCharacterNumber(out var number);
                                    number = Mathf.Clamp(number * 2, 1, 8);
                                    DrawAndShowCard(number, OnInitiativeCoin);
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
                            InitiativeSystemObject = Instantiate(initiativeSystem.gameObject);
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
                        playerTeamData.GetCharacterNumber(out var number);
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
                            selectedCardSystem.OpenUI(
                                onUIOpen: () =>
                                {
                                    handCardSystem.SetCardsInteractable(true);
                                }, 
                                onUIClose: () =>
                                {
                                    handCardSystem.SetCardsInteractable(false);
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
                                            useCardSystem.Use(
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
                            enemyTeamSystem.Attack(
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
                            // ItemGetUISystem.ShowUI(
                            //     content: new PopUpUIContent(
                            //         message: string.Empty,
                            //         confirmButtonTitle: "拿取物品",
                            //         cancelButtonTitle: string.Empty,
                            //         closeButtonTitle: string.Empty),
                            //     items: null, // TODO 怪物掉落物
                            //     onConfirm: () => Debug.Log("Confirm player win."));
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
                            // TODO
                            // UISystem.ShowDefeatUI(
                            //     content: new PopUpUIContent(
                            //         message: "被打敗了",
                            //         confirmButtonTitle: "再來一次",
                            //         cancelButtonTitle: string.Empty,
                            //         closeButtonTitle: string.Empty),
                            //     onConfirm: () =>
                            //     {
                            //         Debug.Log("確認玩家戰敗畫面");
                            //     });
                        },
                        onExit: () =>
                        {
                        }));
                }
            #endregion
        #endregion
    }
}