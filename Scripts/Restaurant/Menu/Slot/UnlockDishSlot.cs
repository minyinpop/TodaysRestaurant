using System;
using Database.Restaurant.Dish;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Menu.Slot
{
    public class UnlockDishSlot : MonoBehaviour
    {
        [field: Header("子物件的組件")]
        [field: SerializeField] private Button Button { get; set; }
        [field: SerializeField] private Image DishImage { get; set; }
        
        public static event Action<DishSO> OnButtonClickEvent;
        private DishSO Dish { get; set; }

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
            OnButtonClickEvent?.Invoke(Dish);
        }

        public void Init(DishSO newDish)
        {
            Dish = newDish;
            DishImage.sprite = Dish.Sprite;
        }
    }
}