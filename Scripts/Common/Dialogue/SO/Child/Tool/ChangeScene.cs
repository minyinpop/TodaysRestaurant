using Common.Dialogue.Data;
using Common.Scene_Name;
using Common.Scene_Starter;
using UnityEngine;

namespace Common.Dialogue.Child.Tool
{
    [CreateAssetMenu(menuName = "Minyinpop/Dialogue/Child/Tool/Change Scene", fileName = "New Data")]
    public sealed class ChangeScene : DialogueData
    {
        [field: Header("State")]
        [field: SerializeField] private bool autoPass;
                                public override bool AutoPass => autoPass;
        
        [field: Header("Scene Name")]
        [field: SerializeField] private SceneNameType sceneNameType;
                                public SceneNameType SceneNameType => sceneNameType;
        [field: SerializeField] private SceneStarterData sceneStarterData;
                                public SceneStarterData SceneStarterData => sceneStarterData;
        
        private const DialogueDataType _dialogueDataType = DialogueDataType.ChangeScene;
        public override DialogueDataType DialogueDataType => _dialogueDataType;
    }
}