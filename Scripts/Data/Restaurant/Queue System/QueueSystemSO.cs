using System.Collections.Generic;
using System.Restaurant.Child.Customer.Main;
using System.Restaurant.Child.Queue.Object;
using UnityEngine;

namespace Data.Restaurant.Queue_System
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Queue System Data", fileName = "New Data")]
    internal sealed class QueueSystemSO : ScriptableObject
    {
        private readonly Queue<QueuePoint> QueuePoints = new();

        public void Init(Transform parent)
        {
            for (var i = 0; i < parent.childCount; i++)
            {
                var currentPoint = parent.GetChild(i).GetComponent<QueuePoint>();
                QueuePoints.Enqueue(currentPoint);
            }
        }

        public void TryGetEmptyQueuePoint(out bool haveEmptyQueuePoint, out QueuePoint queuePoint)
        {
            foreach (var currentPoint in QueuePoints)
            {
                if (currentPoint.IsOccupied()) continue;
                haveEmptyQueuePoint = true;
                queuePoint = currentPoint;
                return;
            }
            
            haveEmptyQueuePoint = false;
            queuePoint = null;
        }

        public void AddCustomer(QueuePoint queuePoint, CustomerSystem customer) => queuePoint.SetCustomer(customer);
    }
}