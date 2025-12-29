using System.Collections.Generic;
using UI_System.System.Main;
using UnityEngine;

namespace Item.Serving_Note
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Serving Note", fileName = "New Data")]
    public sealed class ServingNoteSO : ItemSO
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject servingNotePrefab;

        private Queue<ItemSO> _orderedItems = new();
        
        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use()
            {
                if (!UISystem.TryInitializeServingNoteUI(this, servingNotePrefab))
                    UISystem.ToggleServingNoteUI(this);
            }
        #endregion
        
        #region
            public void SetOrderedItems(Queue<ItemSO> orderedItems)
            {
                _orderedItems = new Queue<ItemSO>(orderedItems);
            }

            public void GetOrderedItems(out Queue<ItemSO> orderedItems)
            {
                orderedItems = _orderedItems;
            }
        #endregion
    }
}