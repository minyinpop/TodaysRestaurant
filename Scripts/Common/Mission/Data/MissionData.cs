using UnityEngine;

namespace Common.Mission.Data
{
    public abstract class MissionData : ScriptableObject
    {
        public abstract MissionType MissionType { get; }
    }
}