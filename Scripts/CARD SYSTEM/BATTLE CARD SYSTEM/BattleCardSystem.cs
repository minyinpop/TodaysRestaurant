using CARD_SYSTEM.BATTLE_CARD_SYSTEM.POINTER_EVENT_SYSTEM;
using UnityEngine;

namespace CARD_SYSTEM.BATTLE_CARD_SYSTEM
{
    [RequireComponent(typeof(PointerEventSystem))]
    internal abstract class BattleCardSystem : MonoBehaviour
    {
        private PointerEventSystem PointerEventSystem;

        private bool Selected;

        private void Awake()
        {
            PointerEventSystem = GetComponent<PointerEventSystem>();
        }
    }
}