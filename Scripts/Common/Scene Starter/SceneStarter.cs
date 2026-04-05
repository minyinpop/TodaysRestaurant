using System;
using UnityEngine;

namespace Common.Scene_Starter
{
    public class SceneStarter : MonoBehaviour
    {
        public virtual void StartSystem(Action onComplete) { }
        
        public virtual void StartSystem(SceneStarterData starterData, Action onComplete) { }
    }
}