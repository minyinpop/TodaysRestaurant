using System;
using System.Economy.Child.Restaurant.Object.Base;
using UnityEngine;

namespace Data.Economy.Restaurant_System
{
    [Serializable]
    internal sealed class RestaurantPoint
    {
        [field: SerializeField] private Point[] Points;

        public void TryGetEmptyPoint(out bool haveEmptyPoint, out Point point)
        {
            foreach (var currentPoint in Points)
            {
                if (currentPoint.IsOccupied()) continue;
                haveEmptyPoint = true;
                point = currentPoint;
                return;
            }
            
            haveEmptyPoint = false;
            point = null;
        }

        public void GetFirstPoint(out Point point)
        {
            point = Points[0];
        }
    }
}