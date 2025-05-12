using System;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Kitchenware.Cook_Menu
{
    public class StickyNoteManager : MonoBehaviour
    {
        [field: Header("自身組件")]
        [field: SerializeField] private Button Button { get; set; }
        [field: SerializeField] private Image DishImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI NameTMP { get; set; }

        public event Action<DishSO> OnClickEvent;
        
        private TodayDishSlot DishSlot { get; set; }

        private void OnEnable() => Button.onClick.AddListener(OnClick);
        
        private void OnDisable() => Button.onClick.RemoveListener(OnClick);

        public void Init(TodayDishSlot dishSlot)
        {
            DishSlot = dishSlot;
            
            DishImage.sprite = DishSlot.Dish.Sprite;
            NameTMP.text = DishSlot.Dish.Name;
        }

        private void OnClick()
        {
            OnClickEvent?.Invoke(DishSlot.Dish);
            DishSlot.TakeDish();
        }
    }
}