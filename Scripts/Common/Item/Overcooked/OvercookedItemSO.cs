using UnityEngine;

namespace Common.Item.Overcooked
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Overcooked Item", fileName = "New Data")]
    public sealed class OvercookedItemSO : ItemSO
    {
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
            public override void Remove() { }
        #endregion
    }
}