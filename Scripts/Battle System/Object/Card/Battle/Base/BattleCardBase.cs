using Battle_System.Object.Card.Base;
using Battle_System.Object.Card.Battle.Child_System;
using UnityEngine;

namespace Battle_System.Object.Card.Battle.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class BattleCardBase : MonoBehaviour, ICard
    {
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
    }
}