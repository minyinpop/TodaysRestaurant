using System.Collections.Generic;
using Database.Player.Unlocked_Dish;
using Database.Restaurant.Dish;
using Database.Restaurant.Selected_Dish;
using Restaurant.Menu.Slot;
using UnityEngine;
using UnityEngine.UI;
using SelectedDishSlot = Restaurant.Menu.Slot.SelectedDishSlot;

namespace Restaurant.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [field: Header("格子預製件"), SerializeField]
        private GameObject DishUnlockedSlot { get; set; }
        
        [field: SerializeField]
        private GameObject DishSelectedSlot01 { get; set; }
        
        [field: SerializeField]
        private GameObject DishSelectedSlot02 { get; set; }
        
        [field: SerializeField]
        private GameObject DishSelectedSlot03 { get; set; }
        
        
        
        [field: Header("格子生成點"), SerializeField]
        private Transform DishUnlockedSlotSpawnPoint { get; set; }
        
        [field: SerializeField]
        private Transform DishSelectedSlotSpawnPoint { get; set; }
        
        
        
        [field: Header("菜單控制按鈕"), SerializeField]
        private Button OpenButton { get; set; }
        
        [field: SerializeField]
        private Button CloseButton { get; set; }
        
        
        
        [field: Header("已解鎖的料理"), SerializeField]
        private UnlockedDishSO UnlockedMainCourseSO { get; set; }
        
        [field: SerializeField]
        private UnlockedDishSO UnlockedDrinkSO { get; set; }
        
        
        
        [field: Header("已選擇的料理"), SerializeField]
        private SelectedDishSO SelectedMainCourseSO { get; set; }
        
        [field: SerializeField]
        private SelectedDishSO SelectedDrinkSO { get; set; }



        private List<GameObject> SelectedDishSlotList { get; set; } = new();



        private void Start()
        {
            foreach (var dishData in UnlockedMainCourseSO.UnlockedDishList)
            {
                if (Instantiate(DishUnlockedSlot, DishUnlockedSlotSpawnPoint).TryGetComponent(out UnlockedDishSlot unlockedDishSlot))
                {
                    unlockedDishSlot.Init(dishData);
                }
            }
            
            foreach (var dishSlot in SelectedMainCourseSO.SelectedDishSlotList)
            {
                GameObject newSlot = null;
                
                if (dishSlot.Locked)
                {
                    newSlot = Instantiate(DishSelectedSlot01, DishSelectedSlotSpawnPoint);
                }
                else if (dishSlot.Dish is null)
                {
                    newSlot = Instantiate(DishSelectedSlot02, DishSelectedSlotSpawnPoint);
                }
                else if (dishSlot.Dish is not null)
                {
                    newSlot = Instantiate(DishSelectedSlot03, DishSelectedSlotSpawnPoint);
                    newSlot.GetComponent<SelectedDishSlot>().Init(dishSlot.Dish);
                }

                SelectedDishSlotList.Add(newSlot);
            }
        }
        
        
        
        private void OnEnable()
        {
            OpenButton.onClick.AddListener(OnOpenButtonClicked);
            CloseButton.onClick.AddListener(OnCloseButtonClicked);
            
            UnlockedDishSlot.OnUnlockedDishSlotClicked += OnUnlockedDishSlotClicked;
        }

        private void OnDisable()
        {
            OpenButton.onClick.RemoveListener(OnOpenButtonClicked);
            CloseButton.onClick.RemoveListener(OnCloseButtonClicked);
            
            UnlockedDishSlot.OnUnlockedDishSlotClicked -= OnUnlockedDishSlotClicked;
        }
        
        
        
        private void OnOpenButtonClicked()
        {
        }

        private void OnCloseButtonClicked()
        {
        }



        private void OnUnlockedDishSlotClicked(DishSO dishData)
        {
            // TODO 改成使用 for，並且要刪除就格子以及插入新格子
            
            // foreach (var dishSlot in SelectedDishSlotList)
            // {
            //     if (!dishSlot.TryGetComponent(out SelectedDishSlot selectedDishSlot))
            //         continue;
            //
            //     if (selectedDishSlot.SlotType != SelectedDishSlotEnum.UnlockedDishSlot02)
            //         continue;
            //     
            //     if (selectedDishSlot.DishData is not null)
            //         continue;
            //     
            //     selectedDishSlot.Init(dishData);
            //     return;
            // }
        }
    }
}