using System;
using System.Collections.Generic;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Restaurant.Kitchenware.Cook_Menu
{
    public class CookMenuManager : MonoBehaviour
    {
        [field: Header("今日販售料理的資料庫")]
        [field: SerializeField] private List<TodayDishSO> TodayDishes { get; set; }
        
        [field: Header("便利貼的生成位置")]
        [field: SerializeField] private Transform StickyNoteParent { get; set; }
        
        [field: Header("便利貼的預製件")]
        [field: SerializeField] private List<GameObject> StickyNotePrefabs { get; set; }

        private List<GameObject> StickyNoteObjs { get; set; } = new();
        
        public event Action<DishSO> OnClickStickyNoteEvent;

        private void OnDestroy()
        {
            foreach (var stickyNoteObj in StickyNoteObjs)
                stickyNoteObj.GetComponent<StickyNoteManager>().OnClickEvent -= OnClickStickyNote;
        }

        public void Init(CookUtensil utensil)
        {
            foreach (var todayDish in TodayDishes)
            {
                foreach (var dishSlot in todayDish.TodayDishSlots)
                {
                    if (!dishSlot.CheckDishExist())
                        continue;
                    
                    if (dishSlot.Dish.CookUtensil != utensil)
                        continue;

                    var stickyNoteObj = Instantiate(StickyNotePrefabs[Random.Range(0, StickyNotePrefabs.Count)], StickyNoteParent);
                    StickyNoteObjs.Add(stickyNoteObj);
                    
                    var stickyNoteManager = stickyNoteObj.GetComponent<StickyNoteManager>();
                    stickyNoteManager.Init(dishSlot);
                    stickyNoteManager.OnClickEvent += OnClickStickyNote;
                }
            }
        }

        private void OnClickStickyNote(DishSO selectDish)
        {
            OnClickStickyNoteEvent?.Invoke(selectDish);
        }
    }
}