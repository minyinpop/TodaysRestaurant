using System.Collections.Generic;
using UI_System.Main;
using UnityEngine;

namespace Common.Item.Serving_Note
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Serving Note", fileName = "New Data")]
    public sealed class ServingNoteSO : ItemSO
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject servingNotePrefab;

        private readonly List<ItemSO> _orderedItems = new();
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use()
            {
                if (!UISystem.TryInitializeServingNoteUI(this, servingNotePrefab))
                    UISystem.ToggleServingNoteUI(this);
            }
            public override void Remove()
            {
                UISystem.RemoveServingNoteUI(this);
            }
        #endregion
        
        #region Ordered Items
            public void SetOrderedItems(List<ItemSO> orderedItems)
            {
                Reset();
                foreach (var item in orderedItems)
                    _orderedItems.Add(item);
            }

            public void GetOrderedItems(out List<ItemSO> orderedItems)
            {
                orderedItems = _orderedItems;
            }
        #endregion

        #region States
            public void Reset()
            {
                _orderedItems.Clear();
            }
        #endregion
    }
}