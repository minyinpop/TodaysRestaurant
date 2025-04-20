using System;
using Database.Restaurant.Dish;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Menu.Slot
{
    public class UnlockedDishSlot : MonoBehaviour
    {
        [field: SerializeField]
        private Image DishImage { get; set; }
        
        [field: SerializeField]
        private Button DishButton { get; set; }
        
        
        
        public static event Action<DishSO> OnUnlockedDishSlotClicked;
        
        private DishSO DishData { get; set; }
        
        
        
        private void OnEnable()
        {
            DishButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            DishButton.onClick.RemoveListener(OnButtonClicked);
        }
        
        
        
        public void Init(DishSO dishData)
        {
            DishData = dishData;
            DishImage.sprite = DishData.Sprite;
        }

        private void OnButtonClicked()
        {
            OnUnlockedDishSlotClicked?.Invoke(DishData);
        }
    }
}