using UnityEngine;

namespace Data.Player.Child.Team
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Team Data", fileName = "New Data")]
    internal sealed class TeamSO : ScriptableObject
    {
        [field: SerializeField] private int CharacterNumber;
        public void GetCharacterNumber(out int number) => number = CharacterNumber;
    }
}