using UnityEngine;

namespace Common.Item.Data
{
    public abstract class ItemSO : ScriptableObject, IItem
    {
        [field: Header("Information")]
        [field: SerializeField] private int itemID;
                                public int ItemID => itemID;
        [field: SerializeField] private string itemName;
                                public string ItemName => itemName;
        [field: SerializeField] private Sprite itemSprite;
                                public Sprite ItemSprite => itemSprite;
        
        #region Interaction
            public abstract void Selected();
            public abstract void UnSelected();
            public abstract void Use();
            public abstract void Remove();
        #endregion
    }
}