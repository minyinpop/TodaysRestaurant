using Common.Item.Object;
using UnityEngine;

namespace Explore_System.Object
{
    public sealed class IngredientSpawnPoint : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private Transform spawnPoint;

        private ItemObject _currentItemObject;

        private void Awake()
        {
            if (spawnPoint == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(spawnPoint)} cannot be null.");
                gameObject.SetActive(false);
            }
        }

        public void InitializeIngredient(ItemObject itemObject)
        {
            if (itemObject == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(InitializeIngredient)} > {nameof(itemObject)} cannot be null.");
                return;
            }

            if (_currentItemObject != null)
            {
                Debug.Log($"{gameObject.name} is already occupied.");
                return;
            }

            var prefab = itemObject.gameObject;
            var position = new Vector3(transform.position.x, transform.position.y + (transform.position.y - itemObject.Root.position.y), transform.position.z);
            var rotation = itemObject.transform.rotation;
            var parent = transform;
            
            _currentItemObject = Instantiate(prefab, position, rotation, parent).GetComponent<ItemObject>();
        }
    }
}