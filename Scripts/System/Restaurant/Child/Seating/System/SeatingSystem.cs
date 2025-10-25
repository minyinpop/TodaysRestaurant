using System.Collections.Generic;
using System.Restaurant.Child.Seating.Object;
using UnityEngine;

namespace System.Restaurant.Child.Seating.System
{
    internal sealed class SeatingSystem : MonoBehaviour
    {
        [field: SerializeField] private Transform SeatParent;
        private readonly Queue<Seat> Seats = new();

        private void Awake()
        {
            for (var i = 0; i < SeatParent.childCount; i++)
            {
                var seat = SeatParent.GetChild(i).GetComponent<Seat>();
                Seats.Enqueue(seat);
            }
        }

        public void GetRandomSeat(out bool haveEmptySeat, out Seat seat)
        {
            var emptySeats = new List<Seat>();
            foreach (var currentSeat in Seats)
            {
                currentSeat.IsOccupied(out var isOccupied);
                if (isOccupied) continue;
                emptySeats.Add(currentSeat);
            }

            switch (emptySeats.Count)
            {
                case > 0:
                {
                    haveEmptySeat = true;
                    seat = emptySeats[UnityEngine.Random.Range(0, emptySeats.Count)];
                    break;
                }
                default:
                {
                    haveEmptySeat = false;
                    seat = null;
                    break;
                }
            }
        }
    }
}