using System.Collections.Generic;
using Database.Restaurant.Meals;
using UnityEngine;

namespace Database.Restaurant.Chosen
{
    // ==================================================
    // 玩家所選擇的料理資料庫。
    // 用於當天餐廳經營時，顧客所選擇的料理。
    // 裡面的資料開放給其它 class 做修改。
    // ==================================================
    
    [CreateAssetMenu(fileName = "New Chosen Meals Type", menuName = "Minyinpop/Restaurant/Chosen Meals Type", order = 2)]
    public class ChosenMealsTypeSO : ScriptableObject
    {
        [field: Tooltip("料理格資訊陣列。"), SerializeField]
        public List<ChosenMealsSlotData> ChosenMealsSlotDataList { get; set; }
        
        
        
        /// <summary>
        /// 用來從料理格資訊陣列裡獲取料理，並且回傳給外部程式碼的方法。
        /// </summary>
        /// <returns> 回傳被選擇的料理。 </returns>
        public MealsSO ReduceRandomMealsQuantity()
        {
            var remainingSlotList = ChosenMealsSlotDataList;
            var selectedSlotIndex = 0;
            
            for (var i = remainingSlotList.Count - 1; i >= 0; i--)
            {;
                var selectedSlot = ChosenMealsSlotDataList[Random.Range(0, remainingSlotList.Count)];

                if (selectedSlot.IsLocked)
                {
                    selectedSlotIndex++;
                    remainingSlotList.Remove(selectedSlot);
                    continue;
                }
                
                if (selectedSlot.Meals is null || selectedSlot.Quantity <= 0)
                {
                    selectedSlotIndex++;
                    remainingSlotList.Remove(selectedSlot);
                    continue;
                }

                ChosenMealsSlotDataList[selectedSlotIndex] = new ChosenMealsSlotData
                {
                    IsLocked = false,
                    Meals = selectedSlot.Meals,
                    Quantity = selectedSlot.Quantity - 1
                };
                return selectedSlot.Meals;
            }
            
            Debug.Log($"{name} 裡沒有料理了 !");
            return null;
        }
    }
}