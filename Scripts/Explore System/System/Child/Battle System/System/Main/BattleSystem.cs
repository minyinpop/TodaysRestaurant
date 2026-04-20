using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio_System.Main;
using Common.Data_Saver.Player_Inventory_Saver.Main;
using Common.Database;
using Common.Enemy_Battle_Group;
using Common.Player.Child.Player_Team;
using Common.Scene_Name;
using Common.Scene_Starter;
using Common.Value;
using Common.Value.Type;
using Explore_System.System.Child.Battle_System.Object;
using Explore_System.System.Child.Battle_System.System.Child;
using Explore_System.System.Child.Battle_System.System.Main.State_Machine;
using Explore_System.System.Child.Battle_System.System.Main.State_Machine.State;
using UI_System.Message_UI_System.Main;
using UnityEngine;

namespace Explore_System.System.Child.Battle_System.System.Main
{
    public sealed class BattleSystem : SceneStarter
    {
        [field: Header("子系統")]
        [field: SerializeField] private SelectedCardSystem selectedCardSystem;
        [field: SerializeField] private CardPoolSystem cardPoolSystem;
        [field: SerializeField] private ShowCardSystem showCardSystem;
        [field: SerializeField] private HandCardSystem handCardSystem;
        [field: SerializeField] private UseCardSystem useCardSystem;
        [field: SerializeField] private PlayerTeamSystem playerTeamSystem;
        [field: SerializeField] private EnemyTeamSystem enemyTeamSystem;
        [field: SerializeField] private CardInformationSystem cardInformationSystem;
        
        [field: Header("資料")]
        [field: SerializeField] private PlayerTeamSO playerTeamData;
        
        [field: Header("敵人位置")]
        [field: SerializeField] private BattleEnemySlot[] enemySlots;
        
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
        
        // TODO 玩家打贏後，要把戰利品給玩家
        public static event Action OnClickPlayerWinConfirmButton;
        public static event Action<SceneNameSO> OnClickEnemyWinConfirmButton;
        
        private void Awake()
        {
            #region 必要條件檢查
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

                if (enemyTeamSystem is null)
                {
                    throw new InvalidOperationException(nameof(enemyTeamSystem));
                }

                if (cardInformationSystem is null)
                {
                    throw new InvalidOperationException($"{nameof(cardInformationSystem)} 沒有被掛載。");
                }

                if (playerTeamData is null)
                {
                    throw new InvalidOperationException(nameof(playerTeamData));
                }
            #endregion

            PlayerTeamSystem.RecycleCard += OnRecycleCard;
            
            handCardSystem.TryAddCardToSelected += selectedCardSystem.TryAdd;
            selectedCardSystem.ReturnCardToHand += handCardSystem.Add;
            
            handCardSystem.OnHoverCardEvent += cardInformationSystem.ShowCardInformation;
            handCardSystem.OnHoverExitEvent += cardInformationSystem.HideCardInformation;
            
            selectedCardSystem.OnHoverCardEvent += cardInformationSystem.ShowCardInformation;
            selectedCardSystem.OnHoverExitEvent += cardInformationSystem.HideCardInformation;
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
            
            if (_drawCardAndShowCardCoroutine is not null)
            {
                StopCoroutine(_drawCardAndShowCardCoroutine);
                _drawCardAndShowCardCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            PlayerTeamSystem.RecycleCard -= OnRecycleCard;
            
            handCardSystem.TryAddCardToSelected -= selectedCardSystem.TryAdd;
            selectedCardSystem.ReturnCardToHand -= handCardSystem.Add;
            
            handCardSystem.OnHoverCardEvent -= cardInformationSystem.ShowCardInformation;
            handCardSystem.OnHoverExitEvent -= cardInformationSystem.HideCardInformation;
            
            selectedCardSystem.OnHoverCardEvent -= cardInformationSystem.ShowCardInformation;
            selectedCardSystem.OnHoverExitEvent -= cardInformationSystem.HideCardInformation;
        }
        
        public override void InvokeOnSceneLoad(SceneStarterData starterData, Action onComplete)
        {
            #region 必要條件檢查
                if (_isStarted)
                {
                    throw new InvalidOperationException(nameof(_isStarted));
                }

                if (starterData is not EnemyBattleGroupSO enemyBattleGroupData)
                {
                    throw new ArgumentException($"{nameof(starterData)} 不是 {nameof(EnemyBattleGroupSO)}。");
                }
            #endregion
            
            #region 參數附值
                _enemyBattleGroupData = enemyBattleGroupData;
            #endregion
            
            #region 生成敵人
                foreach (var enemyObject in _enemyBattleGroupData.EnemyObjects)
                {
                    #region 必要條件檢查
                        if (enemyObject is null)
                        {
                            continue;
                        }
                    #endregion
                    
                    var complete = false;

                    foreach (var enemySlot in enemySlots)
                    {
                        if (enemySlot.SetEnemy(enemyObject))
                        {
                            complete = true;
                            break;
                        }
                    }

                    if (complete)
                    {
                        continue;
                    }
                    
                    Debug.Log("敵人太多了，沒有足夠的格子可供後續的敵人生成。");
                    break;
                }
            #endregion
        }
        
        public override void InvokeOnSceneChangeComplete()
        {
            #region 淡入戰鬥音樂
                AudioSystem.Instance.CommonBGM.FadeInBGM(_enemyBattleGroupData.BattleBGMData);
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

        private void OnRecycleCard(BattleCardType[] cardTypes, Action onComplete)
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
                            Debug.Log("卡池卡片回收完畢。");
                            CardPoolRecycleComplete = true;
                        });
                    
                    handCardSystem.RecycleCard(type,
                        onComplete: () =>
                        {
                            Debug.Log("手牌卡片回收完畢。");
                            HandCardRecycleComplete = true;
                        });
                    
                    useCardSystem.RecycleCard(type,
                        onComplete: () =>
                        {
                            Debug.Log("使用卡片回收完畢。");
                            UseCardRecycleComplete = true;
                        });
                    
                    yield return new WaitUntil(() => CardPoolRecycleComplete && HandCardRecycleComplete && UseCardRecycleComplete);
                    
                    completes[index] = true;
                }
                
                Debug.Log("等待目標卡片被回收中。");
                
                yield return new WaitUntil(() => completes.All(c => c));
                
                Debug.Log("卡片回收完畢。");
                
                var CardPoolRefillComplete = false;
                
                cardPoolSystem.Refill(
                    onComplete: () =>
                    {
                        Debug.Log("卡池卡片填充完畢。");
                        CardPoolRefillComplete = true;
                    });
                
                yield return new WaitUntil(() => CardPoolRefillComplete);
                
                Debug.Log("卡片回收邏輯完成。");
                onComplete.Invoke();
            }
        }

        #region StateMachine
            #region OnBattleStart
                private void OnBattleStart()
                {
                    #region 設定系統狀態
                        _isStarted = true;
                    #endregion
                    
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
                                }, 
                                onUIClose: () =>
                                {
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
                                    Debug.Log("敵人攻擊結束，還有角色存活。");
                                    
                                    haveCharacterAlive.Invoke();
                                },
                                characterAllDead: () =>
                                {
                                    Debug.Log("敵人攻擊結束，所有角色已被打敗。");
                                    
                                    characterAllDead.Invoke();
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
                    #region 清除資料
                        if (_turnCoroutine is not null)
                        {
                            StopCoroutine(_turnCoroutine);
                            _turnCoroutine = null;
                        }
                    #endregion
                    
                    #region 儲存資料
                        playerTeamSystem.SaveData();
                    #endregion
                    
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
                                        throw new InvalidOperationException($"{name} > {nameof(BattleSystem)} > {nameof(OnClickPlayerWinConfirmButton)} has no subscriber.");
                                    }
                                    
                                    PlayerInventorySaver.AddItemToInventory(_enemyBattleGroupData.LootsData);
                                    
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
                    #region 清除資料
                        if (_turnCoroutine is not null)
                        {
                            StopCoroutine(_turnCoroutine);
                            _turnCoroutine = null;
                        }
                    #endregion
                    
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
                                        throw new InvalidOperationException($"{name} > {nameof(BattleSystem)} > {nameof(OnClickEnemyWinConfirmButton)} has no subscriber.");
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