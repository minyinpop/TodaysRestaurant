using Common.Item.Object;
using UnityEngine;

namespace Explore_System.Object
{
    public sealed class IngredientSpawnPoint : MonoBehaviour
    {
        private ItemObject _currentItemObject;

        public void InitializeIngredient(ItemObject itemObject)
        {
            if (itemObject is null)
            {
                Debug.Log($"{gameObject.name} > {nameof(IngredientSpawnPoint)} > {nameof(InitializeIngredient)} > {nameof(itemObject)} cannot be null.");
                return;
            }

            if (_currentItemObject is not null)
            {
                Debug.Log($"{gameObject.name} is already occupied.");
                return;
            }

            var prefab = itemObject.gameObject;
            var position = transform.position;
            var rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            var parent = transform;

            _currentItemObject = Instantiate(prefab, position, rotation, parent).GetComponent<ItemObject>();
            _currentItemObject.Initialize();
        }
    }
}