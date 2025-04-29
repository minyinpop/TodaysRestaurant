using System;
using System.Collections;
using System.Collections.Generic;
using Database.Restaurant.Customer.Attribute;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using Restaurant.Customer.Bubble;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Restaurant.Customer
{
    public class CustomerOrder : MonoBehaviour
    {
        [field: Header("屬性資料")]
        [field: SerializeField] private CustomerAttributeSO Attribute { get; set; }
        
        [field: Header("氣泡的生成位置")]
        [field: SerializeField] private Transform BubbleParent { get; set; }
        
        [field: Header("各類型氣泡的預製件")]
        [field: SerializeField] private GameObject ThinkBubble { get; set; }
        [field: SerializeField] private GameObject OrderBubble { get; set; }
        [field: SerializeField] private GameObject WaitDishBubble { get; set; }
        [field: SerializeField] private GameObject CheckoutBubble { get; set; }
        [field: SerializeField] private GameObject HappyBubble { get; set; }
        [field: SerializeField] private GameObject AngryBubble { get; set; }

        private OrderDish OrderAppetizer { get; set; } = new();
        private OrderDish OrderMainCourse { get; set; } = new();
        private OrderDish OrderDessert { get; set; } = new();
        private OrderDish OrderDrink { get; set; } = new();
        
        private CustomerController CustomerController { get; set; }
        
        private IEnumerator MainCoroutine { get; set; }
        private IEnumerator CurrentCoroutine { get; set; }

        public event Action HappyToLeave;
        public event Action HaveNoPatience;
        
        // For MainProcess Only.
        private BubbleBase BubbleBase { get; set; }
        private bool IsClickBubble { get; set; }

        private void Awake()
        {
            CustomerController = GetComponent<CustomerController>();
        }

        private void OnEnable()
        {
            CustomerController.OnSeat += StartProcess;
            
            if (BubbleBase is not null)
                BubbleBase.ClickBubble += OnClickBubble;
        }
        
        private void OnDisable()
        {
            CustomerController.OnSeat -= StartProcess;
            
            if (BubbleBase is not null)
                BubbleBase.ClickBubble -= OnClickBubble;
            
            if (MainCoroutine is not null)
            {
                StopCoroutine(MainCoroutine);
                MainCoroutine = null;
            }

            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }
        }

        private void StartProcess()
        {
            MainCoroutine = MainProcess();
            StartCoroutine(MainCoroutine);
        }
        
        private IEnumerator MainProcess()
        {
            var orderDish = new List<OrderDish>
            {
                OrderAppetizer,
                OrderMainCourse,
                OrderDessert,
                OrderDrink
            };
            
            var chance = new List<int>
            {
                Attribute.OrderAttribute.OrderAppetizerChance,
                Attribute.OrderAttribute.OrderMainCourseChance,
                Attribute.OrderAttribute.OrderDessertChance,
                Attribute.OrderAttribute.OrderDrinkChance
            };

            var todayDish = new List<TodayDishSO>
            {
                Attribute.OrderAttribute.TodayAppetizer,
                Attribute.OrderAttribute.TodayMainCourse,
                Attribute.OrderAttribute.TodayDessert,
                Attribute.OrderAttribute.TodayDrink
            };

            yield return new WaitForSeconds(1f);
            
            CurrentCoroutine = ThinkProcess();
            yield return CurrentCoroutine;
            
            for (var i = 0; i < orderDish.Count; i++)
            {
                if (Random.Range(0, 100) >= chance[i])
                    continue;
                
                yield return new WaitForSeconds(1f);
                
                CurrentCoroutine = OrderProcess();
                yield return CurrentCoroutine;
                
                yield return new WaitForSeconds(1f);
                
                CurrentCoroutine = WaitDishProcess(orderDish[i], todayDish[i]);
                yield return CurrentCoroutine;
            }
            
            yield return new WaitForSeconds(1f);
            
            CurrentCoroutine = CheckoutProcess();
            yield return CurrentCoroutine;

            yield return new WaitForSeconds(1f);

            CustomerHappyToLeave();
        }

        private IEnumerator ThinkProcess()
        {
            var randomTime = Attribute.OrderAttribute.GetRandomThinkTime();
            Instantiate(ThinkBubble, BubbleParent).GetComponent<BubbleBase>().Init(randomTime);
            yield return new WaitForSeconds(randomTime);
        }

        private IEnumerator OrderProcess()
        {
            BubbleBase = Instantiate(OrderBubble, BubbleParent).GetComponent<BubbleBase>();
            BubbleBase.Init(Attribute.OrderAttribute.GetRandomOrderTime());
            BubbleBase.ClickBubble += OnClickBubble;
            BubbleBase.CustomerHaveNoPatience += CustomerHaveNoPatience;
            
            yield return new WaitUntil(() => IsClickBubble);
            IsClickBubble = false;
            BubbleBase.ClickBubble -= OnClickBubble;
            BubbleBase.CustomerHaveNoPatience -= CustomerHaveNoPatience;
        }

        private IEnumerator WaitDishProcess(OrderDish orderDish, TodayDishSO todayDish)
        {
            if (orderDish.HasSeen)
                yield break;
            
            if (!ChooseDish(orderDish, todayDish))
                yield break;
            
            yield return new WaitUntil(() => IsClickBubble);
            IsClickBubble = false;
            BubbleBase.ClickBubble -= OnClickBubble;
            BubbleBase.CustomerHaveNoPatience -= CustomerHaveNoPatience;
        }

        private IEnumerator CheckoutProcess()
        {
            BubbleBase = Instantiate(CheckoutBubble, BubbleParent).GetComponent<BubbleBase>();
            BubbleBase.Init(Attribute.OrderAttribute.GetRandomCheckoutTime());
            BubbleBase.ClickBubble += OnClickBubble;
            BubbleBase.CustomerHaveNoPatience += CustomerHaveNoPatience;
            
            yield return new WaitUntil(() => IsClickBubble);
            IsClickBubble = false;
            BubbleBase.ClickBubble -= OnClickBubble;
            BubbleBase.CustomerHaveNoPatience -= CustomerHaveNoPatience;
        }
        
        private bool ChooseDish(OrderDish orderDish, TodayDishSO todayDish)
        {
            orderDish.HasSeen = true;

            var todayDishSlots = new List<TodayDishSlot>();
            
            foreach (var slot in todayDish.TodayDishSlots)
                todayDishSlots.Add(slot);

            while (todayDishSlots.Count > 0)
            {
                var slotIndex = Random.Range(0, todayDishSlots.Count);

                if (todayDishSlots[slotIndex].TakeDish())
                {
                    orderDish.Dish = todayDishSlots[slotIndex].Dish;
                    BubbleBase = Instantiate(WaitDishBubble, BubbleParent).GetComponent<BubbleBase>();
                    BubbleBase.Init(Attribute.OrderAttribute.GetRandomWaitDishTime(), orderDish.Dish);
                    BubbleBase.ClickBubble += OnClickBubble;
                    BubbleBase.CustomerHaveNoPatience += CustomerHaveNoPatience;
                    return true;
                }
                
                todayDishSlots.RemoveAt(slotIndex);
            }

            return false;
        }
        
        private void OnClickBubble() => IsClickBubble = true;

        private void CustomerHappyToLeave()
        {
            HappyToLeave?.Invoke();
        }

        private void CustomerHaveNoPatience()
        {
            if (CurrentCoroutine is not null)
            {
                StopCoroutine(CurrentCoroutine);
                CurrentCoroutine = null;
            }

            BubbleBase = Instantiate(AngryBubble, BubbleParent).GetComponent<BubbleBase>();
            BubbleBase.Init(3f);
            HaveNoPatience?.Invoke();
        }
    }

    [Serializable]
    public class OrderDish
    {
        public bool HasSeen { get; set; }
        public DishSO Dish { get; set; }
    }
}