using System.Collections.Generic;
using Database.Restaurant.Dish;
using Database.Restaurant.Menu;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Restaurant.Kitchenware.Cook_Menu
{
    public class CookMenuManager : MonoBehaviour
    {
        [field: Header("今日販售的料理的資料")]
        [field: SerializeField] private List<TodayDishSO> TodayDishes { get; set; }
        
        [field: Header("便利貼的生成位置")]
        [field: SerializeField] private Transform StickyNoteParent { get; set; }
        
        [field: Header("便利貼的預製件")]
        [field: SerializeField] private List<GameObject> StickyNotePrefabs { get; set; }
        
        [field: Header("自身組件")]
        [field: SerializeField] private Button CraftButton { get; set; }
        [field: SerializeField] private Button CloseButton { get; set; }
        
        private KitchenwareManager KitchenwareManager { get; set; }
        private CookingUtensil CookingUtensil { get; set; }
        
        private List<StickyNoteManager> StickyNoteManagers { get; set; } = new();

        private void OnDisable()
        {
            foreach (var stickyNoteManager in StickyNoteManagers)
                stickyNoteManager.OnClickEvent -= ChooseDish;
        }

        public void Init(KitchenwareManager manager,CookingUtensil utensil)
        {
            KitchenwareManager = manager;
            CookingUtensil = utensil;

            foreach (var todayDish in TodayDishes)
            {
                foreach (var todayDishSlot in todayDish.TodayDishSlots)
                {
                    if (!todayDishSlot.CheckDishExist())
                        continue;
                    
                    if (todayDishSlot.Dish.CookingUtensil != CookingUtensil)
                        continue;

                    var stickyNoteManager = Instantiate(StickyNotePrefabs[Random.Range(0, StickyNotePrefabs.Count)], StickyNoteParent).GetComponent<StickyNoteManager>();
                    StickyNoteManagers.Add(stickyNoteManager);
                    
                    stickyNoteManager.Init(todayDishSlot);
                    stickyNoteManager.OnClickEvent += ChooseDish;
                }
            }
        }

        private void ChooseDish(DishSO chooseDish) => KitchenwareManager.ChooseDishAndCook(chooseDish);
    }
}