namespace Scene_Transition_System
{
    public partial class SceneTransitionSystem
    {
        private void GoToRestaurant()
        {
            _changeSceneCoroutine = ChangeSceneCoroutine(
                sceneName: restaurantSceneNameData.SceneName,
                onSceneLoaded: onComplete =>
                {
                    onComplete.Invoke();
                });
            StartCoroutine(_changeSceneCoroutine);
        }
    }
}