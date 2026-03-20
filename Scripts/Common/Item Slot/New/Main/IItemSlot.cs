using Common.Item.Data;

namespace Common.Item_Slot.New.Main
{
    public interface IItemSlot<T> where T : IItem
    {
        public T Item { get; }
        
        public bool TryAddItem(T item);

        public bool TryGetItem(out T item);
    }
}