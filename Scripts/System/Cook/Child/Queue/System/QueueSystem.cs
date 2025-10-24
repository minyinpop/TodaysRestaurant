using System.Collections.Generic;
using System.Cook.Child.Queue.Object;
using UnityEngine;

namespace System.Cook.Child.Queue.System
{
    internal sealed class QueueSystem : MonoBehaviour
    {
        [field: SerializeField] private Transform PointParent;
        private readonly List<QueuePoint> points = new();
    }
}