using System.Collections.Generic;
using UnityEngine;

namespace BATTLE_MANAGEMENT_SYSTEM.CARD_SYSTEM
{
    [CreateAssetMenu(menuName = "Minyinpop/CardSO", fileName = "New Name", order = 1)]
    internal class CardSO : ScriptableObject
    {
        [field: Header("Image")]
        [field: SerializeField] public List<Sprite> FrontImage { get; private set; }
        [field: SerializeField] public List<Sprite> BackImage { get; private set; }
    }
}