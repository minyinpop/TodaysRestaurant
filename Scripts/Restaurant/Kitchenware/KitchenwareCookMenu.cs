using Database.Restaurant.Dish;
using Restaurant.Kitchenware.Cook_Menu;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    internal class KitchenwareCookMenu : MonoBehaviour
    {
        private CookUtensil CookUtensil { get; set; }
        private Transform CookMenuParent { get; set; }
        private GameObject CookMenuPrefab { get; set; }
        
        private GameObject CookMenuObj { get; set; }
        
        private CookMenuManager CookMenuManager { get; set; }
        
        private KitchenwareManager KitchenwareManager { get; set; }

        private void Awake()
        {
            KitchenwareManager = GetComponent<KitchenwareManager>();
        }

        private void OnDestroy()
        {
            if (CookMenuManager is not null)
                CloseCookMenu();
        }
        
        public void Init(CookUtensil utensil, Transform cookMenuParent, GameObject cookMenuPrefab)
        {
            CookUtensil = utensil;
            CookMenuParent = cookMenuParent;
            CookMenuPrefab = cookMenuPrefab;
        }

        public void OpenCookMenu()
        {
            if (CookMenuObj is not null)
                return;

            CookMenuObj = Instantiate(CookMenuPrefab, CookMenuParent);
            CookMenuManager = CookMenuObj.GetComponent<CookMenuManager>();
            CookMenuManager.Init(CookUtensil);
            CookMenuManager.OnClickStickyNoteEvent += OnClickStickyNote;
        }

        public void CloseCookMenu()
        {
            if (CookMenuObj is null)
                return;
            
            CookMenuManager.OnClickStickyNoteEvent -= OnClickStickyNote;
            CookMenuManager = null;
            
            Destroy(CookMenuObj);
            CookMenuObj = null;
        }

        private void OnClickStickyNote(DishSO selectDish)
        {
            CloseCookMenu();
            KitchenwareManager.OnChooseDish(selectDish);
        }
    }
}