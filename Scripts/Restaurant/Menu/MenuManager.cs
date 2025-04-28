using System.Collections.Generic;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using Database.Restaurant.Player.Unlock_Dish;
using Restaurant.Menu.Slot;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Menu
{
    [RequireComponent(typeof(MenuController))]
    public class MenuManager : MonoBehaviour
    {
        [field: Header("已解鎖的料理資料")]
        [field: SerializeField] private UnlockDishSO UnlockAppetizer { get; set; }
        [field: SerializeField] private UnlockDishSO UnlockMainCourse { get; set; }
        [field: SerializeField] private UnlockDishSO UnlockDessert { get; set; }
        [field: SerializeField] private UnlockDishSO UnlockDrink { get; set; }
        
        [field: Header("當日販售的料理資料")]
        [field: SerializeField] private TodayDishSO TodayAppetizer { get; set; }
        [field: SerializeField] private TodayDishSO TodayMainCourse { get; set; }
        [field: SerializeField] private TodayDishSO TodayDessert { get; set; }
        [field: SerializeField] private TodayDishSO TodayDrink { get; set; }
        
        [field: Header("格子的生成位置")]
        [field: SerializeField] private Transform UnlockDishSlotParent { get; set; }
        [field: SerializeField] private Transform SelectDishSlotParent { get; set; }
        
        [field: Header("格子的預製件")]
        [field: SerializeField] private GameObject UnlockDishSlotPrefab { get; set; }
        [field: SerializeField] private GameObject SelectDishSlotPrefab { get; set; }
        
        [field: Header("料理種類切換按鈕")]
        [field: SerializeField] private Button AppetizerButton { get; set; }
        [field: SerializeField] private Button MainCourseButton { get; set; }
        [field: SerializeField] private Button DessertButton { get; set; }
        [field: SerializeField] private Button DrinkButton { get; set; }
        
        private List<GameObject> UnlockDishSlots { get; set; } = new();
        private List<GameObject> SelectDishSlots { get; set; } = new();

        private void OnEnable()
        {
            MenuController.OnOpenMenuButtonClickEvent += Init;
            UnlockDishSlot.OnButtonClickEvent += OnUnlockDishSlotClick;
            
            AppetizerButton.onClick.AddListener(OnAppetizerButtonClick);
            MainCourseButton.onClick.AddListener(OnMainCourseButtonClick);
            DessertButton.onClick.AddListener(OnDessertButtonClick);
            DrinkButton.onClick.AddListener(OnDrinkButtonClick);
        }
        
        private void OnDisable()
        {
            MenuController.OnOpenMenuButtonClickEvent -= Init;
            UnlockDishSlot.OnButtonClickEvent -= OnUnlockDishSlotClick;
            
            AppetizerButton.onClick.RemoveListener(OnAppetizerButtonClick);
            MainCourseButton.onClick.RemoveListener(OnMainCourseButtonClick);
            DessertButton.onClick.RemoveListener(OnDessertButtonClick);
            DrinkButton.onClick.RemoveListener(OnDrinkButtonClick);
        }

        private void Init() => Refresh(UnlockMainCourse, TodayMainCourse);

        private void Refresh(UnlockDishSO newUnlockDish, TodayDishSO newTodayDish)
        {
            foreach (var slot in UnlockDishSlots)
                Destroy(slot);
            
            foreach (var slot in SelectDishSlots)
                Destroy(slot);
            
            UnlockDishSlots.Clear();
            SelectDishSlots.Clear();
            
            foreach (var dish in newUnlockDish.UnlockDishes)
            {
                var newSlot = Instantiate(UnlockDishSlotPrefab, UnlockDishSlotParent);
                UnlockDishSlots.Add(newSlot);
                
                newSlot.GetComponent<UnlockDishSlot>().Init(dish);
            }

            foreach (var slot in newTodayDish.TodayDishSlots)
            {
                var newSlot = Instantiate(SelectDishSlotPrefab, SelectDishSlotParent);
                SelectDishSlots.Add(newSlot);
                
                newSlot.GetComponent<SelectDishSlot>().Init(slot);
            }
        }
        
        private void OnAppetizerButtonClick() => Refresh(UnlockAppetizer, TodayAppetizer);
        
        private void OnMainCourseButtonClick() => Refresh(UnlockMainCourse, TodayMainCourse);
        
        private void OnDessertButtonClick() => Refresh(UnlockDessert, TodayDessert);
        
        private void OnDrinkButtonClick() => Refresh(UnlockDrink, TodayDrink);
        
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