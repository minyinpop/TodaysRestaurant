using Common.Item.Data;

namespace Common.Item_Slot.New.Main
{
    public interface IItemSlot
    {
        public IItem Item { get; }
        
        public bool TryAddItem(IItem item);

        public bool TryGetItem(out IItem item);
    }
}