using System.Collections;
using Common.Level.Main;
using Explore_System.System.Child.Scene_System.Child;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Explore_System.System.Child.Scene_System.Main
{
    public sealed class SceneSystem : MonoBehaviour
    {
        private LevelSO _levelData;
        
        private bool _initialized;

        private Scene _ingredientScene;
        private Scene _enemyScene;

        private IEnumerator _initializeCoroutine;

        public void StartSystem(LevelSO levelData)
        {
            _levelData = levelData;
            
            _initializeCoroutine = Initialize();
            StartCoroutine(_initializeCoroutine);
            return;

            IEnumerator Initialize()
            {
                #region Ingredient
                    var operation = SceneManager.LoadSceneAsync(_levelData.ExploreSceneName, LoadSceneMode.Additive);
                    if (operation is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {nameof(operation)} cannot find the new scene.");
                        Destroy(gameObject);
                        yield break;
                    }

                    yield return operation;

                    var scene = SceneManager.GetSceneByName(levelData.ExploreSceneName);
                    var rootObjects = scene.GetRootGameObjects();
                    var canGetIngredientSystem = false;
                    var canGetEnemySystem = false;

                    foreach (var rootObject in rootObjects)
                    {
                        if (canGetIngredientSystem)
                        {
                            Debug.Log($"{name} > {GetType().Name} > {nameof(IngredientSystem)} is already get.");
                            Destroy(gameObject);
                            yield break;
                        }

                        if (canGetEnemySystem)
                        {
                            Debug.Log($"{name} > {GetType().Name} > {nameof(EnemySystem)} is already get.");
                            Destroy(gameObject);
                            yield break;
                        }

                        if (rootObject.TryGetComponent<IngredientSystem>(out var ingredientSystem))
                        {
                            canGetIngredientSystem = true;
                            ingredientSystem.InitializeIngredient(_levelData);
                        }
                        else if (rootObject.TryGetComponent<EnemySystem>(out var enemySystem))
                        {
                            canGetEnemySystem = true;
                            enemySystem.InitializeEnemy(_levelData);
                        }
                    }

                    if (!canGetIngredientSystem)
                    {
                        Debug.Log($"{name} > {GetType().Name} > cannot find {nameof(IngredientSystem)} in {nameof(rootObjects)}");
                        Destroy(gameObject);
                        yield break;
                    }
                    
                    if (!canGetEnemySystem)
                    {
                        Debug.Log($"{name} > {GetType().Name} > cannot find {nameof(EnemySystem)} in {nameof(rootObjects)}");
                        Destroy(gameObject);
                    }
                #endregion
            }
        }

        private void EndSystem()
        {
        }
    }
}