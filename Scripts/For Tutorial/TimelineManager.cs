using UnityEngine;
using UnityEngine.Playables;

namespace For_Tutorial
{
    internal class TimelineManager : MonoBehaviour
    {
        [field: SerializeField] private PlayableDirector Director { get; set; }
        
        /// <summary>
        /// 給予 Timeline 做呼叫用的
        /// </summary>
        public void Resume() => Director.Resume();
    }
}