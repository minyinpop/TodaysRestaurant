using Explore_System.Child.Resource_Spawn_System.System.Child;
using UnityEngine;

namespace Explore_System.Child.Resource_Spawn_System.System.Main
{
    public sealed class ResourceSpawnSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private IngredientSpawnSystem ingredientSpawnSystem;
        [field: SerializeField] private EnemySpawnSystem enemySpawnSystem;

        private void Awake()
        {
            if (ingredientSpawnSystem == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(ingredientSpawnSystem)} cannot be null.");
                return;
            }

            if (enemySpawnSystem == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(enemySpawnSystem)} cannot be null.");
            }
        }

        public void Initialize()
        {
            // TODO
        }
    }
}