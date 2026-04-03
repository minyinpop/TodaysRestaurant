namespace Scene_Transition_System
{
    public partial class SceneTransitionSystem
    {
        private void GoToLobby()
        {
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: lobbySceneNameData.SceneName,
                onSceneLoaded: _ =>
                {
                    // TODO LobbySystem 還沒製作 StartSystem
                    
                    /*
                    var scene = SceneManager.GetSceneByName(exploreSceneNameData.SceneName);
                    var rootObjects = scene.GetRootGameObjects();

                    foreach (var rootObject in rootObjects)
                    {
                        if (rootObject.TryGetComponent<LobbySystem>(out var lobbySystem))
                        {
                            lobbySystem.StartSystem(onComplete);
                            break;
                        }
                    }
                    */
                });
            StartCoroutine(_changeSceneCoroutine);
        }
    }
}