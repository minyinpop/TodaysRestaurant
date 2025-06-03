using System;
using Storage.Base;
using UnityEngine;
using Utility;

namespace Player
{
    internal class PlayerBackpack : MonoBehaviour
    {
        private InputManager Input { get; set; }
        
        [field: SerializeField] private StorageData StorageData { get; set; }

        private void Awake()
        {
            Input = InputSystem.Input;
            
            var info = $"\n遊戲物件：{gameObject.name}\n遊戲組件：{GetType().Name}\n";
            if (Tools.CheckNull(Input is null, info) ||
                Tools.CheckNull(!StorageData, info)) enabled = false;
        }
    }
}