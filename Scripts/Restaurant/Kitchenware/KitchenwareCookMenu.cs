using Restaurant.Kitchenware.Cook_Menu;
using UnityEngine;

namespace Restaurant.Kitchenware
{
    public class KitchenwareCookMenu : MonoBehaviour
    {
        [field: Header("廚具的種類")]
        [field: SerializeField] private CookUtensil CookUtensil { get; set; }
        
        [field: Header("選擇烹飪料介面的生成位置")]
        [field: SerializeField] private Transform CookMenuParent { get; set; }
        
        [field: Header("選擇烹飪料介面的預製件")]
        [field: SerializeField] private GameObject CookMenuPrefab { get; set; }
        private GameObject CookMenuObj { get; set; }
        private CookMenuManager CookMenuManager { get; set; }

        public void OpenCookMenu()
        {
            if (CookMenuObj is not null)
                return;

            CookMenuObj = Instantiate(CookMenuPrefab, CookMenuParent);
            CookMenuManager = CookMenuObj.GetComponent<CookMenuManager>();
            CookMenuManager.Init(CookUtensil);
        }

        public void CloseCookMenu()
        {
            Destroy(CookMenuObj);
            
            CookMenuObj = null;
            CookMenuManager = null;
        }
    }
}