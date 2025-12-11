using System;
using System.Collections.Generic;
using Economy_System.Child.Cookware_System.System.Main;
using Item;
using Player_System.Inventory.Child;
using UnityEngine;

namespace Player_System.Inventory.Main
{
    internal sealed class InventorySystem : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private HotbarSystem HotbarSystem;
        [field: SerializeField] private BackpackSystem BackpackSystem;
        
        private readonly Queue<Action> ActiveActions = new();

        private void OnEnable()
        {
            CookwareSystem.OnClickCompleteBubble += TryAddItem;
            ActiveActions.Enqueue(() => CookwareSystem.OnClickCompleteBubble -= TryAddItem);
        }
        
        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
        }

        private bool TryAddItem(ITem itemData)
        {
            HotbarSystem.TryAddItem(itemData, out var isSuccess);
            return isSuccess;
        }
    }
}