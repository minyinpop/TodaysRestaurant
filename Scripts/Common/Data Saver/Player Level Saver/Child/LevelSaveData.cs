using System.Collections.Generic;

namespace Common.Data_Saver.Player_Level_Saver.Child
{
    [System.Serializable]
    public sealed class LevelSaveData
    {
        public List<LevelSaveDataEntry> UnlockLevels { get; }

        public LevelSaveData(List<LevelSaveDataEntry> unlockLevels)
        {
            UnlockLevels = unlockLevels;
        }
    }
}