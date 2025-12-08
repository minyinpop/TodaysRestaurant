using System.Collections.Generic;
using System.Economy.Child.Cookware.System.Main;
using System.Player.Inventory.Child;
using Data.Item.Interface;
using UnityEngine;

namespace System.Player.Inventory.Main
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