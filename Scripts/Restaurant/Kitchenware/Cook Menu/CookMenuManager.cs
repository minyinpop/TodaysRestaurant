using System.Collections.Generic;
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
                    stickyNote.GetComponent<StickyNoteManager>().Init(todayDishSlot);
                }
            }
        }
    }
}