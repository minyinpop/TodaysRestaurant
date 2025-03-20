using Item.Category.Cuisine;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    /// <summary>
    /// 用於顯示當前玩家所解鎖的菜品的格子。
    /// </summary>
    public class MenuUnlockCuisineSlot : MonoBehaviour
    {
        [Header("組件"), Tooltip("用來顯示料理的圖片組件。"), SerializeField]
        private Image cuisineImage;
        
        // 當前格子所儲存的菜品資料。
        private Cuisine _cuisineData;

        /// <summary>
        /// 用來更新當前格子所擁有的菜品的資料。
        /// </summary>
        /// <param name="newCuisineData"></param>
        public void Refresh(Cuisine newCuisineData)
        {
            _cuisineData = newCuisineData;
            cuisineImage.sprite = _cuisineData.Sprite;
        }
    }
}
