using System;
using System.Collections;
using Common.Level.Main;
using Explore_System.System.Child.Enemy_System;
using Explore_System.System.Child.Ingredient_System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Explore_System.System.Main
{
    public sealed class ExploreSystem : SceneStarter
    {
        private LevelSO _levelData;
        
        private bool _initialized;
        private IEnumerator _initializeCoroutine;
        
        private Scene _terrainScene;
        private Scene _exploreScene;

        public override void StartSystem(LevelSO levelData, Action onComplete)
        {
            if (_initialized)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(_initialized)} is already initialize.");
                Destroy(gameObject);
                return;
            }

            if (levelData is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(StartSystem)} > {nameof(levelData)} cannot be null.");
                Destroy(gameObject);
                return;
            }

            _initialized = true;
            _levelData = levelData;
            
            _initializeCoroutine = InitializeCoroutine();
            StartCoroutine(_initializeCoroutine);
            return;

            IEnumerator InitializeCoroutine()
            {
                #region Spawn Terrain
                    var operation = SceneManager.LoadSceneAsync(_levelData.TerrainSceneNameData.SceneName, LoadSceneMode.Additive);
                    if (operation is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {_levelData.ExploreSceneNameData} cannot find the new scene.");
                        Destroy(gameObject);
                        yield break;
                    }

                    yield return operation;
                #endregion
                
                #region Spawn Enemy / Ingredient
                    operation = SceneManager.LoadSceneAsync(_levelData.ExploreSceneNameData.SceneName, LoadSceneMode.Additive);
                    if (operation is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {_levelData.ExploreSceneNameData} cannot find the new scene.");
                        Destroy(gameObject);
                        yield break;
                    }

                    yield return operation;
                    
                    _exploreScene = SceneManager.GetSceneByName(_levelData.ExploreSceneNameData.SceneName);
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
                
                onComplete.Invoke();
            }
        }

        public void EndSystem()
        {
        }
    }
}