using UnityEngine;

namespace Restaurant.Menu
{
    // ==================================================
    // 用於管理菜單的程式碼。
    // ==================================================
    public class MenuManager : MonoBehaviour
    {
        [Header("已解所料理的格子"), Tooltip("已解鎖的 料理選擇 格子預製件。"), SerializeField]
        private GameObject unlockedMealsSlotPrefab;
        
        [Tooltip("已上鎖的 料理選擇 格子預製件。"), SerializeField]
        private GameObject lockedMealsSlotPrefab;
        
        
        
        [Header("選擇料理的格子"), Tooltip("已解鎖且有 選擇料理 的格子預製件。"), SerializeField]
        private GameObject unlockedChosenMealsSlotWithMealsPrefab;
        
        [Tooltip("已解鎖但沒有 選擇料理 的格子預製件。"), SerializeField]
        private GameObject unlockedChosenMealsSlotWithoutMealsPrefab;
        
        [Tooltip("未解鎖的 選擇料理 的格子預製件。"), SerializeField]
        private GameObject lockedChosenMealsSlotPrefab;

        public void OnOpenButtonPressed()
        {
        }
    }
}