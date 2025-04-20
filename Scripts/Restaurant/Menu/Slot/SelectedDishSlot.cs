using Database.Restaurant.Dish;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Menu.Slot
{
    public class SelectedDishSlot : MonoBehaviour
    {
        [field: SerializeField]
        public SelectedDishSlotEnum SlotType { get; private set; }
        
        [field: SerializeField]
        private Image DishImage { get; set; }
        
        [field: SerializeField]
        private TextMeshProUGUI ContentTMP { get; set; }
        
        
        
        public DishSO DishData { get; private set; }



        public void Init(DishSO dishData)
        {
            DishData = dishData;
            DishImage.sprite = DishData.Sprite;
            ContentTMP.text = $"{DishData.Name} x{DishData.MaxPortion}";
        }
    }
}