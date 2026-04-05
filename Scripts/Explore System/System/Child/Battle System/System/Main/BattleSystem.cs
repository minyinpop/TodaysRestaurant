using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Enemy_Battle_Group;
using Common.Player.Child.Player_Team;
using Common.Scene_Name;
using Common.Scene_Starter;
using Common.Value;
using Common.Value.Type;
using Explore_System.System.Child.Battle_System.System.Child;
using Explore_System.System.Child.Battle_System.System.Child.Selected_Card_System.Main;
using Explore_System.System.Child.Battle_System.System.Main.State_Machine;
using Explore_System.System.Child.Battle_System.System.Main.State_Machine.State;
using UI_System.Message_UI_System.Main;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Main
{
    public sealed class BattleSystem : SceneStarter
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

        private EnemyBattleGroupSO _enemyBattleGroupData;

        public static event Action OnClickPlayerWinConfirmButton;
        public static event Action<SceneNameSO> OnClickEnemyWinConfirmButton;
        
        private void Awake()
        {
            if (selectedCardSystem is null)
            {
                throw new InvalidOperationException(nameof(selectedCardSystem));
            }
            
            if (cardPoolSystem is null)
            {
                throw new InvalidOperationException(nameof(cardPoolSystem));
            }

            if (showCardSystem is null)
            {
                throw new InvalidOperationException(nameof(showCardSystem));
            }
            
            if (handCardSystem is null)
            {
                throw new InvalidOperationException(nameof(handCardSystem));
            }
            
            if (useCardSystem is null)
            {
                throw new InvalidOperationException(nameof(useCardSystem));
            }
            
            if (playerTeamSystem is null)
            {
                throw new InvalidOperationException(nameof(playerTeamSystem));
            }

            if (playerTeamData is null)
            {
                throw new InvalidOperationException(nameof(playerTeamData));
            }

            if (enemyTeamSystem is null)
            {
                throw new InvalidOperationException(nameof(enemyTeamSystem));
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
        public override void StartSystem(SceneStarterData starterData, Action onComplete)
        {
            #region 必要條件檢查
                if (_isStarted)
                {
                    throw new InvalidOperationException(nameof(_isStarted));
                }

                if (starterData is not EnemyBattleGroupSO data)
                {
                    throw new ArgumentException($"{nameof(data)} is not {nameof(EnemyBattleGroupSO)}");
                }
            #endregion

            #region 參數附值
                _enemyBattleGroupData = data;
            #endregion

            #region 進入開始戰鬥狀態
                OnBattleStart();
            #endregion
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
                                    var number = playerTeamData.CharacterNumber;
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
                            /*
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
                            */
                            
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
                        DrawAndShowCard(playerTeamData.CharacterNumber, () => drawAndShowEnd = true);
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
                                    message: "戰鬥勝利",
                                    confirmButtonTitle: "拿取物品",
                                    cancelButtonTitle: string.Empty,
                                    closeButtonTitle: string.Empty),
                                items: _enemyBattleGroupData.LootsData,
                                onConfirm: () =>
                                {
                                    if (OnClickPlayerWinConfirmButton is null)
                                    {
                                        throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickPlayerWinConfirmButton)} has no subscriber.");
                                    }

                                    OnClickPlayerWinConfirmButton.Invoke();
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
                                    confirmButtonTitle: "返回城鎮",
                                    cancelButtonTitle: string.Empty,
                                    closeButtonTitle: string.Empty),
                                onConfirm: () =>
                                {
                                    if (OnClickEnemyWinConfirmButton is null)
                                    {
                                        // TODO 開發日誌：2026.03.27 16:06 不清楚為什麼 throw 的時候，editor 的 console 沒有 print，build 環境也沒有 crash。
                                        throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(OnClickEnemyWinConfirmButton)} has no subscriber.");
                                    }

                                    SceneNameDatabase.GetSceneName(SceneNameType.Lobby_Scene, out var sceneNameData);
                                    OnClickEnemyWinConfirmButton.Invoke(sceneNameData);
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