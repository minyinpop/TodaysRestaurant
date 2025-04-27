using System;
using UnityEngine;
using UnityEngine.UI;

namespace Restaurant.Menu
{
    [RequireComponent(typeof(Menu))]
    public class MenuController : MonoBehaviour
    {
        [field: Header("菜單的遊戲物件")]
        [field: SerializeField] private GameObject OpenMenu { get; set; }
        [field: SerializeField] private GameObject CloseMenu { get; set; }
        
        [field: Header("菜單控制按鈕")]
        [field: SerializeField] private Button OpenMenuButton { get; set; }
        [field: SerializeField] private Button CloseMenuButton { get; set; }
        
        public static event Action OnOpenMenuButtonClickEvent;
        
        private void OnEnable()
        {
            OpenMenuButton.onClick.AddListener(OnOpenMenuButtonClick);
            CloseMenuButton.onClick.AddListener(OnCloseMenuButtonClick);
        }
        
        private void OnDisable()
        {
            OpenMenuButton.onClick.RemoveListener(OnOpenMenuButtonClick);
            CloseMenuButton.onClick.RemoveListener(OnCloseMenuButtonClick);
        }

        private void OnOpenMenuButtonClick()
        {
            OpenMenu.SetActive(true);
            CloseMenu.SetActive(false);
            
            OnOpenMenuButtonClickEvent?.Invoke();
        }

        private void OnCloseMenuButtonClick()
        {
            // TODO 檢測玩家是否選擇了所有的料理
            
            Destroy(gameObject);
        }
    }
}