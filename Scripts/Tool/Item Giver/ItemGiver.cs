using System;
using System.Collections.Generic;
using Item;
using Item.Data;
using UnityEngine;

namespace Tool.Item_Giver
{
    internal sealed class ItemGiver : MonoBehaviour
    {
        [field: SerializeField] private ItemGiverButton[] Buttons;
        
        internal static event Func<ItemSO, bool> OnClick;
        
        private readonly Queue<Action> ActiveActions = new Queue<Action>();

        private void OnEnable()
        {
            foreach (var button in Buttons)
            {
                button.OnClick += OnButtonClicked;
                ActiveActions.Enqueue(() => button.OnClick -= OnButtonClicked);
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
            Debug.Log(isSuccess.Value ? $"成功添加 {Item.name}" : $"無法添加 {Item.name}");
        }
    }
}