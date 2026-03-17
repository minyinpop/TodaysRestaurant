using Common.Item.Data;

namespace Common.Item_Slot.Main
{
    public interface IItemSlot
    {
        public bool TryAddItem(IItem item);

        public bool TryGetItem(out IItem item);
    }
}