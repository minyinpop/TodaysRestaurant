using System;
using Common.Item;
using Common.Object.Storage_Slot.Main;
using UnityEngine;

namespace Common.Object.Storage_Slot.Child
{
    public sealed class StorageGetSlot : StorageSlot
    {
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!Interactable) return;
                DoAnimation.DoScale_UI(SlotRect, ScaleUpSettings);
            }

            protected override void OnPointerExit()
            {
                if (!Interactable) return;
                DoAnimation.DoScale_UI(SlotRect, ScaleDownSettings);
            }
        #endregion
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
        
        #region Item
            public override void TryAddItem(ItemSO itemData, Action onComplete)
            {
                _currentItemData = itemData ?? throw new NotImplementedException();
                ItemImage.sprite = itemData.ItemSprite;
                ItemImage.gameObject.SetActive(true);
                SlotRect.localScale = Vector2.one * 1.25f;
                DoAnimation.DoScale_UI(SlotRect, ScaleDownSettings,
                    onComplete: () =>
                    {
                        Interactable = true;
                        onComplete?.Invoke();
                    });
            }
        #endregion
    }
}