using System.Collections.Generic;
using UnityEngine;

namespace DataBase.Route
{
    /// <summary>
    /// 用來儲存角色的移動路線。
    /// </summary>
    [CreateAssetMenu(fileName = "New Character Route Data", menuName = "Character Data/Route", order = 3)]
    public class RouteData : ScriptableObject
    {
        [field: Header("座標點"), Tooltip("目標椅子的座標點。"), SerializeField]
        public Vector3 TargetSeatPosition { get; private set; }
        
        [field: Tooltip("從 A 點移動到 B 點的路線關鍵點陣列。"), SerializeField]
        public List<Vector3> RouteAToBList { get; private set; }

        [field: Tooltip("從 B 點移動到 A 點的路線關鍵點陣列。"), SerializeField]
        public List<Vector3> RouteBToAList { get; private set; }
    }
}
