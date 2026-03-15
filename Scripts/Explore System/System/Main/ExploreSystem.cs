using System.Collections;
using Common.Level.Main;
using Explore_System.System.Child.Scene_System.Main;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Explore_System.System.Main
{
    public sealed class ExploreSystem : MonoBehaviour
    {
        [field: Header("System")]
        [field: SerializeField] private EventSystem eventSystem;
        
        private bool _initialized;

        private Scene _currentScene;
        
        private IEnumerator _initializeCoroutine;

        private void Awake()
        {
            if (eventSystem is null)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(eventSystem)} cannot be null.)");
                Destroy(gameObject);
            }
        }

        public void StartSystem(LevelSO levelData)
        {
            if (_initialized)
            {
                Debug.Log($"{name} > {GetType().Name} > {nameof(_initialized)} is already initialize.)");
                Destroy(gameObject);
                return;
            }
            
            _initializeCoroutine = Initialize();
            StartCoroutine(_initializeCoroutine);
            return;

            IEnumerator Initialize()
            {
                var operation = SceneManager.LoadSceneAsync(levelData.SceneName, LoadSceneMode.Additive);
                if (operation is null)
                {
                    Debug.Log($"{name} > {GetType().Name} > {nameof(operation)} cannot find the new scene.");
                    Destroy(gameObject);
                    yield break;
                }

                yield return operation;
                
                _currentScene = SceneManager.GetSceneByName(levelData.SceneName);
                var rootObjects = _currentScene.GetRootGameObjects();
                var canGetSceneSystem = false;
                
                foreach (var rootObject in rootObjects)
                {
                    if (rootObject.TryGetComponent<SceneSystem>(out var system))
                    {
                        canGetSceneSystem = true;
                        
                        system.StartSystem(levelData);
                        break;
                    }
                }

                if (!canGetSceneSystem)
                {
                    Debug.Log($"{name} > {GetType().Name} > cannot find {nameof(SceneSystem)} in {nameof(rootObjects)}");
                    Destroy(gameObject);
                }
            }
        }

        public void EndSystem()
        {
        }
    }
}