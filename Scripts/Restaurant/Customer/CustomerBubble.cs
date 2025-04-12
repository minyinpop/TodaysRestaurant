using Cysharp.Threading.Tasks;
using Database.Restaurant.Customer.Attribute;
using Restaurant.Bubble;
using UnityEngine;

namespace Restaurant.Customer
{
    // ==================================================
    // 用於執行顧客行動氣泡的程式碼。
    // ==================================================
    
    [RequireComponent(typeof(CustomerManager))]
    public class CustomerBubble : MonoBehaviour
    {
        // ========== { 資料相關 } ==========
        
        [field: Header("玩家屬性資料"), SerializeField]
        public CustomerAttributeSO CustomerAttribute { get; private set; }
        
        
        
        // ========== { 氣泡相關 } ==========
        
        [field: Header("氣泡"), Tooltip("氣泡的生成位置。"), SerializeField]
        private Transform BubbleSpawnPoint { get; set; }
        
        [field: Tooltip("顧客在思考時的氣泡。"), SerializeField]
        private GameObject ThinkingBubble { get; set; }
        
        [field: Tooltip("顧客在點餐時的氣泡。"), SerializeField]
        private GameObject OrderingBubble { get; set; }
        
        [field: Tooltip("顧客在等待餐點時的氣泡。"), SerializeField]
        private GameObject WaitingForMealBubble { get; set; }
        
        // 當前生成的氣泡遊戲物件。
        private GameObject CurrentBubble { get; set; }
        
        
        
        /// <summary>
        /// 用於生成思考氣泡的方法。
        /// 顧客在思考要點甚麼餐點。
        /// </summary>
        public async void InitThinkingBubble()
        {
            CheckBubbleIsExist();
            
            CurrentBubble = Instantiate(ThinkingBubble, BubbleSpawnPoint);
            
            var randomTime = Random.Range(CustomerAttribute.ThinkingTime.Min, CustomerAttribute.ThinkingTime.Max);
            CurrentBubble.GetComponent<BubbleManager>().OnInit(randomTime);

            await UniTask.Delay((int)randomTime * 1000);
        }
        
        
        
        private void CheckBubbleIsExist()
        {
            if (CurrentBubble is null)
                return;
            
            Destroy(CurrentBubble);
            CurrentBubble = null;
        }
    }
}