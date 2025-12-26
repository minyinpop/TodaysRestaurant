using System;
using Common.Object;
using Item;
using UnityEngine;

namespace Tool.Item_Giver
{
    [RequireComponent(typeof(Button))]
    internal sealed class ItemGiverButton : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private Button Button;
        
        [field: Header("Data")]
        [field: SerializeField] private ItemSO Item;
        
        internal event Action<ItemSO> OnClick;
        
        private void OnEnable() => Button.onClick += OnButtonClicked;
        private void OnDisable() => Button.onClick -= OnButtonClicked;
        private void OnButtonClicked() => OnClick?.Invoke(Item);
        public void SetInteractable(bool interactable) => Button.SetInteractable(interactable);
    }
}