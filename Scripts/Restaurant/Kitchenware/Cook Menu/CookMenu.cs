using System;
using System.Collections.Generic;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Restaurant.Kitchenware.Cook_Menu
{
    public class CookMenu : MonoBehaviour
    {
        [field: Header("今日販售的料理的資料")]
        [field: SerializeField] private List<TodayDishSO> TodayDishes { get; set; }
        
        [field: Header("便利貼的生成位置")]
        [field: SerializeField] private Transform StickyNoteParent { get; set; }
        
        [field: Header("便利貼的預製件")]
        [field: SerializeField] private List<GameObject> StickyNotePrefabs { get; set; }
        private List<GameObject> StickyNotes { get; set; } = new();
        
        [field: Header("自身組件")]
        [field: SerializeField] private Button CraftButton { get; set; }
        [field: SerializeField] private Button CloseButton { get; set; }
        
        private KitchenwareManager KitchenwareManager { get; set; }
        
        private CookingUtensil CookingUtensil { get; set; }

        public event Action<DishSO> OnStickyNoteClickEvent;
        
        private void OnEnable() => StickyNote.OnClickEvent += OnStickyNoteClick;
        
        private void OnDisable() => StickyNote.OnClickEvent -= OnStickyNoteClick;

        public void Init(KitchenwareManager kitchenware, CookingUtensil utensil)
        {
            KitchenwareManager = kitchenware;
            CookingUtensil = utensil;

            foreach (var todayDish in TodayDishes)
            {
                foreach (var todayDishSlot in todayDish.TodayDishSlots)
                {
                    if (!todayDishSlot.CheckDishExist())
                        continue;
                    
                    if (todayDishSlot.Dish.CookingUtensil != CookingUtensil)
                        continue;

                    var stickyNote = Instantiate(StickyNotePrefabs[Random.Range(0, StickyNotePrefabs.Count)], StickyNoteParent);
                    stickyNote.GetComponent<StickyNote>().Init(todayDishSlot);
                    
                    StickyNotes.Add(stickyNote);
                }
            }
        }

        private void OnStickyNoteClick(DishSO selectDish)
        {
            OnStickyNoteClickEvent?.Invoke(selectDish);
        }
    }
}