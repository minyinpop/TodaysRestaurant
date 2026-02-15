using Explore_System.Child.Resource_Spawn_System.Main;
using UnityEngine;

namespace Explore_System.Main
{
    public sealed class ExploreSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private Transform resourceSpawnSystemParent;
                                private static ResourceSpawnSystem _resourceSpawnSystem;

        private void Awake()
        {
            if (resourceSpawnSystemParent == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(resourceSpawnSystemParent)} cannot be null.");
            }
            else
            {
                _resourceSpawnSystem = resourceSpawnSystemParent.GetComponent<ResourceSpawnSystem>();
            }
        }

        public static void StartSystem()
        {
            // TODO
        }

        public static void EndSystem()
        {
            // TODO
        }
    }
}