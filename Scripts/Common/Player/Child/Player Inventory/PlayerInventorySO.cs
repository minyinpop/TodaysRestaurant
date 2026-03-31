using System;
using Common.Item.Data;
using UnityEngine;

namespace Common.Player.Child.Player_Inventory
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Inventory", fileName = "New Data")]
    internal sealed class PlayerInventorySO : ScriptableObject
    {
        [field: Header("Rename")]
        [field: SerializeField] private int hotbarNumber;
        [field: SerializeField] private int backpackNumber;

        private IItem[] _hotbarItems;
        private IItem[] _backpackItems;

        private bool _initialized;

        public void Initialize()
        {
            if (_initialized)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > is already initialize.");
            }
            
            _hotbarItems = new IItem[hotbarNumber];
            _backpackItems = new IItem[backpackNumber];
        }
    }
}