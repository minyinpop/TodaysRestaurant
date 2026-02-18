using Common.Interactable_Object;
using Common.Item.Data;
using UnityEngine;

namespace Common.Item.Object
{
    public sealed class Item : MonoBehaviour, InteractableObject
    {
        [field: Header("Data")]
        [field: SerializeField] private ItemSO itemData;

        private void Awake()
        {
            if (itemData == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(itemData)} cannot be null.");
            }
        }

        public void OnEnterDetect()
        {
            // TODO
        }

        public void OnExitDetect()
        {
            // TODO
        }
    }
}