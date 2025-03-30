using DataBase.Item.Category.Cuisine;
using DataBase.Player.Cuisine_Deliver;
using NPC.Customer;
using UnityEngine;
using UnityEngine.UI;

namespace Bubble.Order.Category
{
    /// <summary>
    /// 用於管理等待餐點的氣泡的類。
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class WaitCuisineBubble : OrderBubble
    {
        [Header("資料庫"), Tooltip("玩家用來運送料理的資料庫。"), SerializeField]
        private PlayerCuisineDeliverData playerCuisineDeliverData;
        
        [Header("組件"), Tooltip("用來顯示顧客選擇了甚麼樣的菜品的圖片組件。"), SerializeField]
        private Image cuisineImage;
            
        [Tooltip("用於顯示顧客還剩下多少的耐心。"), SerializeField]
        private Image maskImage;
        
        
        
        // 自身的 Button 組件，用於切換是否可以點擊氣泡。
        private Button _button;
        
        // 顧客的 CustomerOrder 組件，用於回傳氣泡的狀況使用。
        private CustomerOrder _customerOrder;
        
        
        
        // 顧客有多久的耐心可以等待。
        private float _targetWaitTime;
        
        
        
        //
        public static event System.Action onCuisineDeliver;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        private void Update()
        {
            maskImage.fillAmount += Time.deltaTime / _targetWaitTime;
        }

        /// <summary>
        /// 當等待餐點的氣泡被按下後，所執行的方法。
        /// 用於判斷玩家頭上是否有料理，不管是不是顧客想要的。
        /// 依照玩家頭上最上面的料理，去給顧客判斷，是否是他想要的。
        /// 用於 Button 的 On Click()。
        /// </summary>
        public void OnClick()
        {
            var selectCuisineData = playerCuisineDeliverData.GetCuisineData();
            
            if (selectCuisineData is null)
                return;
            
            _customerOrder.GetCuisine(selectCuisineData);
            onCuisineDeliver?.Invoke();
        }
        
        /// <summary>
        /// 用於初始化氣泡的方法。
        /// </summary>
        /// <param name="newCustomerOrder"> 新傳入的 CustomerOrder 的組件，用於回傳資訊用。 </param>
        /// <param name="chooseCuisine"> 顧客所選擇的菜品的資料。 </param>
        /// <param name="targetWaitTime"> 新傳入的等待時間。 </param>
        public void InitBubble(CustomerOrder newCustomerOrder, Cuisine chooseCuisine,float targetWaitTime)
        {
            _customerOrder = newCustomerOrder;
            _targetWaitTime = targetWaitTime;

            cuisineImage.sprite = chooseCuisine.Sprite;
        }
        
        /// <summary>
        /// 用於切換按鈕是否可以互動的方法。
        /// 不需要傳入任何 Property。
        /// 用於氣泡內部的條件式判斷。
        /// </summary>
        public override void ChangeButtonInteractable()
        {
            // 判斷玩家是否有在運送料理，並切換氣泡的互動狀態。
            _button.interactable = playerCuisineDeliverData.cuisineDataList[0] is not null;
        }
    }
}
