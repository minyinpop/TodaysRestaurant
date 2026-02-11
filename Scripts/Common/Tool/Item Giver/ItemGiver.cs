using System;
using System.Collections.Generic;
using Common.Data.Item;
using UnityEngine;

namespace Common.Tool.Item_Giver
{
    internal sealed class ItemGiver : MonoBehaviour
    {
        [field: SerializeField] private ItemGiverButton[] Buttons;
        
        internal static event Func<ItemSO, bool> OnClick;

        private readonly Queue<Action> ActiveActions = new();

        private void OnEnable()
        {
            foreach (var button in Buttons)
            {
                button.OnClick += OnButtonClicked;
                ActiveActions.Enqueue(() => button.OnClick -= OnButtonClicked);
                button.SetInteractable(true);
            }
        }
        
        private void OnDisable()
        {
            while (ActiveActions.Count > 0) ActiveActions.Dequeue()?.Invoke();
        }

        private void OnButtonClicked(ItemSO Item)
        {
            var isSuccess = OnClick?.Invoke(Item);
            if (isSuccess is null) return;
        }
    }
}