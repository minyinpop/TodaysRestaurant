using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Menu.Slot
{
    public class SelectDishSlot : MonoBehaviour
    {
        [field: Header("自己本身的組件")]
        [field: SerializeField] private Image SlotImage { get; set; }
        [field: SerializeField] private Button Button { get; set; }
        
        [field: Header("子物件的組件")]
        [field: SerializeField] private Image DishImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI ContentTMP { get; set; }
        
        [field: Header("格子的底圖")]
        [field: SerializeField] private Sprite SelectDishSlot01 { get; set; }
        [field: SerializeField] private Sprite SelectDishSlot02 { get; set; }
        [field: SerializeField] private Sprite SelectDishSlot03 { get; set; }
        
        private TodayDishSlot DishSlot { get; set; }
        
        private void OnEnable()
        {
            Button.onClick.AddListener(OnButtonClick);
        }
        
        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            DishSlot.ClearData();
            
            SlotImage.sprite = SelectDishSlot02;
            Button.interactable = false;
                
            DishImage.gameObject.SetActive(false);
            ContentTMP.gameObject.SetActive(false);
        }

        public void Init(TodayDishSlot newDishSlot)
        {
            DishSlot = newDishSlot;

            if (DishSlot.Lock)
            {
                SlotImage.sprite = SelectDishSlot01;
                Button.interactable = false;
                
                DishImage.gameObject.SetActive(false);
                ContentTMP.gameObject.SetActive(false);
            }
            else if (DishSlot.Dish is null)
            {
                SlotImage.sprite = SelectDishSlot02;
                Button.interactable = false;
                
                DishImage.gameObject.SetActive(false);
                ContentTMP.gameObject.SetActive(false);
            }
            else if (DishSlot.Dish is not null)
            {
                SlotImage.sprite = SelectDishSlot03;
                Button.interactable = true;
                
                DishImage.gameObject.SetActive(true);
                ContentTMP.gameObject.SetActive(true);

                DishImage.sprite = DishSlot.Dish.Sprite;
                ContentTMP.text = $"{DishSlot.Dish.Name} x {DishSlot.Portion}";
            }
        }

        public bool AddDish(DishSO newDish)
        {
            if (DishSlot.Lock || DishSlot.Dish is not null)
                return false;

            DishSlot.AddDish(newDish);
            
            SlotImage.sprite = SelectDishSlot03;
            Button.interactable = true;
            
            DishImage.gameObject.SetActive(true);
            ContentTMP.gameObject.SetActive(true);

            DishImage.sprite = DishSlot.Dish.Sprite;
            ContentTMP.text = $"{DishSlot.Dish.Name} x {DishSlot.Portion}";
            return true;
        }
    }
}