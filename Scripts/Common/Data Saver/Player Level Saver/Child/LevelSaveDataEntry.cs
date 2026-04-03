namespace Common.Data_Saver.Player_Level_Saver.Child
{
    [System.Serializable]
    public sealed class LevelSaveDataEntry
    {
        public bool IsUnlock { get; }
        
        public string LevelName { get; }
        
        public LevelSaveDataEntry(bool isUnlock, string levelName)
        {
            IsUnlock = isUnlock;
            
            LevelName = levelName;
        }
    }
}