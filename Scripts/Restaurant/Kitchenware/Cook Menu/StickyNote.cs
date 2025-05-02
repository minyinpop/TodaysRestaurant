using System;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware.Cook_Menu
{
    public class StickyNote : MonoBehaviour
    {
        [field: Header("自身的組件")]
        [field: SerializeField] private Button Button { get; set; }
        [field: SerializeField] private Image DishImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI DishNameTMP { get; set; }
        
        private TodayDishSlot TodayDish { get; set; }

        public static event Action<DishSO> OnClickEvent;
        
        private void OnEnable() => Button.onClick.AddListener(OnClick);
        
        private void OnDisable() => Button.onClick.RemoveListener(OnClick);

        public void Init(TodayDishSlot todayDish)
        {
            TodayDish = todayDish;
            
            DishImage.sprite = TodayDish.Dish.Sprite;
            DishNameTMP.text = TodayDish.Dish.Name;
        }

        private void OnClick()
        {
            TodayDish.TakeDish();
            OnClickEvent?.Invoke(TodayDish.Dish);
        }
    }
}