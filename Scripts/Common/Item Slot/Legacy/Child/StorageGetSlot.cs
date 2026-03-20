using System;
using Common.Item_Slot.Legacy.Main;
using Common.Item.Data;
using UnityEngine;

namespace Common.Item_Slot.Legacy.Child
{
    public sealed class StorageGetSlot : StorageSlot
    {
        #region PointerEvent
            protected override void OnPointerEnter()
            {
                if (!interactable) return;
                animation.DoScale_UI(slotRect, scaleUpSettings);
            }

            protected override void OnPointerExit()
            {
                if (!interactable) return;
                animation.DoScale_UI(slotRect, scaleDownSettings);
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
                _currentItem = itemData ?? throw new NotImplementedException();
                itemImage.sprite = itemData.ItemSprite;
                itemImage.gameObject.SetActive(true);
                slotRect.localScale = Vector2.one * 1.25f;
                animation.DoScale_UI(slotRect, scaleDownSettings,
                    onComplete: () =>
                    {
                        interactable = true;
                        onComplete?.Invoke();
                    });
            }
        #endregion
    }
}