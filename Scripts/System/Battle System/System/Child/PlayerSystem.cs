using Data.Player;
using UnityEngine;

namespace System.Battle_System.System.Child
{
    internal sealed class PlayerSystem : MonoBehaviour
    {
        [field: Header("Data")]
        [field: SerializeField] private PlayerSO PlayerData;
    }
}