using Common.Level.Main;
using UnityEngine;

namespace Explore_System.System
{
    public partial class ExploreSystem : MonoBehaviour
    {
        private LevelSO _levelData;
        
        public void StartSystem(LevelSO levelData)
        {
            _levelData = levelData;
            
            InitializeIngredient();
            InitializeEnemy();
        }

        public void EndSystem()
        {
            // TODO
        }
    }
}