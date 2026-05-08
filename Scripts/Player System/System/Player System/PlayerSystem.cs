using Input_System;
using Restaurant_System.Object.Cookware.Object.Cook_Selection.System.Child;
using Restaurant_System.Object.Creature.Customer.System.Main;
using Tutorial_System;
using UnityEngine;

namespace Player_System.System.Player_System
{
    public partial class PlayerSystem : MonoBehaviour
    {
        private void Awake()
        {
            mainCamera = Camera.main;
            
            InputSystem.OnClickedLeftButton += OnClickedLeftButton;
            InputSystem.OnClickedRightButton += OnClickedRightButton;
            
            InputSystem.OnPerformedHotbar += PerformHotbar;
            
            // CookwareSystem.TryAddItem += TryAddItem;
            Customer.GivingServingNote += AddItem;

            RestaurantServesTutorialSystem.RemoveAllItems += RemoveAllItems;

            PutIngredientSystem.GiveRemainingItem += AddItem;

            _itemDragSFX = itemDragSFX;
        }

        private void OnDestroy()
        {
            InputSystem.OnClickedLeftButton -= OnClickedLeftButton;
            InputSystem.OnClickedRightButton -= OnClickedRightButton;
            
            InputSystem.OnPerformedHotbar -= PerformHotbar;
            
            // CookwareSystem.TryAddItem -= TryAddItem;
            Customer.GivingServingNote -= AddItem;
            
            RestaurantServesTutorialSystem.RemoveAllItems -= RemoveAllItems;
            
            PutIngredientSystem.GiveRemainingItem -= AddItem;
        }
    }
}