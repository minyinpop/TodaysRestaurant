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
                    if (ShowChapterTitle is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {nameof(ShowChapterTitle)} cannot be null.");
                        Destroy(gameObject);
                        return;
                    }

                    var title = command.ParseCellOptional(AdvColumnName.Arg2, "");
                    var subtitle = command.ParseCellOptional(AdvColumnName.Arg3, "");
                    var duration = command.ParseCellOptional(AdvColumnName.Arg6, 3);
                    
                    ShowChapterTitle.Invoke(title, subtitle, duration);
                    break;
                }
                case "ChangeScene":
                {
                    if (ChangeScene is null)
                    {
                        Debug.Log($"{name} > {GetType().Name} > {nameof(ChangeScene)} cannot be null.");
                        Destroy(gameObject);
                        return;
                    }
                    
                    var label = command.ParseCellOptional(AdvColumnName.Arg2, "");
                    
                    ChangeScene.Invoke(label);
                    break;
                }
            }
        }

        private void OnWait(AdvCommandSendMessage command)
        {
        }
    }
}