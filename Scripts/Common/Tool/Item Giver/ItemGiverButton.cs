using System;
using Common.Item;
using Common.Object;
using UnityEngine;

namespace Common.Tool.Item_Giver
{
    [RequireComponent(typeof(Button))]
    internal sealed class ItemGiverButton : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private Button Button;
        
        [field: Header("Data")]
        [field: SerializeField] private ItemSO Item;
        
        internal event Action<ItemSO> OnClick;
        
        private void OnEnable() => Button.OnClicked += OnButtonClicked;
        private void OnDisable() => Button.OnClicked -= OnButtonClicked;
        private void OnButtonClicked() => OnClick?.Invoke(Item);
        public void SetInteractable(bool interactable) => Button.SetInteractable(interactable);
    }
}