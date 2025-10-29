using UnityEngine;
using Utage;

namespace System.Dialogue.Utage
{
    internal sealed class UtageReceiveMessageSystem : MonoBehaviour
    {
        public static event Action<string, string> ShowChapterTitle;
        public static event Action<string> ChangeScene;

        private void OnDoCommand(AdvCommandSendMessage command)
        {
            switch (command.Name)
            {
                case "Title":
                {
                    var title = command.ParseCellOptional(AdvColumnName.Arg2, "");
                    var subtitle = command.ParseCellOptional(AdvColumnName.Arg3, "");
                    
                    ShowChapterTitle?.Invoke(title, subtitle);
                    break;
                }
                case "ChangeScene":
                {
                    var label = command.ParseCellOptional(AdvColumnName.Arg2, "");
                    
                    ChangeScene?.Invoke(label);
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