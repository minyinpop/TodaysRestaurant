using System;
using Audio_System.Data;
using Audio_System.Main;
using Common.Interactable_Object;
using Common.Item.Data;
using Player_System.Object;
using UnityEngine;

namespace Common.Item.Object
{
    public sealed class ItemObject : MonoBehaviour, InteractableObject
    {
        [field: Header("物品資料")]
        [field: SerializeField] private ItemSO itemData;
        
        [field: Header("音效資料")]
        [field: SerializeField] private PlaySFXData takeSFX;

        public static event Action OnTake;

        private void Awake()
        {
            if (itemData is null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(itemData)} cannot be null.");
                Destroy(gameObject);
            }
        }

        public void OnEnterDetect(PlayerObject playerObject)
        {
        }

        public void OnExitDetect(PlayerObject playerObject)
        {
        }

        public bool OnInteract(PlayerObject playerObject)
        {
            if (playerObject.TryAddItem(itemData))
            {
                AudioSystem.Instance.InteractSFX.PlayOneShot(takeSFX);
                
                OnTake?.Invoke();
                
                Destroy(gameObject);
                return true;
            }

            return false;
        }
    }
}