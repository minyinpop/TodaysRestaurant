using UnityEngine;

namespace Common.Data.Player.Child.Player_Team
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Team", fileName = "New Data")]
    internal sealed class PlayerTeamSO : ScriptableObject
    {
        [field: SerializeField] private int CharacterNumber;
        public void GetCharacterNumber(out int number) => number = CharacterNumber;
    }
}