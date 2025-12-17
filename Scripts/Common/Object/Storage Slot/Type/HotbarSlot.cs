using Animation_System.DOTween;
using Animation_System.DOTween.Basic;
using Item.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Object.Storage_Slot.Type
{
    internal sealed class HotbarSlot : StorageSlot
    {
        [field: Header("Object")]
        [field: SerializeField] private RectTransform BackgroundRect;
        [field: SerializeField] private Image ItemImage;
        
        [field: Header("State")]
        [field: SerializeField] private bool Interactable = true;
        private bool IsSelected;
        
        [field: Header("Selection Colors")]
        [field: SerializeField] private Image BackgroundImage;
        [field: SerializeField] private Color SelectedColor;
        [field: SerializeField] private Color UnSelectedColor;
        [Space(6)]
        [field: SerializeField] private DoAnimation DoAnimation;
        [field: SerializeField] private DoScale SelectedAnimation;
        [field: SerializeField] private DoScale UnSelectedAnimation;
        
        private ItemSO ItemData;
        
        #region Storage Slot
            #region PointerEvent
                protected override void OnPointerEnter()
                {
                    if (!Interactable) return;
                    DoAnimation.DoScale_UI(BackgroundRect, SelectedAnimation);
                }

                protected override void OnPointerExit()
                {
                    if (!Interactable) return;
                    DoAnimation.DoScale_UI(BackgroundRect, UnSelectedAnimation);
                }
            #endregion

            #region Interaction
                public override void Selected()
                {
                    BackgroundImage.color = SelectedColor;
                    ItemData?.Selected();
                }

                public override void UnSelected()
                {
                    BackgroundImage.color = UnSelectedColor;
                    ItemData?.UnSelected();
                }

                public override void Use()
                {
                    ItemData?.Use();
                }
            #endregion

            #region Item
                public override void TryAddItem(ItemSO item, out bool isSuccess)
                {
                    if (item is null) { isSuccess = false; return; }
                    if (ItemData is not null) { isSuccess = false; return; }
                    
                    ItemData = item;
                    ItemImage.sprite = item.ItemSprite;
                    ItemImage.gameObject.SetActive(true);
                    isSuccess = true;
                }

                public override void GetItem(out ItemSO item)
                {
                    if (ItemData is null) item = null;
                    item = ItemData;
                    ItemData = null;
                    ItemImage.gameObject.SetActive(false);
                    ItemImage.sprite = null;
                }
            #endregion

            #region Status
                public override bool IsEmpty()
                {
                    return ItemData is null;
                }
            #endregion
        #endregion
    }
}