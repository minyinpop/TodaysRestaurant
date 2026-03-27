using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Level.Child.Level_Enemy;
using Common.Value;
using Common.Value.Type;
using Explore_System.System.Child.Battle_System.System.Child;
using Explore_System.System.Child.Battle_System.System.Child.Selected_Card_System.Main;
using Explore_System.System.Child.Battle_System.System.Main.State_Machine;
using Explore_System.System.Child.Battle_System.System.Main.State_Machine.State;
using Player_System.Data.Child.Player_Team;
using UI_System.Message_UI_System.Main;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Main
{
    internal sealed class BattleSystem : SceneStarter
    {
        [field: Header("Systems")]
        [field: SerializeField] private SelectedCardSystem selectedCardSystem;
        [field: SerializeField] private CardPoolSystem cardPoolSystem;
        [field: SerializeField] private ShowCardSystem showCardSystem;
        [field: SerializeField] private HandCardSystem handCardSystem;
        [field: SerializeField] private UseCardSystem useCardSystem;
        [field: SerializeField] private PlayerTeamSystem playerTeamSystem;
        [field: SerializeField] private EnemyTeamSystem enemyTeamSystem;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerTeamSO playerTeamData;
        
        private readonly StateMachine _stateMachine = new();
        
        // TODO 改成誰先被打到誰就先行動
        private const TossResult _tossResult = TossResult.Tails;

        private IEnumerator _turnCoroutine;
        private IEnumerator _attackCoroutine;
        private IEnumerator _characterDeathCoroutine;
        private IEnumerator _drawCardAndShowCardCoroutine;

        private bool _isStarted;
        private bool _isEnd;

        private BattleEnemyEntry _currentBattleEnemyEntry;

        public static event Action<Action> OnClickEnemyWinConfirmButton;
        
        private void Awake()
        {
            if (selectedCardSystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(selectedCardSystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            if (cardPoolSystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(cardPoolSystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (showCardSystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(showCardSystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            if (handCardSystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(handCardSystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            if (useCardSystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(useCardSystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }
            
            if (playerTeamSystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(playerTeamSystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (enemyTeamSystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(enemyTeamSystem)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            if (playerTeamData is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(playerTeamData)} cannot be null.");
                Destroy(gameObject);
                return;           
            }

            PlayerTeamSystem.RecycleCard += OnRecycleCard;
        }

        private void OnDisable()
        {
            if (_turnCoroutine is not null)
            {
                StopCoroutine(_turnCoroutine);
                _turnCoroutine = null;
            }

            if (_attackCoroutine is not null)
            {
                StopCoroutine(_attackCoroutine);
                _attackCoroutine = null;
            }
            
            if (_characterDeathCoroutine is not null)
            {
                StopCoroutine(_characterDeathCoroutine);
                _characterDeathCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            PlayerTeamSystem.RecycleCard -= OnRecycleCard;
        }

        // Note: entry 一定不為 null，所以檢測裡面的參數，詳情請點開 class 查看。
        public override void StartSystem(BattleEnemyEntry entry)
        {
            if (_isStarted)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(_isStarted)} is already started.");
            }

            _currentBattleEnemyEntry = entry;

            OnBattleStart();
        }

        private void DrawAndShowCard(int drawNumber, Action onComplete)
        {
            _drawCardAndShowCardCoroutine = DrawAndShowCardCoroutine();
            StartCoroutine(_drawCardAndShowCardCoroutine);
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
                _drawCardAndShowCardCoroutine = null;
            }
        }

        private void OnRecycleCard(CardType[] cardTypes, Action onComplete)
        {
            _characterDeathCoroutine = RecycleCardCoroutine();
            StartCoroutine(_characterDeathCoroutine);
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
                    _stateMachine.ChangeState(new OnBattleStart(
                        onEnter: () =>
                        {
                            cardPoolSystem.Refill(
                                onComplete:() =>
                                {
                                    playerTeamData.GetCharacterNumber(out var number);
                                    number = Mathf.Clamp(number * 2, 1, 8);
                                    DrawAndShowCard(number, TurnManager);
                                });
                        },
                        onExit: () =>
                        {
                        }));
                }
            #endregion
            
            #region TurnManager
                private void TurnManager()
                {
                    _turnCoroutine = TurnManagerCoroutine();
                    StartCoroutine(_turnCoroutine);
                }

                private IEnumerator TurnManagerCoroutine()
                {
                    while (!_isEnd)
                    {
                        var playerTurnEnd = false;
                        var enemyTurnEnd = false;
                        var drawAndShowEnd = false;
                        
                        switch (_tossResult)
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
                                                StopCoroutine(_turnCoroutine);
                                                _turnCoroutine = null;
                                                OnEnemyWin();
                                            });
                                    },
                                    enemyAllDeath: () =>
                                    {
                                        StopCoroutine(_turnCoroutine);
                                        _turnCoroutine = null;
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
                                                StopCoroutine(_turnCoroutine);
                                                _turnCoroutine = null;
                                                OnPlayerWin();
                                            });
                                    },
                                    characterAllDead: () =>
                                    {
                                        StopCoroutine(_turnCoroutine);
                                        _turnCoroutine = null;
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
                    _stateMachine.ChangeState(new OnPlayerTurn(
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
                                    _attackCoroutine = UseCardCoroutine();
                                    StartCoroutine(_attackCoroutine);
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
                                                    StopCoroutine(_attackCoroutine);
                                                    _attackCoroutine = null;
                                                });
                                            yield return new WaitUntil(() => onUseComplete);
                                        }

                                        if (haveAnyEnemyAlive)
                                            haveEnemyAlive?.Invoke();
                                        _attackCoroutine = null;
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
                    _stateMachine.ChangeState(new OnEnemyTurn(
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
                    if (_turnCoroutine is not null)
                    {
                        StopCoroutine(_turnCoroutine);
                        _turnCoroutine = null;
                    }

                    _stateMachine.ChangeState(new OnPlayerWin(
                        onEnter: () =>
                        {
                            _isEnd = true;
                            MessageUISystem.ShowItemGetUI(
                                content: new PopUpUIContent(
                                    message: string.Empty,
                                    confirmButtonTitle: "拿取物品",
                                    cancelButtonTitle: string.Empty,
                                    closeButtonTitle: string.Empty),
                                items: _currentBattleEnemyEntry.ItemsData,
                                onConfirm: () =>
                                {
                                    // TODO 玩家點選確認按鈕後的程序
                                });
                        },
                        onExit: () =>
                        {
                        }));
                }
            #endregion
            
            #region OnEnemyWin
                private void OnEnemyWin()
                {
                    if (_turnCoroutine is not null)
                    {
                        StopCoroutine(_turnCoroutine);
                        _turnCoroutine = null;
                    }
                    
                    _stateMachine.ChangeState(new OnEnemyWin(
                        onEnter: () =>
                        {
                            _isEnd = true;
                            MessageUISystem.ShowDefeatUI(
                                content: new PopUpUIContent(
                                    message: "被打敗了",
                                    confirmButtonTitle: "再來一次",
                                    cancelButtonTitle: string.Empty,
                                    closeButtonTitle: string.Empty),
                                onConfirm: () =>
                                {
                                    if (OnClickEnemyWinConfirmButton is null)
                                    {
                                        // TODO 開發日誌：2026.03.27 16:06 不清楚為什麼 throw 的時候，editor 的 console 沒有 print，build 環境也沒有 crash。
                                        throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickEnemyWinConfirmButton)} has no subscriber.");
                                    }

                                    OnClickEnemyWinConfirmButton.Invoke(() =>
                                    {
                                        Debug.Log("返回到大廳。");
                                    });
                                });
                        },
                        onExit: () =>
                        {
                        }));
                }
            #endregion
        #endregion
    }
}