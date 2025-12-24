using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Object;
using Item;
using Item.Serving_Note;
using Restaurant_System.Object.Creature.Customer.System.Child;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine.State;
using Restaurant_System.Object.Food_Menu.System.Child.Open_Page.Child.Select_Food_Page.Data;
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
        [field: SerializeField] private GameObject waitForOrderBubble;
        [field: SerializeField] private GameObject showItemBubble;
        [field: SerializeField] private GameObject waitForItemBubble;
        
        [field: Header("Data Settings")]
        [field: SerializeField] private SelectFoodPageSO selectFoodPageData;
        
        private readonly StateMachine _stateMachine = new();

        private readonly Queue<ItemSO> _orderedItems = new();
        private readonly Queue<Action> _cleanUpActions = new();
        
        private ClickableBubble _currentBubble;
        
        private IEnumerator _mainCor;

        private ServingNoteSO _servingNoteData;

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
                                selectFoodPageData.GetRandomItemData(out var firstItemData);
                                _orderedItems.Enqueue(firstItemData);
                                
                                // TODO [2025.12.12] 讓顧客可以點更多餐點的程式碼，尚未更新，預留給未來。
                                // var chance = UnityEngine.Random.Range(0, 100);
                                // if (chance > 0)
                                // {
                                //     SelectFoodPageData.GetRandomItemData(OrderItems.ToArray(), out var secondItemData);
                                //     if (secondItemData is not null) OrderItems.Enqueue(secondItemData);
                                // }

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
                        _currentBubble = Instantiate(waitForOrderBubble, bubbleParent).GetComponent<ClickableBubble>();
                        _currentBubble.onClick += ShowOrderItem;
                        _currentBubble.StartCountDown(15, () =>
                        {
                            // TODO 等待點餐太久
                        });
                    }
                    
                    void OnExit()
                    {
                        _currentBubble.onClick -= ShowOrderItem;
                        Destroy(_currentBubble.gameObject);
                        _currentBubble = null;
                    }
                }
            #endregion
            
            #region ShowOrderItem
                private void ShowOrderItem()
                {
                    _stateMachine.ChangeState(new ShowOrderItem(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        _mainCor = ShowOrderItemCoroutine();
                        StartCoroutine(_mainCor);
                        return;

                        IEnumerator ShowOrderItemCoroutine()
                        {
                            foreach (var item in _orderedItems)
                            {
                                var complete = false;
                                _currentBubble = Instantiate(showItemBubble, bubbleParent).GetComponent<ClickableBubble>();
                                _currentBubble.ChangeItemImage(item, 1,
                                    onComplete: () =>
                                    {
                                        Destroy(_currentBubble.gameObject);
                                        _currentBubble = null;
                                        complete = true;
                                    });
                                yield return new WaitUntil(() => complete);
                            }
                            
                            WaitToGiveServingNote();
                        }
                    }

                    void OnExit()
                    {
                    }
                }
            #endregion
            
            #region WaitToGiveServingNote
                private void WaitToGiveServingNote()
                {
                    _stateMachine.ChangeState(new WaitToGiveServingNote(OnEnter, OnExit));
                    return;
                    
                    void OnEnter()
                    {
                        _currentBubble = Instantiate(waitForItemBubble, bubbleParent).GetComponent<ClickableBubble>();
                        _currentBubble.ChangeItemImage(_servingNoteData, 3, rename);
                        _currentBubble.StartCountDown(60, () =>
                        {
                            // TODO 等待餐點送達太久
                        });
                        _currentBubble.onClick += rename;
                    }
                    
                    void OnExit()
                    {
                        _currentBubble.onClick -= rename;
                        Destroy(_currentBubble.gameObject);
                        _currentBubble = null;
                    }
                }
            #endregion
            
            #region Rename
                private void rename()
                {
                }
            #endregion
        #endregion
    }
}