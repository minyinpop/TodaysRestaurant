using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Object;
using Common.Value;
using Item;
using Item.Serving_Note;
using Restaurant_System.Object.Creature.Customer.System.Child;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine.State;
using UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Child.Open_Page.Child.Select_Food_Page.Data;
using UI_System.System.Main;
using UnityEngine;

namespace Restaurant_System.Object.Creature.Customer.System.Main
{
    public sealed class Customer : MonoBehaviour, InteractableObject
    {
        [field: Header("Component Settings")]
        [field: SerializeField] private MoveSystem moveSystem;
        [field: SerializeField] private AnimationSystem animationSystem;
        [field: SerializeField] private FlipSystem flipSystem;
        [field: SerializeField] private SkinSystem skinSystem;
        
        [field: Header("Clickable Bubble Settings")]
        [field: SerializeField] private Transform bubbleParent;
        [field: SerializeField] private GameObject thinkBubble;
        [field: SerializeField] private GameObject orderBubble;
        [field: SerializeField] private GameObject servingNoteBubble;
        [field: SerializeField] private GameObject happyBubble;
        [field: SerializeField] private GameObject angryBubble;
        private ClickableBubble _currentBubble;
        
        [field: Header("Data Settings")]
        [field: SerializeField] private SelectFoodPageSO selectFoodPageData;
        
        private readonly StateMachine _stateMachine = new();
        
        private IEnumerator _mainCor;
        
        private ServingNoteSO _servingNoteData;
        
        public static event Func<ItemSO, bool> GivingServingNote;
        
        private readonly Queue<Action> _cleanUpActions = new();
        
        private void Start()
        {
            skinSystem.SetRandomSkin();
        }

        private void OnEnable()
        {
            moveSystem.ToLeft += flipSystem.TurnsLeft;
            _cleanUpActions.Enqueue(() => moveSystem.ToLeft -= flipSystem.TurnsLeft);
                
            moveSystem.ToRight += flipSystem.TurnsRight;
            _cleanUpActions.Enqueue(() => moveSystem.ToRight -= flipSystem.TurnsRight);
        }

        private void OnDisable()
        {
            while (_cleanUpActions.Count > 0) _cleanUpActions.Dequeue()?.Invoke();
            if (_mainCor is not null) { StopCoroutine(_mainCor); _mainCor = null; }
        }

        public void GiveServingNote(ServingNoteSO servingNote)
        {
            _servingNoteData = servingNote;
        }

        #region InteractableObject
            public void OnEnterDetect() => _currentBubble?.SetInteractable(true);
            public void OnExitDetect() => _currentBubble?.SetInteractable(false);
        #endregion

        #region StateMachine
            #region WalkToSeatPoint
                public void WalkToSeatPoint(Transform standPoint, Transform sitPoint)
                {
                    _stateMachine.ChangeState(new WalkToSeatPoint(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        animationSystem.Walk();
                        moveSystem.StartWalk(standPoint,
                            onArrive: () =>
                            {
                                animationSystem.Sit();
                                moveSystem.SitDown(sitPoint);

                                WatchFoodMenu();
                            });
                    }
                        
                    void OnExit()
                    {
                    }
                }
            #endregion
            
            #region WatchFoodMenu
                private void WatchFoodMenu()
                {
                    _stateMachine.ChangeState(new WatchFoodMenu(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        _currentBubble = Instantiate(thinkBubble, bubbleParent).GetComponent<ClickableBubble>();
                        _currentBubble.StartCountDown(3,
                            onComplete: () =>
                            {
                                var orderedItems = new List<ItemSO>();
                                
                                selectFoodPageData.GetRandomItemData(out var firstItemData);
                                orderedItems.Add(firstItemData);
                                
                                if (UnityEngine.Random.Range(0, 100) > 80)
                                {
                                    selectFoodPageData.GetRandomItemData(out var secondItemData);
                                    orderedItems.Add(secondItemData);
                                }

                                _servingNoteData.SetOrderedItems(orderedItems);
                                WaitForOrder();
                            });
                    }

                    void OnExit()
                    {
                        Destroy(_currentBubble.gameObject);
                        _currentBubble = null;
                    }
                }
            #endregion
            
            #region WaitForOrder
                private void WaitForOrder()
                {
                    _stateMachine.ChangeState(new WaitForOrder(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        _currentBubble = Instantiate(orderBubble, bubbleParent).GetComponent<ClickableBubble>();
                        _currentBubble.onClick += Rename;
                        _currentBubble.SetInteractable(true);
                        _currentBubble.StartCountDown(15, () =>
                        {
                            // TODO 顧客等待玩家點餐太久
                        });
                    }
                    
                    void OnExit()
                    {
                        _currentBubble.onClick -= Rename;
                        Destroy(_currentBubble.gameObject);
                        _currentBubble = null;
                    }

                    void Rename()
                    {
                        var isGivingServingNoteSuccess = GivingServingNote?.Invoke(_servingNoteData) ?? false;
                        if (isGivingServingNoteSuccess)
                        {
                            WaitForReturnServingNote();
                        }
                        else
                        {
                            Debug.Log("顧客無法給予玩家點餐的紙條");
                            // TODO 顧客無法給予玩家點餐的紙條
                        }
                    }
                }
            #endregion
            
            #region WaitForReturnServingNote
                private void WaitForReturnServingNote()
                {
                    _stateMachine.ChangeState(new WaitForReturnServingNote(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        _currentBubble = Instantiate(servingNoteBubble, bubbleParent).GetComponent<ClickableBubble>();
                        _currentBubble.ShowItem(_servingNoteData);
                        _currentBubble.StartCountDown(60, () =>
                        {
                            // TODO 顧客等待玩家收取點餐紙條太久
                        });
                        _currentBubble.onClick += OnClick;
                    }
                    
                    void OnExit()
                    {
                        _currentBubble.onClick -= OnClick;
                        Destroy(_currentBubble.gameObject);
                        _currentBubble = null;
                    }

                    void OnClick()
                    {
                        UISystem.ShowSwitchUI(
                            content: new PopUpUIContent(
                                message: "確定要把料理給予顧客嗎？",
                                confirmButtonTitle: "確定",
                                cancelButtonTitle: "返回",
                                closeButtonTitle: string.Empty),
                            onConfirm: () =>
                            {
                                UISystem.GetServingNoteItems(_servingNoteData, out var servingNoteItems);
                                _servingNoteData.GetOrderedItems(out var orderedItems);
                                
                                var correctNumber = orderedItems.Count;
                                
                                for (var i = servingNoteItems.Count - 1; i >= 0; i--)
                                {
                                    var servingNoteItem = servingNoteItems[i];
                                    
                                    if (servingNoteItem is null)
                                    {
                                        servingNoteItems.RemoveAt(i);
                                        continue;
                                    }
                                    
                                    foreach (var orderedItem in orderedItems)
                                    {
                                        if (servingNoteItem.ItemID == orderedItem.ItemID)
                                        {
                                            correctNumber--;
                                        }
                                    }
                                    
                                    servingNoteItems.RemoveAt(i);
                                }
                                
                                if (correctNumber == 0)
                                {
                                    Happy();
                                }
                                else
                                {
                                    Angry();
                                }
                            });
                    }
                }
            #endregion
            
            #region Happy
                private void Happy()
                {
                    _stateMachine.ChangeState(new Happy(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        _currentBubble = Instantiate(happyBubble, bubbleParent).GetComponent<ClickableBubble>();
                        _currentBubble.StartCountDown(3, WalkToEntrance);
                    }
                    
                    void OnExit()
                    {
                        Destroy(_currentBubble.gameObject);
                        _currentBubble = null;
                    }
                }
            #endregion
            
            #region Angry
                private void Angry()
                {
                    _stateMachine.ChangeState(new Angry(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        _currentBubble = Instantiate(angryBubble, bubbleParent).GetComponent<ClickableBubble>();
                        _currentBubble.StartCountDown(3, WalkToEntrance);
                    }
                    
                    void OnExit()
                    {
                        Destroy(_currentBubble.gameObject);
                        _currentBubble = null;
                    }
                }
            #endregion
            
            #region WalkToEntrance
                private void WalkToEntrance()
                {
                    _stateMachine.ChangeState(new WalkToEntrance(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                    }
                    
                    void OnExit()
                    {
                    }
                }
            #endregion
        #endregion
    }
}