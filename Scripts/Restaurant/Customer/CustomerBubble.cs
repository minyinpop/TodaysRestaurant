using Restaurant.Customer.BubbleState;
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
        
        private GameObject CurrentBubble { get; set; }
        private Button CurrentBubbleButton { get; set; }

        private void Awake()
        {
            CustomerManager = GetComponent<CustomerManager>();
        }
        
        public void Init(Transform parent, GameObject think, GameObject order)
        {
            BubbleParent = parent;
            
            ThinkBubblePrefab = think;
            OrderBubblePrefab = order;
        }

        public void SetButtonInteractable(bool interactable)
        {
            CurrentBubbleButton.interactable = interactable;
        }

        public void InitThinkBubble()
        {
            CurrentBubble = Instantiate(ThinkBubblePrefab, BubbleParent);
            CurrentBubbleButton = CurrentBubble.GetComponent<Button>();
            CurrentBubbleButton.onClick.AddListener(CustomerManager.BubbleStateMachine.OnClick);
        }

        public void InitOrderBubble()
        {
            // TODO
        }

        public void DestroyBubble()
        {
            CurrentBubbleButton.onClick.RemoveListener(CustomerManager.BubbleStateMachine.OnClick);
            CurrentBubbleButton = null;
            
            Destroy(CurrentBubble);
            CurrentBubble = null;
        }
    }
}