using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Menu.Slot
{
    // ==================================================
    // 用於管理被解鎖的料理的格子的程式碼。
    // ==================================================
    public class UnlockMealsSlot : MonoBehaviour
    {
        [Header("顯示相關"), Tooltip("用於顯示料理的圖片組件。"), SerializeField]
        private Image mealsImage;
    }
}