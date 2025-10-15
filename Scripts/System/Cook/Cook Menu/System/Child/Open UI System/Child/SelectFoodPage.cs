using System.Collections.Generic;
using General.Object.Item_Slot.Base;
using UnityEngine;

namespace System.Cook.Cook_Menu.System.Child.Open_UI_System.Child
{
    internal sealed class SelectFoodPage : MonoBehaviour
    {
        [field: Header("Select Food Slot")]
        [field: SerializeField] private Transform SelectFoodSlotParent;
        [field: SerializeField] private GameObject SelectFoodSlotPrefab_01;
        [field: SerializeField] private GameObject SelectFoodSlotPrefab_02;
        [field: SerializeField] private GameObject SelectFoodSlotPrefab_03;
        private readonly List<ItemSlot> SelectFoodSlots = new();
        private readonly List<Action> SelectFoodSlot_Actions = new();
        
        private void OnDisable()
        {
            foreach (var action in SelectFoodSlot_Actions) action?.Invoke();
            SelectFoodSlot_Actions.Clear();
        }

        public void Spawn()
        {
            for (var i = 0; i < 12; i++)
            {
                var slot = Instantiate(i > 2 ? SelectFoodSlotPrefab_01 : SelectFoodSlotPrefab_02, SelectFoodSlotParent);
                var slot_ItemSlot = slot.GetComponent<ItemSlot>();
                SelectFoodSlots.Add(slot_ItemSlot);
            }
        }
    }
}