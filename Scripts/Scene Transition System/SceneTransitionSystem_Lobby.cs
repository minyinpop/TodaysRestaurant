using Lobby_System.Main;
using UnityEngine.SceneManagement;

namespace Scene_Transition_System
{
    public partial class SceneTransitionSystem
    {
        private void GoToLobby()
        {
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: lobbySceneNameData.SceneName,
                onSceneLoaded: onComplete =>
                {
                    var scene = SceneManager.GetSceneByName(lobbySceneNameData.SceneName);
                    var rootObjects = scene.GetRootGameObjects();

                    foreach (var rootObject in rootObjects)
                    {
                        if (rootObject.TryGetComponent<LobbySystem>(out var lobbySystem))
                        {
                            lobbySystem.StartSystem(onComplete);
                            break;
                        }
                    }
                });
            StartCoroutine(_changeSceneCoroutine);
        }
    }
}