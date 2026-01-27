using System;
using UI_System.System.Child.Food_Menu_UI_System.Food_Menu_UI.System.Main;
using UI_System.System.Main;
using UnityEngine;

namespace UI_System.System.Child.Food_Menu_UI_System
{
    public sealed class FoodMenuUISystem : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject uiPrefab;
        [field: SerializeField] private RectTransform uiParent;
        
        [field: Header("Mask")]
        [field: SerializeField] private GameObject maskImage;
        
        private GameObject _ui;
        private FoodMenu _foodMenu;

        private event Action _foodMenuCleanupAction;

        public void SpawnUI(Action onConfirm)
        {
            maskImage.SetActive(true);
            
            _ui = Instantiate(uiPrefab, uiParent);
            _foodMenu = _ui.GetComponent<FoodMenu>();

            _foodMenu.OnConfirm += onConfirm;
            _foodMenuCleanupAction = () =>
            {
                _foodMenu.OnConfirm -= onConfirm;
            };
        }

        public void DestroyUI()
        {
            Destroy(_ui);
            _ui = null;
            
            maskImage.SetActive(false);

            _foodMenuCleanupAction?.Invoke();
        }
    }
}