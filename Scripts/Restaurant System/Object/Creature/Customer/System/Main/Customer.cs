using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Object;
using Item;
using Restaurant_System.Object.Creature.Customer.System.Child;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine.State;
using Restaurant_System.Object.Food_Menu.System.Child.Open_Page.Child.Select_Food_Page.Data;
using UnityEngine;

namespace Restaurant_System.Object.Creature.Customer.System.Main
{
    internal sealed class Customer : MonoBehaviour, InteractableObject
    {
        [field: Header("Component Settings")]
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private FlipSystem FlipSystem;
        [field: SerializeField] private SkinSystem SkinSystem;
        
        [field: Header("Clickable Bubble Settings")]
        [field: SerializeField] private Transform BubbleParent;
        [field: SerializeField] private GameObject ThinkBubble;
        [field: SerializeField] private GameObject WaitForOrderBubble;
        [field: SerializeField] private GameObject ShowItemBubble;
        [field: SerializeField] private GameObject WaitForItemBubble;
        
        [field: Header("Data Settings")]
        [field: SerializeField] private SelectFoodPageSO SelectFoodPageData;
        
        private readonly StateMachine StateMachine = new();

        private readonly Queue<ItemSO> OrderItems = new();
        private readonly Queue<Action> ActiveActions = new();
        
        private ClickableBubble CurrentBubble;
        
        private bool Interactable;

        private IEnumerator MainCor;

        private void Start()
        {
            SkinSystem.SetRandomSkin();
        }

        private void OnEnable()
        {
            MoveSystem.ToLeft += FlipSystem.TurnsLeft;
            ActiveActions.Enqueue(() => MoveSystem.ToLeft -= FlipSystem.TurnsLeft);
                
            MoveSystem.ToRight += FlipSystem.TurnsRight;
            ActiveActions.Enqueue(() => MoveSystem.ToRight -= FlipSystem.TurnsRight);
        }

        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
            if (MainCor is not null) { StopCoroutine(MainCor); MainCor = null; }
        }

        #region InteractableObject
            public void OnEnterDetect() => CurrentBubble?.SetInteractable(true);
            public void OnExitDetect() => CurrentBubble?.SetInteractable(false);
        #endregion

        #region StateMachine
            #region WalkToSeatPoint
                public void WalkToSeatPoint(Transform standPoint, Transform sitPoint)
                {
                    StateMachine.ChangeState(new WalkToSeatPoint(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        AnimationSystem.Walk();
                        MoveSystem.StartWalk(standPoint,
                            onArrive: () =>
                            {
                                AnimationSystem.Sit();
                                MoveSystem.SitDown(sitPoint);

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
                    StateMachine.ChangeState(new WatchFoodMenu(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        CurrentBubble = Instantiate(ThinkBubble, BubbleParent).GetComponent<ClickableBubble>();
                        CurrentBubble.StartCountDown(3,
                            onComplete: () =>
                            {
                                SelectFoodPageData.GetRandomItemData(out var firstItemData);
                                OrderItems.Enqueue(firstItemData);
                                
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
                        Destroy(CurrentBubble.gameObject);
                        CurrentBubble = null;
                    }
                }
            #endregion
            
            #region WaitForOrder
                private void WaitForOrder()
                {
                    StateMachine.ChangeState(new ShowOrderItem(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        CurrentBubble = Instantiate(WaitForOrderBubble, BubbleParent).GetComponent<ClickableBubble>();
                        CurrentBubble.OnClick += ShowOrderItem;
                        CurrentBubble.StartCountDown(15, () => Debug.Log($"{name} 等待點餐太久了，已經沒了耐心。"));
                    }
                    
                    void OnExit()
                    {
                        CurrentBubble.OnClick -= ShowOrderItem;
                        Destroy(CurrentBubble.gameObject);
                        CurrentBubble = null;
                    }
                }
            #endregion
            
            #region ShowOrderItem
                private void ShowOrderItem()
                {
                    StateMachine.ChangeState(new ShowOrderItem(OnEnter, OnExit));
                    return;

                    void OnEnter()
                    {
                        MainCor = ShowOrderItemCoroutine();
                        StartCoroutine(MainCor);
                        return;

                        IEnumerator ShowOrderItemCoroutine()
                        {
                            foreach (var item in OrderItems)
                            {
                                var complete = false;
                                CurrentBubble = Instantiate(ShowItemBubble, BubbleParent).GetComponent<ClickableBubble>();
                                CurrentBubble.ChangeItemImage(item, 1,
                                    onComplete: () =>
                                    {
                                        Destroy(CurrentBubble.gameObject);
                                        CurrentBubble = null;
                                        complete = true;
                                    });
                                yield return new WaitUntil(() => complete);
                            }
                            
                            WaitForItem();
                        }
                    }

                    void OnExit()
                    {
                    }
                }
            #endregion
            
            #region WaitForItem
                private void WaitForItem()
                {
                    StateMachine.ChangeState(new WaitForItem(OnEnter, OnExit));
                    return;
                    
                    void OnEnter()
                    {
                        CurrentBubble = Instantiate(WaitForItemBubble, BubbleParent).GetComponent<ClickableBubble>();
                        // CurrentBubble.OnClick +=
                    }
                    
                    void OnExit()
                    {
                    }
                }
            #endregion
        #endregion
    }
}