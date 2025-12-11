using System;
using UnityEngine;
using Utage;

namespace Dialogue_System.Utage
{
    internal sealed class UtageReceiveMessageSystem : MonoBehaviour
    {
        public static event Action<string, string, float> ShowChapterTitle;
        public static event Action<string> ChangeScene;

        private void OnDoCommand(AdvCommandSendMessage command)
        {
            switch (command.Name)
            {
                case "Title":
                {
                    var title = command.ParseCellOptional(AdvColumnName.Arg2, "");
                    var subtitle = command.ParseCellOptional(AdvColumnName.Arg3, "");
                    var duration = command.ParseCellOptional(AdvColumnName.Arg6, 3);
                    
                    ShowChapterTitle?.Invoke(title, subtitle, duration);
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