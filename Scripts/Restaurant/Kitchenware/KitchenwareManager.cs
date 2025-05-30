using System;
using Database.Restaurant.Dish;
using PixelCrushers.DialogueSystem;
using Restaurant.Kitchenware.BubbleState;
using Restaurant.Kitchenware.BubbleState.State;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware
{
    [RequireComponent(typeof(KitchenwareDetector))]
    [RequireComponent(typeof(KitchenwareCookMenu))]
    [RequireComponent(typeof(KitchenwareGame))]
    internal class KitchenwareManager : MonoBehaviour
    {
        [field: Header("虛擬相機的組件")]
        [field: SerializeField] public CinemachineCamera CinemachineCamera { get; private set; }
        
        
        
        [field: Header("廚具的種類")]
        [field: SerializeField] private CookUtensil CookUtensil { get; set; }
        
        [field: Header("選擇烹飪料介面的生成位置")]
        [field: SerializeField] private Transform CookMenuParent { get; set; }
        
        [field: Header("選擇烹飪料介面的預製件")]
        [field: SerializeField] private GameObject CookMenuPrefab { get; set; }
        
        
        
        [field: Header("氣泡的生成位置")]
        [field: SerializeField] private Transform BubbleParent { get; set; }

        [field: Header("各類型氣泡的預製件")]
        [field: SerializeField] private GameObject EmptyBubblePrefab { get; set; }
        [field: SerializeField] private GameObject CookBubblePrefab { get; set; }
        [field: SerializeField] private GameObject GameBubblePrefab { get; set; }
        [field: SerializeField] private GameObject BurnBubblePrefab { get; set; }
        [field: SerializeField] private GameObject FinishBubblePrefab { get; set; }
        
        
        
        [field: Header("小遊戲的生成位置")]
        [field: SerializeField] private Transform GameParent { get; set; }
        
        [field: Header("小遊戲的預製件")]
        [field: SerializeField] private GameObject GamePrefab { get; set; }
        
        [field: Header("對話系統觸發組件")]
        [field: SerializeField] private DialogueSystemTrigger DialogueSystemTrigger { get; set; }

        public bool IsTutorialCanPlayGame { get; set; }
        private bool IsGameStart { get; set; }
        
        private GameObject CurrentBubbleObj { get; set; }
        public Image CurrentBubbleCountDownImage { get; private set; }
        private Button CurrentBubbleButton { get; set; }

        private BubbleStateMachine BubbleStateMachine { get; set; }
        public DishSO CurrentCookDish { get; private set; }
        public float RemainingCookTime { get; set; }
        
        private KitchenwareDetector KitchenwareDetector { get; set; }
        private KitchenwareCookMenu KitchenwareCookMenu { get; set; }
        private KitchenwareGame KitchenwareGame { get; set; }
        
        public static event Func<DishSO, bool> GetDishEvent;
        public static event Action CloseCoachMaskEvent;

        private void Awake()
        {
            KitchenwareDetector = GetComponent<KitchenwareDetector>();
            KitchenwareCookMenu = GetComponent<KitchenwareCookMenu>();
            KitchenwareGame = GetComponent<KitchenwareGame>();
            
            KitchenwareCookMenu.Init(CookUtensil, CookMenuParent, CookMenuPrefab);
            KitchenwareGame.Init(GameParent, GamePrefab);
        }

        private void OnDisable()
        {
            if (CurrentBubbleButton is not null)
                CurrentBubbleButton.onClick.RemoveListener(BubbleStateMachine.OnClick);
        }

        public void Init()
        {
            BubbleStateMachine = new BubbleStateMachine();
            BubbleStateMachine.SetState(new EmptyBubble(), this);
        }

        public void ChangeState(IBubbleState nextState)
        {
            BubbleStateMachine.ChangeState(nextState, this);
        }

        public void SetPlayerEnter(bool isEnter)
        {
            if (IsGameStart)
                return;
            
            if (isEnter)
                BubbleStateMachine.PlayerEnter();
            else
                BubbleStateMachine.PlayerLeave();
        }
        
        public void OpenCookMenu() => KitchenwareCookMenu.OpenCookMenu();
        public void CloseCookMenu() => KitchenwareCookMenu.CloseCookMenu();
        
        public void SetBubbleInteractableTrue() => CurrentBubbleButton.interactable = true;
        public void SetBubbleInteractableFalse() => CurrentBubbleButton.interactable = false;

        public void InitEmptyBubble()
        {
            CurrentBubbleObj = Instantiate(EmptyBubblePrefab, BubbleParent);
            CurrentBubbleButton = CurrentBubbleObj.GetComponent<Button>();
            CurrentBubbleButton.onClick.AddListener(BubbleStateMachine.OnClick);
        }

        public void InitCookBubble()
        {
            CurrentBubbleObj = Instantiate(CookBubblePrefab, BubbleParent);
            CurrentBubbleButton = CurrentBubbleObj.GetComponent<Button>();
            CurrentBubbleButton.onClick.AddListener(BubbleStateMachine.OnClick);
        }

        public void InitGameBubble()
        {
            CurrentBubbleObj = Instantiate(GameBubblePrefab, BubbleParent);
            CurrentBubbleCountDownImage = CurrentBubbleObj.GetComponentInChildren<Image>();
            CurrentBubbleButton = CurrentBubbleObj.GetComponent<Button>();
            CurrentBubbleButton.onClick.AddListener(BubbleStateMachine.OnClick);
        }

        public void InitBurnBubble()
        {
            CurrentBubbleObj = Instantiate(BurnBubblePrefab, BubbleParent);
            CurrentBubbleButton = CurrentBubbleObj.GetComponent<Button>();
            CurrentBubbleButton.onClick.AddListener(BubbleStateMachine.OnClick);
        }

        public void InitFinishBubble()
        {
            CurrentBubbleObj = Instantiate(FinishBubblePrefab, BubbleParent);
            CurrentBubbleButton = CurrentBubbleObj.GetComponent<Button>();
            CurrentBubbleButton.onClick.AddListener(BubbleStateMachine.OnClick);
        }

        public void DestroyBubble()
        {
            CurrentBubbleButton.onClick.RemoveListener(BubbleStateMachine.OnClick);
            CurrentBubbleButton = null;

            CurrentBubbleCountDownImage = null;
            
            Destroy(CurrentBubbleObj);
            CurrentBubbleObj = null;
        }
        
        public void OnChooseDish(DishSO selectDish)
        {
            CurrentCookDish = selectDish;
            RemainingCookTime = CurrentCookDish.CookTime;
            ChangeState(new CookBubble());
        }
        
        public void OnGameStart()
        {
            IsGameStart = true;
            KitchenwareGame.OnGameStart();
        }

        public void OnGameCancel()
        {
            IsGameStart = false;
            KitchenwareGame.OnGameCancel();
        }

        public void OnGameFinish()
        {
            ChangeState(new CookBubble());
        }

        public void GetDish()
        {
            if (GetDishEvent is null)
                return;
            
            if (GetDishEvent.Invoke(CurrentCookDish))
                ChangeState(new EmptyBubble());
        }

        public void ClearDish() => CurrentCookDish = null;
        public void StartConversation() => DialogueSystemTrigger.OnUse();
        public void CloseCoachMask() => CloseCoachMaskEvent?.Invoke();
    }
}