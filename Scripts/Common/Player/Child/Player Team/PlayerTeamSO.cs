using Common.Character;
using UnityEngine;

namespace Common.Player.Child.Player_Team
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Team", fileName = "New Data")]
    internal sealed class PlayerTeamSO : ScriptableObject
    {
        [field: SerializeField] private CharacterSO[] characters;
                                public int CharacterNumber => characters.Length;
    }
}