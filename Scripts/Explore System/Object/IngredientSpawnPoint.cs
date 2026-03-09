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
                Destroy(gameObject);
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
            var position = transform.position;
            var rotation = new Quaternion(itemObject.gameObject.transform.rotation.x, Random.Range(0, 360), itemObject.gameObject.transform.rotation.z, itemObject.gameObject.transform.rotation.w);
            var parent = transform;

            _currentItemObject = Instantiate(prefab, position, rotation, parent).GetComponent<ItemObject>();
        }
    }
}