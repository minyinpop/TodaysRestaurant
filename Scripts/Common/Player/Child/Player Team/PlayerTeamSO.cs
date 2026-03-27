using Common.Player.Child.Player_Character;
using UnityEngine;

namespace Common.Player.Child.Player_Team
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Child/Team", fileName = "New Data")]
    internal sealed class PlayerTeamSO : ScriptableObject
    {
        [field: Header("Team")]
        [field: SerializeField] private PlayerCharacterSO[] characters;
                                public int CharacterNumber => characters.Length;

        [field: Header("Character")]
        [field: SerializeField] private PlayerCharacterSO bernardData;
                                public PlayerCharacterSO BernardData => bernardData;
        [field: SerializeField] private PlayerCharacterSO rayData;
                                public PlayerCharacterSO RayData => rayData;
    }
}