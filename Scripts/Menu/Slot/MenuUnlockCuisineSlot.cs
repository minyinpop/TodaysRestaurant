using Item.Category.Cuisine;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Slot
{
    /// <summary>
    /// 用於顯示當前玩家所解鎖的菜品的格子。
    /// </summary>
    public class MenuUnlockCuisineSlot : MonoBehaviour
    {
        [Header("組件"), Tooltip("用來顯示菜品的圖片組件。"), SerializeField]
        private Image cuisineImage;
        
        // 當前格子所儲存的菜品資料。
        private Cuisine _cuisineData;
        
        // 當玩家按下該格子後，所使用的事件廣播。
        // 目前為 MenuManager 訂閱該廣播。
        public static event System.Action<Cuisine> onClick;

        /// <summary>
        /// 用來更新當前格子所擁有的菜品的資料。
        /// </summary>
        /// <param name="newCuisineData"></param>
        public void Refresh(Cuisine newCuisineData)
        {
            _cuisineData = newCuisineData;
            cuisineImage.sprite = _cuisineData.Sprite;
        }

        /// <summary>
        /// 當玩家點擊了該格子後，所發生的事情的方法。
        /// 用於 Button 裡的 On Click()
        /// </summary>
        public void OnClick()
        {
            onClick?.Invoke(_cuisineData);
        }
    }
}
