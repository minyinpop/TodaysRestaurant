using Common.Interactable_Object;
using Common.Item.Data;
using Player_System.System.Player_System;
using UnityEngine;

namespace Common.Item.Object
{
    public sealed class ItemObject : MonoBehaviour, InteractableObject
    {
        [field: Header("Data")]
        [field: SerializeField] private ItemSO itemData;

        private void Awake()
        {
            if (itemData == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(itemData)} cannot be null.");
                Destroy(gameObject);
            }
        }

        public void OnEnterDetect()
        {
        }

        public void OnExitDetect()
        {
        }

        public bool OnInteract(PlayerSystem playerSystem)
        {
            if (playerSystem.TryAddItem(itemData))
            {
                Destroy(gameObject);
                return true;
            }

            return false;
        }
    }
}