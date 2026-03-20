using System.Collections.Generic;
using System.Linq;
using UI_System.Restaurant_UI_System.Main;
using UnityEngine;

namespace Common.Item.Data.Serving_Note
{
    [CreateAssetMenu(menuName = "Minyinpop/Item/Serving Note", fileName = "New Data")]
    public sealed class ServingNoteSO : ItemSO
    {
        [field: Header("Components")]
        [field: SerializeField] private GameObject servingNotePrefab;

        public List<IItem> OrderedItems { get; private set; } = new();

        #region Interaction
            public override void Selected() { }
            public override void UnSelected() { }
            public override void Use()
            {
                if (!RestaurantUISystem.TryInitializeServingNoteUI(this, servingNotePrefab))
                    RestaurantUISystem.ToggleServingNoteUI(this);
            }
            public override void Remove()
            {
                RestaurantUISystem.RemoveServingNoteUI(this);
            }
        #endregion

        public void SetOrderedItems(List<IItem> items)
        {
            Reset();
            OrderedItems = items.ToList();
        }

        public void Reset()
        {
            OrderedItems.Clear();
        }
    }
}