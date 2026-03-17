using System;
using Common.Level.Main;
using UnityEngine;

namespace Common
{
    public abstract class SceneStarter : MonoBehaviour
    {
        public virtual void StartSystem()
        {
            /*
         * 
         */
        }

        public virtual void StartSystem(LevelSO levelSO, Action onComplete)
        {
            /*
         * 
         */
        }
    }
}