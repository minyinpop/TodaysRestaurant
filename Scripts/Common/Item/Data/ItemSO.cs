using Audio_System.Data;
using UnityEngine;

namespace Common.Item.Data
{
    public abstract class ItemSO : ScriptableObject, IItem
    {
        [field: Header("父資料 - 物品編號")]
        [field: SerializeField] private int itemID;
                                public int ItemID => itemID;
        
        [field: Header("父資料 - 物品名稱")]
        [field: SerializeField] private string itemName;
                                public string ItemName => itemName;
                                
        [field: Header("父資料 - 物品圖片")]
        [field: SerializeField] private Sprite itemSprite;
                                public Sprite ItemSprite => itemSprite;
                                
        [field: Header("子資料 - 拿取音效")]
        [field: SerializeField] private PlaySFXData takeSFX;
                                public PlaySFXData TakeSFX => takeSFX;

        [field: Header("子資料 - 拿取特效")]
        [field: SerializeField] private ParticleSystem takeVFX;
                                public ParticleSystem TakeVFX => takeVFX;
        
        #region Interaction
            public abstract void Selected();
            public abstract void UnSelected();
            public abstract void Use();
            public abstract void Remove();
        #endregion
    }
}