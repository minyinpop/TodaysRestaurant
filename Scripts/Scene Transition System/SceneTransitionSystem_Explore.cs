using System;
using Common.Level.Main;
using Explore_System.System.Main;
using UnityEngine.SceneManagement;

namespace Scene_Transition_System
{
    public partial class SceneTransitionSystem
    {
        private void GoToExplore(LevelSO levelData)
        {
            var systemFound = false;
            
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: exploreSceneNameData.SceneName,
                onSceneLoaded: onComplete =>
                {
                    var scene = SceneManager.GetSceneByName(exploreSceneNameData.SceneName);
                    var rootObjects = scene.GetRootGameObjects();
                    
                    foreach (var rootObject in rootObjects)
                    {
                        if (rootObject.TryGetComponent<ExploreSystem>(out var exploreSystem))
                        {
                            systemFound = true;
                            
                            exploreSystem.StartSystem(levelData, onComplete);
                            break;
                        }
                    }
                    
                    if (!systemFound)
                    {
                        throw new InvalidOperationException(nameof(ExploreSystem));
                    }
                });
            StartCoroutine(_changeSceneCoroutine);
        }
    }
}