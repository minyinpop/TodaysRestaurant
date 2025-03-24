using Item.Category.Cuisine;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Slot
{
    /// <summary>
    /// 用於顯示當前玩家所解鎖的料理的格子。
    /// </summary>
    public class MenuUnlockCuisineSlot : MonoBehaviour
    {
        [Header("組件"), Tooltip("用來顯示料理圖片的圖片組件。"), SerializeField]
        private Image cuisineImage;
        
        // 當格子被點擊後，所使用的廣播。
        // 目前為 MenuManager 做訂閱。
        public static event System.Action<Cuisine> onClick;
        
        // 格子裡所儲存的料理資料。
        private Cuisine _cuisineData;

        /// <summary>
        /// 用來執行 Button 的 On Click() 邏輯。
        /// 掛載在 Button 組件裡的 On Click() 做使用。
        /// </summary>
        public void OnClick()
        {
            onClick?.Invoke(_cuisineData);
        }
        
        /// <summary>
        /// 刷新格子的顯示。
        /// </summary>
        public void Refresh(Cuisine newCuisineData)
        {
            _cuisineData = newCuisineData;
            cuisineImage.sprite = _cuisineData.Sprite;
        }
    }
}
