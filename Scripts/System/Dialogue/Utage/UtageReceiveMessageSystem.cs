using UnityEngine;
using Utage;

namespace System.Dialogue.Utage
{
    internal sealed class UtageReceiveMessageSystem : MonoBehaviour
    {
        public static event Action<Action> ShowChapterTitle;
        public static event Action<string, int> StartScenario;

        private void OnDoCommand(AdvCommandSendMessage command)
        {
            switch (command.Name)
            {
                case "Title":
                {
                    ShowChapterTitle?.Invoke(() =>
                    {
                    });
                    break;
                }
                case "StartScenario":
                {
                    Debug.Log("Start Scenario");
                    var label = command.ParseCellOptional(AdvColumnName.Arg2, "");
                    var page = command.ParseCellOptional(AdvColumnName.Arg3, 0);
                    StartScenario?.Invoke(label, page);
                    break;
                }
                default:
                {
                    throw new Exception("未知指令");
                }
            }
        }

        private void OnWait(AdvCommandSendMessage command)
        {
        }
    }
}