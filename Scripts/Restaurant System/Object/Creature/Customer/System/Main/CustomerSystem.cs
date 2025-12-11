using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Object;
using Item;
using Restaurant_System.Object.Creature.Customer.System.Child;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine;
using Restaurant_System.Object.Creature.Customer.System.Main.State_Machine.State;
using Restaurant_System.System.Child.Food_Menu_System.System.Child.Open_UI_System.Child.Select_Food_Page.Data;
using UnityEngine;

namespace Restaurant_System.Object.Creature.Customer.System.Main
{
    internal sealed class CustomerSystem : MonoBehaviour, InteractableObject
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
        
        [field: Header("Data Settings")]
        [field: SerializeField] private SelectFoodPageSO SelectFoodPageData;
        
        private readonly StateMachine StateMachine = new();
        
        private readonly Queue<ClickableBubble> Bubbles = new();
        private readonly Queue<ITem> OrderItems = new();
        private readonly Queue<Action> ActiveActions = new();
        
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
            public void OnEnterDetect()
            {
                foreach (var bubble in Bubbles) bubble.SetInteractable(true);
            }
            
            public void OnExitDetect()
            {
                foreach (var bubble in Bubbles) bubble.SetInteractable(false);
            }
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
                        Bubbles.Enqueue(Instantiate(ThinkBubble, BubbleParent).GetComponent<ClickableBubble>());

                        Bubbles.Peek().StartCountDown(3,
                            onComplete: () =>
                            {
                                SelectFoodPageData.GetRandomItemData(out var firstItemData);
                                OrderItems.Enqueue(firstItemData);
                                
                                var chance = UnityEngine.Random.Range(0, 100);
                                if (chance > 0)
                                {
                                    SelectFoodPageData.GetRandomItemData(OrderItems.ToArray(), out var secondItemData);
                                    if (secondItemData is not null) OrderItems.Enqueue(secondItemData);
                                }

                                WaitForOrder();
                            });
                    }

                    void OnExit()
                    {
                        var bubble = Bubbles.Dequeue();
                        Destroy(bubble.gameObject);
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
                        Bubbles.Enqueue(Instantiate(WaitForOrderBubble, BubbleParent).GetComponent<ClickableBubble>());
                        Bubbles.Peek().OnClickBubble += ShowOrderItem;
                        ActiveActions.Enqueue(() => Bubbles.Peek().OnClickBubble -= ShowOrderItem);
                        Bubbles.Peek().SetInteractable(Interactable);
                        Bubbles.Peek().StartCountDown(15,
                            onComplete: () => Debug.Log($"{name} 等待點餐太久了，已經沒了耐心。"));
                    }
                    
                    void OnExit()
                    {
                        var bubble = Bubbles.Dequeue();
                        Destroy(bubble.gameObject);
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
                                var bubble = Instantiate(ShowItemBubble, BubbleParent).GetComponent<ClickableBubble>();
                                bubble.ChangeItemImage(item, 1, () => complete = true);
                                yield return new WaitUntil(() => complete);
                            }
                            
                            WaitForOrderItem();
                        }
                    }

                    void OnExit()
                    {
                    }
                }
            #endregion
            
            #region WaitForOrderItem
                private void WaitForOrderItem()
                {
                    StateMachine.ChangeState(new WaitForOrderItem(OnEnter, OnExit));
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