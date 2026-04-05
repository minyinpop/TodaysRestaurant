using System;
using System.Collections;
using Common.Enemy.Enemy_Object;
using Common.Level.Child.Level_Enemy;
using Common.Level.Main;
using Common.Scene_Starter;
using Explore_System.System.Child.Battle_System.System.Main;
using Explore_System.System.Child.Enemy_System;
using Explore_System.System.Child.Ingredient_System;
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
        private IEnumerator _exploreCoroutine;
        private IEnumerator _battleCoroutine;
        
        private Scene _terrainScene;
        private Scene _exploreScene;
        private Scene _battleScene;

        private Action _onEnemyAttackCleanupAction;
        private Action _onExitBattleCleanupAction;

        private EnemyObject _attackingEnemy;

        private void Awake()
        {
            if (exploreUISystem is null)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(exploreUISystem)} cannot be null.");
            }
        }

        private void OnDisable()
        {
            if (_exploreCoroutine is not null)
            {
                StopCoroutine(_exploreCoroutine);
                _exploreCoroutine = null;
            }

            if (_battleCoroutine is not null)
            {
                StopCoroutine(_battleCoroutine);
                _battleCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            _onEnemyAttackCleanupAction?.Invoke();
            _onExitBattleCleanupAction?.Invoke();
        }

        public override void StartSystem(SceneStarterData starterData, Action onComplete)
        {
            if (_initialized)
            {
                throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(StartSystem)} is already initialize.");
            }

            if (starterData is not LevelSO levelData)
            {
                throw new ArgumentException($"{starterData} is not {nameof(LevelSO)}.");
            }

            _initialized = true;
            _levelData = levelData;
            
            _exploreCoroutine = InitializeExploreCoroutine();
            StartCoroutine(_exploreCoroutine);
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
                    EnemyObject.OnAttack += EnterBattle;
                    _onEnemyAttackCleanupAction = () =>
                    {
                        EnemyObject.OnAttack -= EnterBattle;
                        _onEnemyAttackCleanupAction = null;
                    };

                    BattleSystem.OnClickPlayerWinConfirmButton += ExitBattle;
                    _onExitBattleCleanupAction = () =>
                    {
                        BattleSystem.OnClickPlayerWinConfirmButton -= ExitBattle;
                        _onExitBattleCleanupAction = null;
                    };
                #endregion
                
                onComplete.Invoke();
                
                yield break;
                
                void EnterBattle(EnemyObject enemy, BattleEnemyEntry entry)
                {
                    if (_battleCoroutine is not null)
                    {
                        throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(EnterBattle)} is already in enter battle state.");
                    }

                    _battleCoroutine = EnterBattleCoroutine();
                    StartCoroutine(_battleCoroutine);
                    return;

                    IEnumerator EnterBattleCoroutine()
                    {
                        var complete = false;

                        #region 設定哪個敵人發起的攻擊
                            _attackingEnemy = enemy;
                        #endregion
                        
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
                                if (rootObject.TryGetComponent<BattleSystem>(out var battleSystem))
                                {
                                    isGetSceneStarter = true;
                                    
                                    battleSystem.StartSystem(entry);
                                    break;
                                }
                            }

                            if (!isGetSceneStarter)
                            {
                                Debug.Log($"{name} > {GetType().Name} > cannot find {nameof(BattleSystem)} in {nameof(rootObjects)}");
                                Destroy(gameObject);
                            }
                        #endregion

                        #region 清除戰鬥系統的暫存
                            _battleCoroutine = null;
                        #endregion
                    }
                }

                void ExitBattle()
                {
                    if (_battleCoroutine is not null)
                    {
                        throw new InvalidOperationException($"{name} > {GetType().Name} > {nameof(ExitBattle)} is already in exit battle state.");
                    }

                    _battleCoroutine = ExitBattleCoroutine();
                    StartCoroutine(_battleCoroutine);
                    return;

                    IEnumerator ExitBattleCoroutine()
                    {
                        var complete = false;
                        
                        #region 淡入過場
                            exploreUISystem.FadeIn(
                                onComplete: () => complete = true);
                            yield return new WaitUntil(() => complete);
                        #endregion

                        #region 啟用戰鬥前所關閉的 UI
                            PlayerUISystem.SetHotbarUI(true);
                            PlayerUISystem.SetBackpackUI(true);
                        #endregion

                        #region 清除戰鬥場景
                            yield return SceneManager.UnloadSceneAsync(_battleScene);
                        #endregion
                        
                        #region 啟用探索場景
                            foreach (var rootObject in _exploreScene.GetRootGameObjects())
                            {
                                rootObject.SetActive(true);
                            }
                        #endregion

                        #region 清除發起攻擊的敵人
                            Destroy(_attackingEnemy.gameObject);
                            _attackingEnemy = null;
                        #endregion

                        complete = false;
                        
                        #region 淡出過場
                            exploreUISystem.FadeOut(
                                onComplete: () => complete = true);
                            yield return new WaitUntil(() => complete);
                        #endregion
                        
                        #region 清除戰鬥系統的暫存
                            _battleCoroutine = null;
                        #endregion
                    }
                }
            }
        }
    }
}