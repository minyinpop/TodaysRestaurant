using DataBase.Item.Category.Cuisine;
using NPC.Customer;
using UnityEngine;
using UnityEngine.UI;

namespace NPC.Bubble.Order.Category
{
    /// <summary>
    /// 用於管理等待餐點的氣泡的類。
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class WaitCuisineBubble : OrderBubble
    {
        [Header("組件"), Tooltip("用來顯示顧客選擇了甚麼樣的菜品的圖片組件。"), SerializeField]
        private Image cuisineImage;
            
        [Tooltip("用於顯示顧客還剩下多少的耐心。"), SerializeField]
        private Image maskImage;
        
        // 顧客的 CustomerOrder 組件，用於回傳氣泡的狀況使用。
        private CustomerOrder _customerOrder;
        
        // 顧客所選擇的菜品的資料。
        private Cuisine _chooseCuisine;
        
        // 顧客有多久的耐心可以等待。
        private float _targetWaitTime;
        
        // 自身的 Button 組件，用於切換是否可以點擊氣泡。
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        private void Update()
        {
            maskImage.fillAmount += Time.deltaTime / _targetWaitTime;
        }
        
        /// <summary>
        /// 用於初始化氣泡的方法。
        /// </summary>
        /// <param name="newCustomerOrder"> 新傳入的 CustomerOrderBubble 的組件，用於回傳資訊用。 </param>
        /// <param name="chooseCuisine"> 顧客所選擇的菜品的資料。 </param>
        /// <param name="targetWaitTime"> 新傳入的等待時間。 </param>
        public void InitBubble(CustomerOrder newCustomerOrder, Cuisine chooseCuisine,float targetWaitTime)
        {
            _customerOrder = newCustomerOrder;
            _chooseCuisine = chooseCuisine;
            _targetWaitTime = targetWaitTime;

            cuisineImage.sprite = chooseCuisine.Sprite;
        }
        
        /// <summary>
        /// 用於切換按鈕是否可以互動的類。
        /// </summary>
        /// <param name="interactable"> 是否可以互動。 </param>
        public override void ChangeButtonInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }
    }
}
