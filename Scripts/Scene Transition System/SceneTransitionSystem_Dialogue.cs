using System;
using Dialogue_System.Custom;
using UnityEngine.SceneManagement;

namespace Scene_Transition_System
{
    public partial class SceneTransitionSystem
    {
        private void GoToDialogue(string label, int page)
        {
            var systemFound = false;

            DialogueSystem dialogueSystem = null;
            
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: dialogueSceneNameData.SceneName,
                onSceneLoaded: onLoaded =>
                {
                    var scene = SceneManager.GetSceneByName(dialogueSceneNameData.SceneName);
                    var rootObjects = scene.GetRootGameObjects();
                    
                    foreach (var rootObject in rootObjects)
                    {
                        if (rootObject.TryGetComponent(out dialogueSystem))
                        {
                            systemFound = true;
                            onLoaded.Invoke();
                            break;
                        }
                    }
                    
                    if (!systemFound)
                    {
                        throw new InvalidOperationException(nameof(DialogueSystem));
                    }
                },
                onComplete: () =>
                {
                    dialogueSystem.StartSystem(label, page);
                });
            StartCoroutine(_changeSceneCoroutine);
        }
    }
}