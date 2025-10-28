using System.Collections;
using System.Collections.Generic;
using System.Economy.Child.Customer.Child;
using System.Economy.Child.Customer.Child.Bubble;
using System.Economy.Child.Customer.Main.State_Machine;
using System.Economy.Child.Customer.Main.State_Machine.State;
using Data.Economy.Food_Menu.Select_Food_Page;
using Data.Item.Base;
using UnityEngine;

namespace System.Economy.Child.Customer.Main
{
    internal sealed class CustomerSystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private MoveSystem MoveSystem;
        [field: SerializeField] private AnimationSystem AnimationSystem;
        [field: SerializeField] private FlipSystem FlipSystem;
        [field: SerializeField] private SkinSystem SkinSystem;
        
        [field: Header("Bubble")]
        [field: SerializeField] private Transform BubbleParent;
        [field: SerializeField] private GameObject ThinkBubblePrefab;
        [field: SerializeField] private GameObject WaitForOrderBubblePrefab;
        [field: SerializeField] private GameObject ShowOrderItemBubblePrefab;
        
        [field: Header("Data")]
        [field: SerializeField] private SelectFoodPageSO SelectFoodPageData;
        
        private readonly StateMachine StateMachine = new();
        
        private readonly Queue<Bubble> Bubbles = new();
        private readonly Queue<ItemSO> OrderItems = new();
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
        
        public void SetInteractable(bool interactable)
        {
            Interactable = interactable;
            foreach (var bubble in Bubbles) bubble.SetInteractable(Interactable);
        }
        
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
                        Bubbles.Enqueue(Instantiate(ThinkBubblePrefab, BubbleParent).GetComponent<Bubble>());

                        Bubbles.Peek().CountDown(3,
                            onComplete: () =>
                            {
                                SelectFoodPageData.GetRandomItemData(out var firstItemData);
                                OrderItems.Enqueue(firstItemData);
                                
                                // var chance = UnityEngine.Random.Range(0, 100);
                                // if (chance > 50)
                                // {
                                //     SelectFoodPageData.GetRandomItemData(OrderItems.ToArray(), out var secondItemData);
                                //     if (secondItemData is not null) OrderItems.Enqueue(secondItemData);
                                // }

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
                        Bubbles.Enqueue(Instantiate(WaitForOrderBubblePrefab, BubbleParent).GetComponent<Bubble>());
                        Bubbles.Peek().OnClick += ShowOrderItem;
                        ActiveActions.Enqueue(() => Bubbles.Peek().OnClick -= ShowOrderItem);
                        Bubbles.Peek().SetInteractable(Interactable);
                        Bubbles.Peek().CountDown(15,
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
                            while (OrderItems.Count > 0)
                            {
                                var complete = false;
                                var orderItem = OrderItems.Dequeue();

                                var bubble = Instantiate(ShowOrderItemBubblePrefab, BubbleParent).GetComponent<Bubble>();
                                bubble.ShowItem(orderItem, 1, () => complete = true);
                                Bubbles.Enqueue(bubble);
                                
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
                        foreach (var bubble in Bubbles)
                        {
                            bubble.CountDown(60,
                                onComplete: () => Debug.Log("沒有及時做餐點給顧客，他被氣走了。"));
                        }
                    }
                    
                    void OnExit()
                    {
                    }
                }
            #endregion
        #endregion
    }
}