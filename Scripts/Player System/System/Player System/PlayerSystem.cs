using Input_System;
using Restaurant_System.Object.Creature.Customer.System.Main;
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
            
            InputSystem.OnPerformedBackpack += RequireBackpackUI;
            
            // CookwareSystem.TryAddItem += TryAddItem;
            Customer.GivingServingNote += TryAddItem;

            _itemDragSFX = itemDragSFX;
        }

        private void OnDestroy()
        {
            InputSystem.OnClickedLeftButton -= OnClickedLeftButton;
            InputSystem.OnClickedRightButton -= OnClickedRightButton;
            
            InputSystem.OnPerformedHotbar -= PerformHotbar;
            
            InputSystem.OnPerformedBackpack -= RequireBackpackUI;
            
            // CookwareSystem.TryAddItem -= TryAddItem;
            Customer.GivingServingNote -= TryAddItem;
        }
    }
}