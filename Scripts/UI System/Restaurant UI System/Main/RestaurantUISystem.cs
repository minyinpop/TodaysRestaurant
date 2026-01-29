using System;
using System.Collections.Generic;
using Common.Item;
using Common.Item.Serving_Note;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System;
using UI_System.Restaurant_UI_System.Child.Food_Menu_UI_System.System;
using UI_System.Restaurant_UI_System.Child.Serving_Note_UI_System;
using UI_System.Restaurant_UI_System.Child.Serving_Note_UI_System.System;
using UnityEngine;

namespace UI_System.Restaurant_UI_System.Main
{
    public sealed class RestaurantUISystem : MonoBehaviour
    {
        [field: Header("Objects")]
        [field: SerializeField] private Transform foodMenuUISystemParent;
                                private static FoodMenuUISystem _foodMenuUISystem;
        [field: SerializeField] private Transform servingNoteUISystemParent;
                                private static ServingNoteUISystem _servingNoteUISystem;
                                
        private void Awake()
        {
            if (foodMenuUISystemParent == null)
            {
                Debug.Log($"{nameof(RestaurantUISystem)} > {nameof(foodMenuUISystemParent)} cannot be null.");
                return;
            }
            
            if (servingNoteUISystemParent == null)
            {
                Debug.Log($"{nameof(RestaurantUISystem)} > {nameof(servingNoteUISystemParent)} cannot be null.");
                return;
            }

            _foodMenuUISystem = foodMenuUISystemParent.GetComponent<FoodMenuUISystem>();
            _servingNoteUISystem = servingNoteUISystemParent.GetComponent<ServingNoteUISystem>();
        }
        
        #region Food Menu
            public static void OpenFoodMenu(Action onConfirm)
            {
                _foodMenuUISystem.OpenUI(onConfirm);
            }
            
            public static void CloseFoodMenu()
            {
                _foodMenuUISystem.CloseUI();
            }
        #endregion
        
        #region ServingNote
            public static bool TryInitializeServingNoteUI(ServingNoteSO servingNoteData, GameObject prefab)
            {
                return _servingNoteUISystem.TryInitialize(servingNoteData, prefab);
            }
            
            public static void ToggleServingNoteUI(ServingNoteSO servingNoteData)
            {
                _servingNoteUISystem.ToggleUI(servingNoteData);
            }
            
            public static void GetServingNoteItems(ServingNoteSO servingNoteData, out List<ItemSO> servingNoteItems)
            {
                _servingNoteUISystem.GetServingNoteItems(servingNoteData, out servingNoteItems);
            }
            
            public static void RemoveServingNoteUI(ServingNoteSO servingNoteData)
            {
                _servingNoteUISystem.RemoveServingNoteUI(servingNoteData); 
            }
        #endregion
    }
}