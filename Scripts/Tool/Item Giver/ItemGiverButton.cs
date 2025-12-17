using System;
using Common.Object;
using Item;
using Item.Data;
using UnityEngine;

namespace Tool.Item_Giver
{
    [RequireComponent(typeof(Button))]
    internal sealed class ItemGiverButton : MonoBehaviour
    {
        [field: SerializeField] private ItemSO Item;
        
        private Button Button;
        
        internal event Action<ItemSO> OnClick;
        
        private void Awake() => Button = GetComponent<Button>();
        private void OnEnable() => Button.OnClick += OnButtonClicked;
        private void OnDisable() => Button.OnClick -= OnButtonClicked;
        private void OnButtonClicked() => OnClick?.Invoke(Item);
    }
}