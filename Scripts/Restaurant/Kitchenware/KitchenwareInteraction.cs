using Database.Restaurant.Dish;
using Restaurant.Kitchenware.Cook_Menu;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareInteraction : MonoBehaviour
    {
        [field: Header("UI 物件的生成位置")]
        [field: SerializeField] private Transform UIParent { get; set; }
        
        [field: Header("介面遮罩的預製件")]
        [field: SerializeField] private GameObject MaskPrefab { get; set; }
        private GameObject Mask { get; set; }
        
        [field: Header("選擇料理烹飪的選單")]
        [field: SerializeField] private GameObject CookMenuPrefab { get; set; }
        private GameObject CookMenu { get; set; }
        
        [field: Header("廚具種類的資料庫")]
        [field: SerializeField] private CookingUtensil CookingUtensil { get; set; }
        
        private KitchenwareManager KitchenwareManager { get; set; }
        
        private void Awake() => KitchenwareManager = GetComponent<KitchenwareManager>();

        private void OnEnable() => StickyNoteManager.OnClickEvent += OnStickyNoteClick;
        
        private void OnDisable() => StickyNoteManager.OnClickEvent -= OnStickyNoteClick;

        public void Interact()
        {
            if (CookMenu is null)
                OpenMenu();
            else
                CloseMenu();
        }
        
        private void OpenMenu()
        {
            Mask = Instantiate(MaskPrefab, UIParent);
            CookMenu = Instantiate(CookMenuPrefab, UIParent);
            CookMenu.GetComponent<CookMenuManager>().Init(KitchenwareManager, CookingUtensil);
        }

        public void CloseMenu()
        {
            if (CookMenu is null)
                return;
            
            Destroy(Mask);
            Mask = null;
            
            Destroy(CookMenu);
            CookMenu = null;
        }

        private void OnStickyNoteClick(DishSO selectDish)
        {
            Debug.Log(selectDish.Name);
        }
    }
}