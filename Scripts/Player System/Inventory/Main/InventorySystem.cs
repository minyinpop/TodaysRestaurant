using System;
using System.Collections.Generic;
using Item;
using Player_System.Inventory.Child;
using Restaurant_System.Object.Cookware.System;
using Tool.Item_Giver;
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
            ItemGiver.OnClick += TryAddItem;
            ActiveActions.Enqueue(() => ItemGiver.OnClick -= TryAddItem);
        }
        
        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
        }

        private bool TryAddItem(ITem item)
        {
            HotbarSystem.TryAddItem(item, out var isSuccess);
            return isSuccess;
        }
        
        private bool TryAddItem(ItemSO item)
        {
            HotbarSystem.TryAddItem(item, out var isSuccess);
            return isSuccess;
        }
    }
}