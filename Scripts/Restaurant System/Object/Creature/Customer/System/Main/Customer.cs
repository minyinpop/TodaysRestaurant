using System;
using System.Collections;
using System.Collections.Generic;
using Common.Clickable_Bubble;
using Common.Customer;
using Common.Interactable_Object;
using Common.Item.Data;
using Common.Item.Data.Serving_Note;
using Common.Value;
using Player_System.Object;
using Player_System.System.Player_System;
using Restaurant_System.Object.Creature.Customer.System.Child;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine.State;
using UI_System.Message_UI_System.Main;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Child.Open_Page.Child.Select_Food_Page.Data;
using UI_System.Restaurant_UI_System.Main;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Restaurant_System.Object.Creature.Customer.System.Main
{
    public sealed class Customer : MonoBehaviour, InteractableObject
    {
        [field: Header("狀態")]
        [field: SerializeField] private bool interactable;
                                public bool Interactable => interactable;
        
        [field: Header("自身組件")]
        [field: SerializeField] private MoveSystem moveSystem;
        [field: SerializeField] private AnimationSystem animationSystem;
        [field: SerializeField] private FlipSystem flipSystem;
        [field: SerializeField] private SkinSystem skinSystem;
        
        [field: Header("互動氣泡設定")]
        [field: SerializeField] private Transform bubbleParent;
        [field: SerializeField] private GameObject thinkBubble;
        [field: SerializeField] private GameObject orderBubble;
        [field: SerializeField] private GameObject servingNoteBubble;
        [field: SerializeField] private GameObject happyBubble;
        [field: SerializeField] private GameObject angryBubble;
        [field: SerializeField] private GameObject checkoutBubble;
        private ClickableBubble _currentBubble;
        
        [field: Header("資料")]
        [field: SerializeField] private SelectFoodPageSO selectFoodPageData;
        [field: SerializeField] private CustomerSO customerData;
        
        private readonly StateMachine _stateMachine = new();

        private IEnumerator _mainCoroutine;
        
        private ServingNoteSO _servingNoteData;
        
        public static event Func<ItemSO, bool> GivingServingNote;
        
        public event Action HappyToLeave;
        public event Action AngryToLeave;
        
        private bool isHappy;

        private void Awake()
        {
            moveSystem.ToLeft += flipSystem.TurnsLeft;
            moveSystem.ToRight += flipSystem.TurnsRight;
        }

        private void Start()
        {
            skinSystem.SetRandomSkin();
        }

        private void OnDisable()
        {
            if (_mainCoroutine is not null)
            {
                StopCoroutine(_mainCoroutine);
                _mainCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            moveSystem.ToLeft -= flipSystem.TurnsLeft;
            moveSystem.ToRight -= flipSystem.TurnsRight;
        }

        public void GiveServingNote(ServingNoteSO servingNote)
        {
            _servingNoteData = servingNote;
        }

        #region InteractableObject
            public void OnEnterDetect(PlayerObject playerObject)
            {
                _currentBubble?.SetInteractable(true);
            }
            
            public void OnExitDetect()
            {
                _currentBubble?.SetInteractable(false);
            }

            public void Interact(PlayerObject playerObject)
            {
                Debug.Log($"尚未實作 {nameof(Interact)}");
            }

            #endregion

        #region StateMachine
            #region WalkToSeatPoint
                public void WalkToSeatPoint(Transform standPoint, Transform sitPoint)
                {
                    _stateMachine.ChangeState(new WalkToSeatPoint(
                        onEnter: () =>
                        {
                            animationSystem.Walk();
                            moveSystem.StartWalk(standPoint,
                                onArrive: () =>
                                {
                                    animationSystem.Sit();
                                    moveSystem.SitDown(sitPoint);

                                    WatchFoodMenu();
                                });
                        },
                        onExit: () =>
                        {
                        }));
                }
            #endregion
            
            #region WatchFoodMenu
                private void WatchFoodMenu()
                {
                    _stateMachine.ChangeState(new WatchFoodMenu(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(thinkBubble, bubbleParent).GetComponent<ClickableBubble>();
                            _currentBubble.StartCountDown(
                                time: customerData.WatchFoodMenuDuration,
                                onComplete: () =>
                                {
                                    var orderedItems = new List<IItem>();
                                    
                                    selectFoodPageData.GetRandomItemData(out var firstItemData);
                                    orderedItems.Add(firstItemData);
                                    
                                    if (Random.Range(0, 100) > 80)
                                    {
                                        selectFoodPageData.GetRandomItemData(out var secondItemData);
                                        orderedItems.Add(secondItemData);
                                    }

                                    _servingNoteData.SetOrderedItems(orderedItems);
                                    WaitForOrder();
                                });
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                }
            #endregion
            
            #region WaitForOrder
                private void WaitForOrder()
                {
                    _stateMachine.ChangeState(new WaitForOrder(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(orderBubble, bubbleParent).GetComponent<ClickableBubble>();
                            _currentBubble.OnClick += OnClick;
                            _currentBubble.StartCountDown(
                                time: customerData.WaitForOrderDuration,
                                onComplete: () =>
                                {
                                    if (PlayerSystem.RemoveItem(_servingNoteData))
                                    {
                                        Angry();
                                        return;
                                    }
                                    
                                    throw new InvalidOperationException($"移除 {nameof(_servingNoteData)} 時，發生了錯誤。");
                                });
                        },
                        onExit: () =>
                        {
                            _currentBubble.OnClick -= OnClick;
                            
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                    
                    return;

                    void OnClick()
                    {
                        if (GivingServingNote?.Invoke(_servingNoteData) ?? false)
                        {
                            WaitForReturnServingNote();
                        }
                        else
                        {
                            throw new InvalidOperationException($"{name} 無法將 {nameof(_servingNoteData)} 給予玩家。");
                        }
                    }
                }
            #endregion
            
            #region WaitForReturnServingNote
                private void WaitForReturnServingNote()
                {
                    _stateMachine.ChangeState(new WaitForReturnServingNote(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(servingNoteBubble, bubbleParent).GetComponent<ClickableBubble>();
                            _currentBubble.ShowItem(_servingNoteData);
                            _currentBubble.StartCountDown(
                                time: customerData.WaitForReturnServingNoteDuration,
                                onComplete: () =>
                                {
                                    Angry();
                                });
                            
                            _currentBubble.OnClick += OnClick;
                        },
                        onExit: () =>
                        {
                            _currentBubble.OnClick -= OnClick;
                            
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;

                            if (!PlayerSystem.RemoveItem(_servingNoteData))
                            {
                                throw new InvalidOperationException($"無法把 {nameof(_servingNoteData)} 從玩家身上移除。");
                            }
                        }));
                    
                    return;
                    
                    void OnClick()
                    {
                        MessageUISystem.ShowSwitchUI(
                            content: new PopUpUIContent(
                                message: "確定要把料理給予顧客嗎？",
                                confirmButtonTitle: "確定",
                                cancelButtonTitle: "返回",
                                closeButtonTitle: string.Empty),
                            onConfirm: () =>
                            {
                                RestaurantUISystem.GetServingNoteItems(_servingNoteData, out var servingNoteItems);
                                
                                var correctNumber = _servingNoteData.OrderedItems.Count;
                                
                                for (var i = servingNoteItems.Count - 1; i >= 0; i--)
                                {
                                    var servingNoteItem = servingNoteItems[i];
                                    
                                    if (servingNoteItem is null)
                                    {
                                        servingNoteItems.RemoveAt(i);
                                        continue;
                                    }
                                    
                                    foreach (var orderedItem in _servingNoteData.OrderedItems)
                                    {
                                        if (servingNoteItem.ItemID == orderedItem.ItemID)
                                        {
                                            correctNumber--;
                                        }
                                    }
                                    
                                    servingNoteItems.RemoveAt(i);
                                }
                                
                                isHappy = correctNumber == 0;
                                ThinksServingNoteItems();
                            });
                    }
                }
            #endregion
            
            #region ThinksServingNoteItems
                private void ThinksServingNoteItems()
                {
                    _stateMachine.ChangeState(new ThinksServingNoteItems(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(thinkBubble, bubbleParent).GetComponent<ClickableBubble>();
                            
                            if (isHappy)
                            {
                                _currentBubble.StartCountDown(
                                    time: customerData.ThinksServingNoteItemsDuration,
                                    onComplete: () =>
                                    {
                                        Happy();
                                    });
                            }
                            else
                            {
                                _currentBubble.StartCountDown(
                                    time: customerData.ThinksServingNoteItemsDuration,
                                    onComplete: () =>
                                    {
                                        Angry();
                                    });
                            }
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                }
            #endregion
            
            #region Happy
                private void Happy()
                {
                    _stateMachine.ChangeState(new Happy(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(happyBubble, bubbleParent).GetComponent<ClickableBubble>();
                            _currentBubble.StartCountDown(
                                time: customerData.EmotionDuration,
                                onComplete: () =>
                                {
                                    WaitForCheckout();
                                });
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                }
            #endregion
            
            #region Angry
                private void Angry()
                {
                    _stateMachine.ChangeState(new Angry(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(angryBubble, bubbleParent).GetComponent<ClickableBubble>();
                            _currentBubble.StartCountDown(
                                time: customerData.EmotionDuration,
                                onComplete: () =>
                                {
                                    if (AngryToLeave is null)
                                    {
                                        throw new InvalidOperationException($"沒有其它 class 訂閱 {nameof(AngryToLeave)}。");
                                    }
                                    
                                    AngryToLeave();
                                });
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                }
            #endregion
            
            #region WaitForCheckout
                private void WaitForCheckout()
                {
                    _stateMachine.ChangeState(new WaitForCheckout(
                        onEnter: () =>
                        {
                            _currentBubble = Instantiate(checkoutBubble, bubbleParent).GetComponent<ClickableBubble>();
                            _currentBubble.OnClick += HappyToLeave;
                        },
                        onExit: () =>
                        {
                            _currentBubble.OnClick -= HappyToLeave;
                            
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                }
            #endregion
            
            #region WalkToEntrance
                public void WalkToEntrance(Transform standPoint, Transform spawnPoint, Action onArrive)
                {
                    _stateMachine.ChangeState(new WalkToEntrance(
                        onEnter: () =>
                        {
                            animationSystem.Walk();
                            moveSystem.StandUp(standPoint);
                            moveSystem.StartWalk(spawnPoint, onArrive);
                        },
                        onExit: () =>
                        {
                            Destroy(_currentBubble.gameObject);
                            _currentBubble = null;
                        }));
                }
            #endregion
        #endregion
    }
}