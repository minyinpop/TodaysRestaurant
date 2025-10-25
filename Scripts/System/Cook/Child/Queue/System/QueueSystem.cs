using System.Collections.Generic;
using System.Cook.Child.Queue.Object;
using UnityEngine;

namespace System.Cook.Child.Queue.System
{
    internal sealed class QueueSystem : MonoBehaviour
    {
        [field: SerializeField] private Transform QueuePointParent;
        private readonly Queue<QueuePoint> QueuePoints = new();

        private void Awake()
        {
            for (var i = 0; i < QueuePointParent.childCount; i++)
            {
                var point = QueuePointParent.GetChild(i).GetComponent<QueuePoint>();
                QueuePoints.Enqueue(point);
            }
        }

        public void TryGetEmptyPoint(out bool haveEmptyPoint, out QueuePoint emptyPoint)
        {
            foreach (var point in QueuePoints)
            {
                point.IsOccupied(out var isOccupied); Debug.Log($"Is occupied: {isOccupied}");
                if (isOccupied) continue;
                haveEmptyPoint = true;
                emptyPoint = point;
                return;
            }
            
            haveEmptyPoint = false;
            emptyPoint = null;
        }
    }
}