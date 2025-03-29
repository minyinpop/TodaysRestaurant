using DataBase.Menu.ChooseCuisine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kitchenware.Cuisine_Choose_UI
{
    /// <summary>
    /// 用於廚俱的料理的選擇介面的便利貼的類。
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class StickyNote : MonoBehaviour
    {
        [Header("組件"), Tooltip("用來顯示料理圖案的圖片組件。"), SerializeField]
        private Image cuisineImage;
        
        [Tooltip("用來顯示料理名稱的文字組件。"), SerializeField]
        private TextMeshProUGUI cuisineNameTMP;

        [Tooltip("用來顯示料理剩餘份數的文字組件。"), SerializeField]
        private TextMeshProUGUI cuisineRemainingTMP;
        
        // 料理的選擇的介面的類，用於回傳資訊。
        private KitchenwareCuisineChooseUI _kitchenwareCuisineChooseUI;
        
        // 料理資料的暫存。
        private PlayerChooseCuisineSlotData _slotData;

        /// <summary>
        /// 用來初始化便利貼的方法。
        /// </summary>
        /// <param name="kitchenwareCuisineChooseUI"> 用於回傳資訊。 </param>
        /// <param name="newData"> 新傳入的玩家選擇的料理的資訊。 </param>
        public void InitStickyNote(KitchenwareCuisineChooseUI kitchenwareCuisineChooseUI , PlayerChooseCuisineSlotData newData)
        {
            _kitchenwareCuisineChooseUI = kitchenwareCuisineChooseUI;
            _slotData = newData;
            
            Refresh();
        }

        /// <summary>
        /// 用於刷新便利貼顯示的方法。
        /// </summary>
        private void Refresh()
        {
            if (_slotData.cuisineData is null)
                return;

            if (cuisineImage is not null)
            {
                cuisineImage.gameObject.SetActive(true);
                cuisineImage.sprite = _slotData.cuisineData.Sprite;
            }

            if (cuisineNameTMP is not null)
            {
                cuisineNameTMP.gameObject.SetActive(true);
                cuisineNameTMP.text = _slotData.cuisineData.Name;
            }

            if (cuisineRemainingTMP is not null)
            {
                cuisineRemainingTMP.gameObject.SetActive(true);
                cuisineRemainingTMP.text = _slotData.cuisineRemaining.ToString();
            }
        }

        /// <summary>
        /// 用於 Button 組件的 On Click() 做使用。
        /// </summary>
        public void OnClick()
        {
            _kitchenwareCuisineChooseUI.OnStickyNoteClick(_slotData.cuisineData);
        }
    }
}
