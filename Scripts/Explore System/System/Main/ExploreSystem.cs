using System;
using System.Collections;
using Common;
using Common.Level.Main;
using Explore_System.System.Child.Enemy_System;
using Explore_System.System.Child.Ingredient_System;
using Player_System.Object;
using Player_System.System.Player_System;
using UI_System.Explore_UI_System;
using UI_System.Player_UI_System.Main;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Explore_System.System.Main
{
    public sealed class ExploreSystem : SceneStarter
    {
        [field: Header("Component")]
        [field: SerializeField] private ExploreUISystem exploreUISystem;
        
        private LevelSO _levelData;
        
        private bool _initialized;
        private IEnumerator _initializeExploreCoroutine;
        private IEnumerator _initializeBattleCoroutine;
        
        private Scene _terrainScene;
        private Scene _exploreScene;
        private Scene _battleScene;

        private Action _playerHurtEvent;

        private void Awake()
        {
            if (exploreUISystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(exploreUISystem)} cannot be null.");
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            _playerHurtEvent?.Invoke();
        }

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
            
            _initializeExploreCoroutine = InitializeExploreCoroutine();
            StartCoroutine(_initializeExploreCoroutine);
            return;

            IEnumerator InitializeExploreCoroutine()
            {
                #region 生成地形
                    var operation = SceneManager.LoadSceneAsync(_levelData.TerrainSceneNameData.SceneName, LoadSceneMode.Additive);
                    if (operation is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {_levelData.ExploreSceneNameData} cannot find the new scene.");
                        Destroy(gameObject);
                        yield break;
                    }

                    yield return operation;
                #endregion
                
                #region 生成敵人與可採集的資源
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
                
                #region 訂閱事件
                    PlayerObject.OnHurt += InitializeBattle;
                    _playerHurtEvent = () =>
                    {
                        PlayerObject.OnHurt -= InitializeBattle;
                        _playerHurtEvent = null;
                    };
                #endregion
                
                onComplete.Invoke();
                
                yield break;
                
                void InitializeBattle()
                {
                    _initializeBattleCoroutine = InitializeBattleCoroutine();
                    StartCoroutine(_initializeBattleCoroutine);
                    return;

                    IEnumerator InitializeBattleCoroutine()
                    {
                        var complete = false;
                        
                        #region 淡入過場
                            exploreUISystem.FadeIn(
                                onComplete: () => complete = true);
                            yield return new WaitUntil(() => complete);
                        #endregion
                        
                        #region 關閉與戰鬥場景不相關的 UI
                            PlayerUISystem.SetHotbarUI(false);
                            PlayerUISystem.SetBackpackUI(false);
                        #endregion

                        #region 隱藏探索場景
                            foreach (var rootObject in _exploreScene.GetRootGameObjects())
                            {
                                rootObject.SetActive(false);
                            }
                        #endregion
                        
                        #region 生成戰鬥場景
                            yield return SceneManager.LoadSceneAsync(_levelData.BattleSceneNameData.SceneName, LoadSceneMode.Additive);
                        #endregion
                        
                        complete = false;
                        
                        #region 淡出過場
                            exploreUISystem.FadeOut(
                                onComplete: () => complete = true);
                            yield return new WaitUntil(() => complete);
                        #endregion
                        
                        #region 啟動戰鬥系統
                            var isGetSceneStarter = false;
                            _battleScene = SceneManager.GetSceneByName(_levelData.BattleSceneNameData.SceneName);
                            
                            foreach (var rootObject in _battleScene.GetRootGameObjects())
                            {
                                if (rootObject.TryGetComponent<SceneStarter>(out var sceneStarter))
                                {
                                    isGetSceneStarter = true;
                                    
                                    sceneStarter.StartSystem(_levelData);
                                    break;
                                }
                            }

                            if (!isGetSceneStarter)
                            {
                                Debug.Log($"{name} > {GetType().Name} > cannot find {nameof(SceneStarter)} in {nameof(rootObjects)}");
                                Destroy(gameObject);
                            }
                        #endregion
                    }
                }
            }
        }
    }
}