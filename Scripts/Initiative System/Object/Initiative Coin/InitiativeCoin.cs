using UnityEngine;

namespace Initiative_System.Object.Initiative_Coin
{
    [RequireComponent(typeof(AnimationSystem))]
    internal sealed class InitiativeCoin : MonoBehaviour
    {
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
    }
}