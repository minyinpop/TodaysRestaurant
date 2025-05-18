using System.Collections.Generic;
using Database.Restaurant.Customer.Attribute;
using Database.Restaurant.Customer.Path;
using Database.Restaurant.Customer.Skin;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using Database.Restaurant.Player.Dish_Deliver;
using Restaurant.Customer.BubbleState;
using Restaurant.Customer.CustomerState;
using Restaurant.Customer.CustomerState.State;
using Spine.Unity;
using UnityEngine;

namespace Restaurant.Customer
{
    [RequireComponent(typeof(CustomerAnimator))]
    [RequireComponent(typeof(CustomerMove))]
    [RequireComponent(typeof(CustomerSkin))]
    [RequireComponent(typeof(CustomerBubble))]
    [RequireComponent(typeof(CustomerDetector))]
    [RequireComponent(typeof(CustomerOrder))]
    internal class CustomerManager : MonoBehaviour
    {
        [field: Header("顧客的各類資料庫")]
        [field: SerializeField] public DishDeliverSO DishDeliver { get; private set; }
        [field: SerializeField] public AttributeSO Attribute { get; private set; }
        [field: SerializeField] private PathSO Path { get; set; }
        [field: SerializeField] private List<SkinSO> Skins { get; set; }
        
        
        
        [field: Header("今日上架的料理資料庫")]
        [field: SerializeField] private TodayDishSO TodayAppetizer { get; set; }
        [field: SerializeField] private TodayDishSO TodayMainCourse { get; set; }
        [field: SerializeField] private TodayDishSO TodayDessert { get; set; }
        [field: SerializeField] private TodayDishSO TodayDrink { get; set; }
        
        
        
        [field: Header("顧客的 Spine 動畫資產")]
        [field: SerializeField] private AnimationReferenceAsset IdleClip { get; set; }
        [field: SerializeField] private AnimationReferenceAsset WalkClip { get; set; }
        [field: SerializeField] private AnimationReferenceAsset SitClip { get; set; }
        
        
        
        [field: Header("氣泡的生成位置")]
        [field: SerializeField] private Transform BubbleParent { get; set; }
        
        
        
        [field: Header("各類氣泡的預製件")]
        [field: SerializeField] private GameObject ThinkBubblePrefab { get; set; }
        [field: SerializeField] private GameObject OrderBubblePrefab { get; set; }
        [field: SerializeField] private GameObject WaitDishBubblePrefab { get; set; }
        [field: SerializeField] private GameObject HappyBubblePrefab { get; set; }
        [field: SerializeField] private GameObject AngryBubblePrefab { get; set; }
        
        
        
        private CustomerAnimator CustomerAnimator { get; set; }
        private CustomerMove CustomerMove { get; set; }
        private CustomerSkin CustomerSkin { get; set; }
        public CustomerBubble CustomerBubble { get; private set; }
        private CustomerDetector CustomerDetector { get; set; }
        public CustomerOrder CustomerOrder { get; private set; }
        
        
        
        private StateMachine StateMachine { get; set; } = new();
        public BubbleStateMachine BubbleStateMachine { get; private set; } = new();

        private void Awake()
        {
            CustomerAnimator = GetComponent<CustomerAnimator>();
            CustomerMove = GetComponent<CustomerMove>();
            CustomerSkin = GetComponent<CustomerSkin>();
            CustomerBubble = GetComponent<CustomerBubble>();
            CustomerDetector = GetComponent<CustomerDetector>();
            CustomerOrder = GetComponent<CustomerOrder>();

            CustomerAnimator.Init(IdleClip, WalkClip, SitClip);
            CustomerMove.Init(Attribute, Path);
            CustomerSkin.Init(Skins);
            CustomerBubble.Init(BubbleParent, ThinkBubblePrefab, OrderBubblePrefab, WaitDishBubblePrefab, HappyBubblePrefab, AngryBubblePrefab);
            CustomerOrder.Init(Attribute, DishDeliver, TodayAppetizer, TodayMainCourse, TodayDessert, TodayDrink);
        }

        private void Start()
        {
            SetCustomerState(new EnterState());
        }
        
        
        
        // =======
        // 狀態相關
        // =======
        public void SetCustomerState(IState newState)
        {
            StateMachine.SetState(new EnterState(), this);
        }
        
        public void ChangeCustomerState(IState nextState)
        {
            StateMachine.ChangeState(nextState, this);
        }

        public void SetBubbleState(IBubbleState newState)
        {
            BubbleStateMachine.SetState(newState, this);
        }

        public void ChangeBubbleState(IBubbleState nextState)
        {
            BubbleStateMachine.ChangeState(nextState, this);
        }

        public void ExitBubbleState()
        {
            BubbleStateMachine.ExitState();
        }
        
        
        
        // =======
        // 動畫相關
        // =======
        public void PlayIdleAnima()
        {
            // TODO
        }
        
        public void PlayWalkAnima()
        {
            CustomerAnimator.PlayWalkAnima();
        }

        public void PlaySitAnima()
        {
            CustomerAnimator.PlaySitAnima();
        }
        
        
        
        // =======
        // 移動相關
        // =======
        public void WalkToSeat()
        {
            CustomerMove.WalkToSeat();
        }

        public void OnSeat()
        {
            CustomerMove.OnSeat();
        }

        public void WalkToCheckout()
        {
            // TODO
        }

        public void WalkToEntrance()
        {
            // TODO
        }
        
        
        
        // =======
        // 氣泡相關
        // =======
        public void InitThinkBubble()
        {
            CustomerBubble.InitThinkBubble();
        }

        public void InitOrderBubble()
        {
            CustomerBubble.InitOrderBubble();
        }

        public void InitWaitDishBubble()
        {
            CustomerBubble.InitWaitDishBubble();
        }

        public void InitHappyBubble()
        {
            // TODO
        }

        public void InitAngryBubble()
        {
            CustomerBubble.InitAngryBubble();
        }

        public void DestroyBubble()
        {
            CustomerBubble.DestroyBubble();
        }
        
        
        
        // =======
        // 偵測相關
        // =======
        public void SetPlayerEnter(bool isEnter)
        {
            if (CustomerBubble.CurrentBubble is null)
                return;
            
            if (isEnter)
                BubbleStateMachine.PlayerEnter();
            else
                BubbleStateMachine.PlayerLeave();
        }

        public void SetBubbleInteractable(bool interactable)
        {
            if (CustomerBubble.CurrentBubble is not null)
                CustomerBubble.SetButtonInteractable(interactable);
        }

        public void SetFlipX(bool isFlip)
        {
            CustomerAnimator.SetFlipX(isFlip);
        }

        public DishSO TryOrderDish()
        {
            return CustomerOrder.TryOrderDish();
        }

        public DishSO TryTakeDish()
        {
            return CustomerOrder.TryTakeDish();
        }
    }
}