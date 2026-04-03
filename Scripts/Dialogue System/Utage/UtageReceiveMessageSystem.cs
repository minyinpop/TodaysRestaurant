using System;
using Common.Database;
using Common.Level.Main;
using UnityEngine;
using Utage;

namespace Dialogue_System.Utage
{
    internal sealed class UtageReceiveMessageSystem : MonoBehaviour
    {
        public static event Action<string, string, float> ShowChapterTitle;
        public static event Action<LevelSO> GoToExplore;

        private void OnDoCommand(AdvCommandSendMessage command)
        {
            switch (command.Name)
            {
                case "Title":
                {
                    #region 必要條件檢查
                        if (ShowChapterTitle is null)
                        {
                            throw new InvalidOperationException(nameof(ShowChapterTitle));
                        }
                    #endregion

                    #region 讀取資料
                        var title = command.ParseCellOptional(AdvColumnName.Arg2, "");
                        var subtitle = command.ParseCellOptional(AdvColumnName.Arg3, "");
                        var duration = command.ParseCellOptional(AdvColumnName.Arg6, 3);
                    #endregion
                    
                    ShowChapterTitle.Invoke(title, subtitle, duration);
                    break;
                }
                case "GoToExplore":
                {
                    #region 必要條件檢查
                        if (GoToExplore is null)
                        {
                            throw new InvalidOperationException(nameof(GoToExplore));
                        }
                    #endregion

                    #region 讀取資料
                        var levelName = command.ParseCellOptional(AdvColumnName.Arg2, "");
                    #endregion

                    if (LevelDatabase.GetLevel(levelName, out var levelData))
                    {
                        GoToExplore.Invoke(levelData);
                    }
                    else
                    {
                        throw new InvalidOperationException(nameof(levelName));
                    }

                    break;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(command.Name);
                }
            }
        }
        
        private void OnWait(AdvCommandSendMessage command)
        {
        }
    }
}