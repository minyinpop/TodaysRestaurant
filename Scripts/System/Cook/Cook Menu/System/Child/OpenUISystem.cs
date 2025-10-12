using System.Collections.Generic;
using Data.General.Enum.Item.Child;
using Data.General.Enum.Item.Main;
using Data.Player;
using General.Object.Storage_Slot.Base;
using UnityEngine;

namespace System.Cook.Cook_Menu.System.Child
{
    internal sealed class OpenUISystem : MonoBehaviour
    {
        [field: Header("UI")]
        [field: SerializeField] private GameObject UI;
        
        [field: Header("Unlock Dish Slot")]
        [field: SerializeField] private Transform UnlockDishSlotParent;
        [field: SerializeField] private GameObject UnlockDishSlotPrefab;
        private readonly List<ItemSlot> UnlockDishSlots = new();
        
        [field: Header("Select Dish Slot")]
        [field: SerializeField] private Transform SelectDishSlotParent;
        [field: SerializeField] private GameObject SelectDishSlotPrefab_01;
        [field: SerializeField] private GameObject SelectDishSlotPrefab_02;
        [field: SerializeField] private GameObject SelectDishSlotPrefab_03;
        
        [field: Header("Dish Type Button")]
        [field: SerializeField] private Transform DishTypeButtonParent;
        [field: SerializeField] private GameObject DishTypeButtonPrefab;
        
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;

        private DishType CurrentDishType = DishType.Soup;
        

        public void Open()
        {
            PlayerData.GetUnlockedDishes(out var dishes);
            foreach (var dish in dishes)
            {
                dish.GetItemType(out ItemType itemType, out DishType dishType);
                if (dishType != CurrentDishType) continue;
            }
        }
    }
}