using System.Collections.Generic;
using System.Cook.Child.Seating.Object;
using UnityEngine;

namespace System.Cook.Child.Seating.System
{
    internal sealed class SeatingSystem : MonoBehaviour
    {
        [field: SerializeField] private Transform SeatParent;
        private readonly List<Seat> Seats = new();

        private void Awake()
        {
            for (var i = 0; i < SeatParent.childCount; i++)
            {
                var seat = SeatParent.GetChild(i).GetComponent<Seat>();
                Seats.Add(seat);
            }
        }

        public void GetRandomSeat(out Seat targetSeat)
        {
            var seats = new List<Seat>();
            foreach (var seat in Seats)
            {
                seat.Check(out var isOccupied);
                if (isOccupied) continue;
                seats.Add(seat);
            }

            targetSeat = seats.Count switch
            {
                0 => null,
                > 0 => seats[UnityEngine.Random.Range(0, seats.Count)],
                _ => null
            };
        }
    }
}