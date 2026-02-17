using System;
using System.Collections.Generic;
using Input_System;
using Restaurant_System.Object.Cookware.System;
using Restaurant_System.Object.Creature.Customer.System.Main;
using UnityEngine;

namespace Player_System.System
{
    public partial class PlayerSystem : MonoBehaviour
    {
        private readonly Queue<Action> _cleanUpActions = new();

        private void Start()
        {
            #region Input
                InputSystem.OnClickedLeftButton += OnClickedLeftButton;
                _cleanUpActions.Enqueue(() => InputSystem.OnClickedLeftButton -= OnClickedLeftButton);
                
                InputSystem.OnClickedRightButton += OnClickedRightButton;
                _cleanUpActions.Enqueue(() => InputSystem.OnClickedRightButton -= OnClickedRightButton);
                
                InputSystem.OnPerformedHotbar += PerformHotbar;
                _cleanUpActions.Enqueue(() => InputSystem.OnPerformedHotbar -= PerformHotbar);
                
                InputSystem.OnPerformedBackpack += RequireBackpackUI;
                _cleanUpActions.Enqueue(() => InputSystem.OnPerformedBackpack -= RequireBackpackUI);
            #endregion
            
            #region TryAddItem
                CookwareSystem.TryAddItem += TryAddItem;
                _cleanUpActions.Enqueue(() => CookwareSystem.TryAddItem -= TryAddItem);
                
                Customer.GivingServingNote += TryAddItem;
                _cleanUpActions.Enqueue(() => Customer.GivingServingNote -= TryAddItem);
            #endregion
        }

        private void OnDestroy()
        {
            while (_cleanUpActions.Count > 0)
            {
                _cleanUpActions.Dequeue()?.Invoke();
            }
        }
    }
}