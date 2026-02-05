using Common.Object.Storage_Slot.Main;

namespace Common.Object.Storage_Slot.Child
{
    public sealed class LevelInformationSlot : StorageSlot
    {
        #region Storage Slot
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
                public override void Selected()
                {
                }

                public override void UnSelected()
                {
                }

                public override void Use()
                {
                }
            #endregion
        #endregion
        
    }
}