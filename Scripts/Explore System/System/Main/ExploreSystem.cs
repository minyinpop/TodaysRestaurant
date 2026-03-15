using System.Collections;
using Common.Level.Main;
using Explore_System.System.Child;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Explore_System.System.Main
{
    public sealed class ExploreSystem : MonoBehaviour
    {
        private LevelSO _levelData;
        
        private Scene _terrainScene;
        private Scene _exploreScene;
        
        private bool _initialized;
        private IEnumerator _initializeCoroutine;

        public void StartSystem(LevelSO levelData)
        {
            if (_initialized)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(_initialized)} is already initialize.");
                Destroy(gameObject);
                return;
            }
            
            _levelData = levelData;
            
            _initializeCoroutine = Initialize();
            StartCoroutine(_initializeCoroutine);
            return;

            IEnumerator Initialize()
            {
                #region Spawn Terrain
                    var operation = SceneManager.LoadSceneAsync(_levelData.TerrainSceneName, LoadSceneMode.Additive);
                    if (operation is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {_levelData.ExploreSceneName} cannot find the new scene.");
                        Destroy(gameObject);
                        yield break;
                    }

                    yield return operation;
                #endregion
                
                #region Spawn Enemy / Ingredient
                    operation = SceneManager.LoadSceneAsync(_levelData.ExploreSceneName, LoadSceneMode.Additive);
                    if (operation is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {_levelData.ExploreSceneName} cannot find the new scene.");
                        Destroy(gameObject);
                        yield break;
                    }

                    yield return operation;
                    
                    _exploreScene = SceneManager.GetSceneByName(_levelData.ExploreSceneName);
                    var rootObjects = _exploreScene.GetRootGameObjects();
                    var canGetEnemySystem = false;
                    var canGetIngredientSystem = false;

                    foreach (var rootObject in rootObjects)
                    {
                        if (!canGetEnemySystem)
                        {
                            if (rootObject.TryGetComponent<EnemySystem>(out var enemySystem))
                            {
                                canGetEnemySystem = true;
                                enemySystem.InitializeEnemy(_levelData);
                            }
                        }

                        if (!canGetIngredientSystem)
                        {
                            if (rootObject.TryGetComponent<IngredientSystem>(out var ingredientSystem))
                            {
                                canGetIngredientSystem = true;
                                ingredientSystem.InitializeIngredient(_levelData);
                            }
                        }
                    }
                    
                    if (!canGetEnemySystem)
                    {
                        Debug.Log($"{name} > {GetType().Name} > cannot find {nameof(EnemySystem)} in {nameof(rootObjects)}");
                        Destroy(gameObject);
                        yield break;
                    }
                    
                    if (!canGetIngredientSystem)
                    {
                        Debug.Log($"{name} > {GetType().Name} > cannot find {nameof(IngredientSystem)} in {nameof(rootObjects)}");
                        Destroy(gameObject);
                    }
                #endregion
            }
        }

        public void EndSystem()
        {
        }
    }
}