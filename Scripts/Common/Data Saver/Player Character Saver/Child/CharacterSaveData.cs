namespace Common.Data_Saver.Player_Character_Saver.Child
{
    [System.Serializable]
    public sealed class CharacterSaveData
    {
        public string CharacterName { get; }
        
        public int Health { get; }

        public CharacterSaveData(string characterName, int health)
        {
            CharacterName = characterName;
            Health = health;
        }
    }
}