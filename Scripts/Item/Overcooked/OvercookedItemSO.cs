using UnityEngine;

namespace Item.Overcooked
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Overcooked Item Data", fileName = "New Data")]
    public sealed class OvercookedItemSO : ItemSO
    {
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use() { }
        #endregion
    }
}