using UnityEngine;
using UnityEngine.UI;

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





        private void OnEnable()
        {
            OpenButton.onClick.AddListener(OnOpenButtonClicked);
            CloseButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            OpenButton.onClick.RemoveListener(OnOpenButtonClicked);
            CloseButton.onClick.RemoveListener(OnCloseButtonClicked);
        }
        
        
        
        private void OnOpenButtonClicked()
        {
        }

        private void OnCloseButtonClicked()
        {
        }
    }
}