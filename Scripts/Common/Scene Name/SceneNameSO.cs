using UnityEngine;

namespace Common.Scene_Name
{
    [CreateAssetMenu(menuName = "Minyinpop/Scene Name", fileName = "New Data")]
    public sealed class SceneNameSO : ScriptableObject
    {
        [field: Header("Scene Name")]
        [field: SerializeField] private string sceneName;
                                public string SceneName => sceneName;
    }
}