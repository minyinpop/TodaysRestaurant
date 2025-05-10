using Database.Restaurant.Dish;
using Restaurant.Kitchenware.StateMachine;
using Restaurant.Kitchenware.StateMachine.BubbleState;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware
{
    [RequireComponent(typeof(KitchenwareDetector))]
    [RequireComponent(typeof(KitchenwareCookMenu))]
    [RequireComponent(typeof(KitchenwareGame))]
    public class KitchenwareManager : MonoBehaviour
    {
        [field: Header("氣泡的生成位置")]
        [field: SerializeField]
        private Transform BubbleParent { get; set; }

        [field: Header("各類型氣泡的預製件")]
        [field: SerializeField] private GameObject EmptyBubblePrefab { get; set; }
        [field: SerializeField] private GameObject CookBubblePrefab { get; set; }
        [field: SerializeField] private GameObject GameBubblePrefab { get; set; }
        [field: SerializeField] private GameObject BurnBubblePrefab { get; set; }
        [field: SerializeField] private GameObject FinishBubblePrefab { get; set; }
        private GameObject CurrentBubbleObj { get; set; }
        private Image CurrentBubbleCountDownImage { get; set; }
        private Button CurrentBubbleButton { get; set; }

        private BubbleStateMachine BubbleStateMachine { get; set; } = new();
        public DishSO CurrentCookDish { get; private set; }
        
        private KitchenwareDetector KitchenwareDetector { get; set; }
        private KitchenwareCookMenu KitchenwareCookMenu { get; set; }
        private KitchenwareGame KitchenwareGame { get; set; }
        
        private void Awake()
        {
            KitchenwareDetector = GetComponent<KitchenwareDetector>();
            KitchenwareCookMenu = GetComponent<KitchenwareCookMenu>();
            KitchenwareGame = GetComponent<KitchenwareGame>();
        }

        private void Start()
        {
            BubbleStateMachine.SetState(new EmptyBubble(), this);
        }

        private void OnDestroy()
        {
            if (CurrentBubbleButton is not null)
                CurrentBubbleButton.onClick.RemoveListener(BubbleStateMachine.OnClick);
        }

        public void ChangeState(IBubbleState nextState)
        {
            BubbleStateMachine.ChangeState(nextState, this);
        }

        public void SetPlayerEnter(bool isEnter)
        {
            if (isEnter)
                BubbleStateMachine.PlayerEnter();
            else
                BubbleStateMachine.PlayerLeave();
        }

        public void SetCookMenuVisible(bool visible)
        {
            if (visible)
                KitchenwareCookMenu.OpenCookMenu();
            else
                KitchenwareCookMenu.CloseCookMenu();
        }

        public void SetBubbleInteractable(bool interactable)
        {
            if (CurrentBubbleButton is not null)
                CurrentBubbleButton.interactable = interactable;
        }
        
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
            CurrentBubbleButton = CurrentBubbleObj.GetComponent<Button>();
            CurrentBubbleCountDownImage = CurrentBubbleObj.GetComponentInChildren<Image>();
            CurrentBubbleButton.onClick.AddListener(BubbleStateMachine.OnClick);
        }

        public void InitBurnBubble()
        {
            CurrentBubbleObj = Instantiate(BurnBubblePrefab, BubbleParent);
            CurrentBubbleCountDownImage = CurrentBubbleObj.GetComponentInChildren<Image>();
            CurrentBubbleButton = CurrentBubbleObj.GetComponent<Button>();
            CurrentBubbleButton.onClick.AddListener(BubbleStateMachine.OnClick);
        }

        public void InitFinishBubble()
        {
            // TODO
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
            ChangeState(new CookBubble());
        }

        public void OnGameStart()
        {
            KitchenwareGame.OnGameStart();
        }

        public void OnGameFinish()
        {
            // TODO
            Debug.Log("Game Finish");
        }

        public void ClearDish()
        {
            CurrentCookDish = null;
        }
    }
}