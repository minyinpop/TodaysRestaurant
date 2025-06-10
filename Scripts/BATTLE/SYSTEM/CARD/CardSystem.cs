using UnityEngine;

namespace BATTLE.SYSTEM.CARD
{
    internal class CardSystem : MonoBehaviour
    {
        [field: SerializeField] private CardLayoutSettings CardLayoutSettings { get; set; }

        private void LateUpdate()
        {
            CardLayoutSettings.Update();
        }
    }
}