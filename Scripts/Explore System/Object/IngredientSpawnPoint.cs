using Common.Item.Object;
using UnityEngine;

namespace Explore_System.Object
{
    public sealed class IngredientSpawnPoint : MonoBehaviour
    {
        private ItemObject _currentItemObject;

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
            var rotation = Quaternion.Euler(transform.rotation.x, Random.Range(0, 360), transform.rotation.z);
            var parent = transform;

            _currentItemObject = Instantiate(prefab, position, rotation, parent).GetComponent<ItemObject>();
        }
    }
}