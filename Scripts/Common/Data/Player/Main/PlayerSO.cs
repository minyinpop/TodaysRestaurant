using Common.Data.Player.Child.Player_Attribute;
using Common.Data.Player.Child.Player_Deck;
using Common.Data.Player.Child.Player_Inventory;
using Common.Data.Player.Child.Player_Level;
using Common.Data.Player.Child.Player_Team;
using Common.Data.Player.Child.Player_Unlock_Food;
using UnityEngine;

namespace Common.Data.Player.Main
{
    [CreateAssetMenu(menuName = "Minyinpop/Player/Main/Player", fileName = "New Data")]
    internal sealed class PlayerSO : ScriptableObject
    {
        [field: Header("Player Data")]
        [field: SerializeField] private PlayerAttributeSO playerAttribute;
                                public PlayerAttributeSO PlayerAttribute => playerAttribute;
        [field: SerializeField] private PlayerDeckSO playerDeck;
                                public PlayerDeckSO PlayerDeck => playerDeck;
        [field: SerializeField] private PlayerInventorySO playerInventory;
                                public PlayerInventorySO PlayerInventory => playerInventory;
        [field: SerializeField] private PlayerLevelSO playerLevel;
                                public PlayerLevelSO PlayerLevel => playerLevel;
        [field: SerializeField] private PlayerTeamSO playerTeam;
                                public PlayerTeamSO PlayerTeam => playerTeam;
        [field: SerializeField] private PlayerUnlockFoodSO playerUnlockFood;
                                public PlayerUnlockFoodSO PlayerUnlockFood => playerUnlockFood;
    }
}