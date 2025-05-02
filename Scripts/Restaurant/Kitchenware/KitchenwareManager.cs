using Database.Restaurant.Dish;
using Interface;
using Restaurant.Kitchenware.Cook_Menu;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    [RequireComponent(typeof(KitchenwareCook))]
    public class KitchenwareManager : IPlayerInteractable
    {
        [field: Header("烹飪料理選擇選單的生成位置")]
        [field: SerializeField] private Transform CookMenuParent { get; set; }
        
        [field: Header("烹飪料理選擇選單的預製件")]
        [field: SerializeField] private GameObject CookMenuPrefab { get; set; }
        private GameObject CookMenu { get; set; }
        private CookMenu CookMenuScript { get; set; }
        
        [field: Header("家具的種類")]
        [field: SerializeField] private CookingUtensil CookingUtensil { get; set; }
        
        private DishSO CurrentCookDish { get; set; }
        
        private KitchenwareCook KitchenwareCook { get; set; }

        private void OnDisable()
        {
            if (CookMenuScript is not null)
                CookMenuScript.OnStickyNoteClickEvent -= OnStickyNoteClick;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            PlayerEnter(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            PlayerLeave(this);
        }

        public override void Interact()
        {
            if (CookMenu is null)
            {
                CookMenu = Instantiate(CookMenuPrefab, CookMenuParent);
                CookMenuScript = CookMenu.GetComponent<CookMenu>();
                CookMenuScript.Init(this, CookingUtensil);
                CookMenuScript.OnStickyNoteClickEvent += OnStickyNoteClick;
            }
            else
            {
                Destroy(CookMenu);
                CookMenu = null;
            }
        }

        private void OnStickyNoteClick(DishSO selectDish)
        {
            // TODO 關閉 CookMenu 並開始烹飪料理
        }
    }
}