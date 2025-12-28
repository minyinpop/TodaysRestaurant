using System;
using System.Collections.Generic;
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
            
            public static event Action<ServingNoteSO, GameObject> ServingNoteUIRequired;
            public override void Use() => ServingNoteUIRequired?.Invoke(this, servingNotePrefab);
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