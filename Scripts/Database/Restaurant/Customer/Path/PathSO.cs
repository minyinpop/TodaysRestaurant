using System.Collections.Generic;
using UnityEngine;

namespace Database.Restaurant.Customer.Path
{
    [CreateAssetMenu(menuName = "Minyinpop/Restaurant/Customer/Path", fileName = "New Data", order = 2)]
    public class PathSO : ScriptableObject
    {
        [field: Header("門口到座位的路徑")]
        [field: SerializeField] public List<Vector3> GoToSeat { get; private set; }
        
        [field: Header("座位到門口的路徑")]
        [field: SerializeField] public List<Vector3> Leave { get; private set; }
        
        [field: Header("入座點")]
        [field: SerializeField] public Vector3 SeatPoint { get; private set; }
    }
}