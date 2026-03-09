using Common.Level.Main;
using UnityEngine;

namespace Explore_System.System
{
    public partial class ExploreSystem : MonoBehaviour
    {
        [field: Header("Develop Only")]
        [field: SerializeField] private LevelSO levelData;

        private void OnValidate()
        {
            if (levelData == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(levelData)} cannot be null.");
            }

            if (ingredientSpawnPoints.Length <= 0)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(ingredientSpawnPoints)} cannot be empty.");
            }
        }
        
        private void Start()
        {
            // TODO 因為開發中，所以寫在 Start 裡，之後就交給過場系統
            StartSystem();
        }

        private void OnDisable()
        {
            // TODO 因為開發中，所以寫在 OnDisable 裡，之後就交給過場系統
            EndSystem();
        }

        public void StartSystem()
        {
            InitializeIngredient();
            InitializeEnemy();
        }

        public void EndSystem()
        {
            // TODO
        }
    }
}