using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Customer
{
    public class CustomerBubble : MonoBehaviour
    {
        private CustomerManager CustomerManager { get; set; }
        
        private Transform BubbleParent { get; set; }
        
        private GameObject ThinkBubblePrefab { get; set; }
        private GameObject OrderBubblePrefab { get; set; }
        private GameObject WaitDishBubblePrefab { get; set; }
        private GameObject HappyBubblePrefab { get; set; }
        private GameObject AngryBubblePrefab { get; set; }
        
        public GameObject CurrentBubble { get; private set; }
        public Image CurrentBubbleCountDownImage { get; private set; }
        private Button CurrentBubbleButton { get; set; }
        public Image CurrentBubbleDishImage { get; private set; }

        private void Awake()
        {
            CustomerManager = GetComponent<CustomerManager>();
        }
        
        public void Init(Transform parent, GameObject think, GameObject order, GameObject waitDish,GameObject happy, GameObject angry)
        {
            BubbleParent = parent;
            
            ThinkBubblePrefab = think;
            OrderBubblePrefab = order;
            WaitDishBubblePrefab = waitDish;
            HappyBubblePrefab = happy;
            AngryBubblePrefab = angry;
        }

        public void SetButtonInteractable(bool interactable)
        {
            if (CurrentBubbleButton is not null)
                CurrentBubbleButton.interactable = interactable;
        }

        public void InitThinkBubble()
        {
            CurrentBubble = Instantiate(ThinkBubblePrefab, BubbleParent);
        }

        public void InitOrderBubble()
        {
            CurrentBubble = Instantiate(OrderBubblePrefab, BubbleParent);
            CurrentBubbleCountDownImage = CurrentBubble.GetComponent<Image>();
            CurrentBubbleButton = CurrentBubble.GetComponent<Button>();
            CurrentBubbleButton.onClick.AddListener(CustomerManager.BubbleStateMachine.OnClick);
        }

        public void InitWaitDishBubble()
        {
            CurrentBubble = Instantiate(WaitDishBubblePrefab, BubbleParent);
            CurrentBubbleCountDownImage = CurrentBubble.GetComponent<Image>();
            CurrentBubbleButton = CurrentBubble.GetComponent<Button>();
            CurrentBubbleButton.onClick.AddListener(CustomerManager.BubbleStateMachine.OnClick);
            CurrentBubbleDishImage = CurrentBubble.transform.Find("Icon").GetComponent<Image>();
        }

        public void InitHappyBubble()
        {
            // TODO
        }

        public void InitAngryBubble()
        {
            CurrentBubble = Instantiate(AngryBubblePrefab, BubbleParent);
        }

        public void DestroyBubble()
        {
            if (CurrentBubbleButton is not null)
            {
                CurrentBubbleButton.onClick.RemoveListener(CustomerManager.BubbleStateMachine.OnClick);
                CurrentBubbleButton = null;
            }
            
            if (CurrentBubbleCountDownImage is not null)
                CurrentBubbleCountDownImage = null;
            
            Destroy(CurrentBubble);
            CurrentBubble = null;
        }
    }
}