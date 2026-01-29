using System;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.Object.Food_Menu_UI.System.Main;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.System
{
    public sealed class FoodMenuUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private GameObject mask;
        [field: SerializeField] private FoodMenu foodMenu;

        private event Action _foodMenuCleanupAction;

        private void Awake()
        {
            if (mask == null)
            {
                Debug.Log($"{nameof(FoodMenuUISystem)} > {nameof(mask)} cannot be null.");
                 return;
            }

            if (foodMenu == null)
            {
                Debug.Log($"{nameof(FoodMenuUISystem)} > {nameof(foodMenu)} cannot be null.)");
            }
        }

        public void OpenUI(Action onConfirm)
        {
            mask.SetActive(true);
            
            foodMenu.gameObject.SetActive(true);
            foodMenu.OnConfirm += onConfirm;
            _foodMenuCleanupAction = () =>
            {
                foodMenu.OnConfirm -= onConfirm;
            };
        }

        public void CloseUI()
        {
            mask.SetActive(false);
            
            Destroy(foodMenu.gameObject);
            _foodMenuCleanupAction?.Invoke();
        }
    }
}