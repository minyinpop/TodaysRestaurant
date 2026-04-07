using Common.Character;

namespace Common.Data_Saver.Player_Character_Saver.Child
{
    [System.Serializable]
    public sealed class CharacterSaveData
    {
        public CharacterType CharacterType { get; }
        
        public int Health { get; }

        public CharacterSaveData(CharacterType characterType, int health)
        {
            CharacterType = characterType;
            Health = health;
        }
    }
}