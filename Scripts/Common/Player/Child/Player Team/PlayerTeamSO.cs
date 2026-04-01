using Common.Character;
using UnityEngine;

namespace Common.Player.Child.Player_Team
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Team", fileName = "New Data")]
    internal sealed class PlayerTeamSO : ScriptableObject
    {
        [field: Header("Team")]
        [field: SerializeField] private CharacterSO[] characters;
                                public int CharacterNumber => characters.Length;

        [field: Header("Character")]
        [field: SerializeField] private CharacterSO bernardData;
                                public CharacterSO BernardData => bernardData;
        [field: SerializeField] private CharacterSO rayData;
                                public CharacterSO RayData => rayData;
    }
}