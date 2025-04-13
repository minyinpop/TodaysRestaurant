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
            var remainingSlotList = new List<ChosenMealsSlotData>();

            foreach (var selectedSlotData in ChosenMealsSlotDataList)
            {
                if (selectedSlotData.IsLocked)
                    continue;

                if (selectedSlotData.Meals is null)
                    continue;
                
                if (selectedSlotData.Meals is not null && selectedSlotData.Quantity <= 0)
                    continue;
                
                remainingSlotList.Add(selectedSlotData);
            }
            
            var randomIndex = Random.Range(0, remainingSlotList.Count);
            var selectedMeal = remainingSlotList[randomIndex].Meals;

            for (var i = 0; i < ChosenMealsSlotDataList.Count; i++)
            {
                var selectedSlotData = ChosenMealsSlotDataList[i];
                
                if (selectedMeal != selectedSlotData.Meals)
                    continue;

                ChosenMealsSlotDataList[i] = new ChosenMealsSlotData
                {
                    IsLocked = false,
                    Meals = selectedSlotData.Meals,
                    Quantity = selectedSlotData.Quantity - 1
                };
                return selectedMeal;
            }

            Debug.Log($"{name} 的料理已經賣完了！");
            return null;
        }
    }
}