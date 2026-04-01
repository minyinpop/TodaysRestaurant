namespace Common.Data_Saver.Player_Character_Saver.Child
{
    [System.Serializable]
    public sealed class CharacterSaveDataEntry
    {
        public string CharacterName { get; }
        public int Health { get; }

        public CharacterSaveDataEntry(string characterName, int health)
        {
            CharacterName = characterName;
            Health = health;
        }
    }
}