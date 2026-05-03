using System.Collections.Generic;
using Common.Interactable_Object;
using Common.Item.Data;
using Player_System.Object;
using UnityEngine;

namespace Storage_System.Main
{
    public abstract class StorageSystem : MonoBehaviour, InteractableObject
    {
        public abstract bool Interactable { get; }
        
        public abstract GameObject StorageUIPrefab { get; }

        public abstract IReadOnlyList<ItemSO> ItemsData { get; }
        
        public abstract void OnEnterDetect(PlayerObject playerObject);
        public abstract void OnExitDetect();
        public abstract void Interact(PlayerObject playerObject);
    }
}