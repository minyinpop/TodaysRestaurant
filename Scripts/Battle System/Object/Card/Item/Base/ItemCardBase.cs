using Battle_System.Object.Card.Base;
using Battle_System.Object.Card.Item.Child_System;
using UnityEngine;

namespace Battle_System.Object.Card.Item.Base
{
    [RequireComponent(typeof(AnimationSystem))]
    internal abstract class ItemCardBase : MonoBehaviour, ICard
    {
        [field: Header("Child System")]
        [field: SerializeField] private AnimationSystem AnimationSystem;
    }
}