using Common.Item.Data;

namespace Common.Item_Slot.New.Main
{
    public interface IItemSlot
    {
        public IItem Item { get; }

        public bool AddItem(IItem item);

        public bool GetItem(out IItem item);

        public bool ChangeItem(IItem targetItem, out IItem slotItem);
    }
}