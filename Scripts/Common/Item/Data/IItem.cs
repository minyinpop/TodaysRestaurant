using UnityEngine;

namespace Common.Item.Data
{
    public interface IItem
    {
        public int ItemID { get; }
        
        public string ItemName { get; }
        
        public Sprite ItemSprite { get; }
        

        public void Selected();

        public void UnSelected();

        public void Use();
        
        public void Remove();
    }
}