using System.Collections.Generic;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using Database.Restaurant.Player.Unlock_Dish;
using Restaurant.Menu.Slot;
using UnityEngine;

namespace Restaurant.Menu
{
    [RequireComponent(typeof(MenuController))]
    public class Menu : MonoBehaviour
    {
        [field: Header("已解鎖的料理資料")]
        [field: SerializeField] private UnlockDishSO UnlockMainCourse { get; set; }
        [field: SerializeField] private UnlockDishSO UnlockDrink { get; set; }
        
        [field: Header("當日販售的料理資料")]
        [field: SerializeField] private TodayDishSO TodayMainCourse { get; set; }
        [field: SerializeField] private TodayDishSO TodayDrink { get; set; }
        
        [field: Header("格子的生成位置")]
        [field: SerializeField] private Transform UnlockDishSlotParent { get; set; }
        [field: SerializeField] private Transform SelectDishSlotParent { get; set; }
        
        [field: Header("格子的預製件")]
        [field: SerializeField] private GameObject UnlockDishSlotPrefab { get; set; }
        [field: SerializeField] private GameObject SelectDishSlotPrefab { get; set; }
        
        private List<GameObject> UnlockDishSlots { get; set; } = new();
        private List<GameObject> SelectDishSlots { get; set; } = new();

        private void OnEnable()
        {
            MenuController.OnOpenMenuButtonClickEvent += Init;
            UnlockDishSlot.OnButtonClickEvent += OnUnlockDishSlotClick;
        }
        
        private void OnDisable()
        {
            MenuController.OnOpenMenuButtonClickEvent -= Init;
            UnlockDishSlot.OnButtonClickEvent -= OnUnlockDishSlotClick;
        }

        private void Init()
        {
            foreach (var dish in UnlockMainCourse.UnlockDishes)
            {
                var newSlot = Instantiate(UnlockDishSlotPrefab, UnlockDishSlotParent);
                UnlockDishSlots.Add(newSlot);
                
                newSlot.GetComponent<UnlockDishSlot>().Init(dish);
            }

            foreach (var slot in TodayMainCourse.TodayDishSlots)
            {
                var newSlot = Instantiate(SelectDishSlotPrefab, SelectDishSlotParent);
                SelectDishSlots.Add(newSlot);
                
                newSlot.GetComponent<SelectDishSlot>().Init(slot);
            }
        }

        private void OnUnlockDishSlotClick(DishSO slotDish)
        {
            foreach (var slot in SelectDishSlots)
            {
                if (slot.GetComponent<SelectDishSlot>().AddDish(slotDish))
                    return;
            }
        }
    }
}