using Player_System.Data.Child.Player_Attribute;
using Player_System.Data.Child.Player_Deck;
using Player_System.Data.Child.Player_Inventory;
using Player_System.Data.Child.Player_Level;
using Player_System.Data.Child.Player_Team;
using Player_System.Data.Child.Player_Unlock_Food;
using UnityEngine;

namespace Player_System.Data.Main
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