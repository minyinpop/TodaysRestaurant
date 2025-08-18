using UnityEngine;

namespace BATTLE.CARD_SYSTEM.CARD
{
    internal interface ICard
    {
        public void OnSpawnInCardPool(Transform slotParent);
        public void OnDrawCardAndShow(Transform slotParent);
    }
}