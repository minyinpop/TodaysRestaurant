using Common.Player.Child.Player_Deck;
using Common.Player.Child.Player_Team;
using Common.Player.Child.Player_Unlock_Food;
using UnityEngine;

namespace Common.Player.Main
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Main/Player", fileName = "New Data")]
    internal sealed class PlayerSO : ScriptableObject
    {
        [field: Header("Player Data")]
        [field: SerializeField] private PlayerDeckSO playerDeck;
                                public PlayerDeckSO PlayerDeck => playerDeck;
        [field: SerializeField] private PlayerTeamSO playerTeam;
                                public PlayerTeamSO PlayerTeam => playerTeam;
        [field: SerializeField] private PlayerUnlockFoodSO playerUnlockFood;
                                public PlayerUnlockFoodSO PlayerUnlockFood => playerUnlockFood;
    }
}