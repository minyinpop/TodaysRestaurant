using UnityEngine;

namespace Common.Scene_Name
{
    [CreateAssetMenu(menuName = "Minyinpop/Scene Name", fileName = "New Data")]
    public sealed class SceneNameSO : ScriptableObject
    {
        [field: Header("Data")]
        [field: SerializeField] private SceneNameType sceneType;
                                public SceneNameType SceneType => sceneType;
        [field: SerializeField] private string sceneName;
                                public string SceneName => sceneName;
    }
}